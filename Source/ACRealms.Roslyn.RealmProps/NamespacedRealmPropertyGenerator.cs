using ACRealms.Roslyn.RealmProps.IntermediateModels;
using Corvus.Json;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System.Collections.Frozen;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json.Nodes;
using System.Threading;
using System.Xml;
using System.Linq;
using System.Collections.Immutable;
using JsonObject = Corvus.Json.JsonObject;
using System.Diagnostics;
using ACRealms.Roslyn.RealmProps.Builders.Phase1;
using ACRealms.Roslyn.RealmProps.Builders.Phase2Src;
/*using Corvus.Json.Validator;
using JsonObject = Corvus.Json.JsonObject;
using JsonSchema = Corvus.Json.Validator.JsonSchema;
using ValidationContext = Corvus.Json.ValidationContext;*/

#nullable enable

namespace ACRealms.Roslyn.RealmProps;


// To debug: Test Explorer -> ACRealms.Tests.Tests.RealmPropGenerator -> Debug CanGenerateRealmProps test
// Important: Significant changes to this generator's process or algorithm should be reflected in ACRealms.Roslyn.Analyzer.ACR20XX_RealmProps where applicable
//            This allows for more accurate errors to be displayed during compilation
//
// The C# model classes in ACRealms.RealmProps.RealmPropModels are generated through a manual script invocation as needed
//#pragma warning disable RS1038 // Compiler extensions should be implemented in assemblies with compiler-provided references
[Generator(LanguageNames.CSharp)]
//#pragma warning restore RS1038 // Compiler extensions should be implemented in assemblies with compiler-provided references
public class NamespacedRealmPropertyGenerator : IIncrementalGenerator
{
    const bool REPORT_ALL_ERRORS = false;

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        // Regex for property names: https://regexr.com/8915n

        IncrementalValuesProvider<AdditionalText> realmPropsDirContext = context.AdditionalTextsProvider
            .Where(static text => { char sep = Path.DirectorySeparatorChar; return text.Path.Contains($"{sep}PropDefs{sep}json"); });

        IncrementalValuesProvider<(string Path, string Contents)> realmPropFiles = realmPropsDirContext
            .Where(static text => { char sep = Path.DirectorySeparatorChar; return text.Path.Contains($"{sep}PropDefs{sep}json{sep}"); })
            .Where(static text => Path.GetExtension(text.Path).Equals(".jsonc", StringComparison.Ordinal))
            .Select(static (text, cancellationToken) => {
                try
                {
                    return (text.Path, text.GetText(cancellationToken)!.ToString());
                }
                catch (Exception ex)
                {
                    Helpers.ReThrowWrappedException("2" + text.Path, ex);
                    throw;
                }
            });

        // Expensive part here, should only run for the file contents that have changed
        IncrementalValuesProvider<NamespaceData?> realmPropNamespaces = realmPropFiles
            .Select(static (file, cancellationToken) =>
            {
                int step = 0;
                try
                {
                    string commentsRemoved = Helpers.RemoveJsonComments(file.Contents, cancellationToken);
                    step++;

                    var jObject = JsonObject.Parse(commentsRemoved);
                    step++;

                    // RealmPropertySchema is a class, and not cache-safe so we shouldn't return it. Convert to NamespaceData record
                    var namespaceObj = PropDefs.FromJson(jObject.AsJsonElement);
                    step++;

                    string namespaceRaw = namespaceObj.Namespace.GetString()!;

                    IEnumerable<ObjPropInfo> props = Parsers.NamespaceDataParser.GetProps(namespaceRaw, namespaceObj);
                    step++;

                    var array = props.ToImmutableArray();
                    step++;

                    var propsWrapper = new CompilerDomainModels.ImmutableArrayWrapper<ObjPropInfo>(array);
                    step++;

                    return new NamespaceData(
                        originalPath: file.Path,
                        namespaceFull: namespaceObj.Namespace.GetString()!,
                        objProps: propsWrapper,
                        description: namespaceObj.Description?.GetString()
                    );
                }
                catch (Exception ex)
                {
                    if (REPORT_ALL_ERRORS)
                    {
#pragma warning disable CS0162 // Unreachable code detected
                        Helpers.ReThrowWrappedException($"at Step 1.{step}: " + file.Path, ex);
#pragma warning restore CS0162 // Unreachable code detected
                        throw;
                    }
#pragma warning disable CS0162 // Unreachable code detected
                    else
                        return null;
#pragma warning restore CS0162 // Unreachable code detected
                }
            });

        context.RegisterSourceOutput(realmPropNamespaces, static (ctx, data) =>
        {
            if (data != null)
            {
                string fileName = string.Join("/", data.NestedClassNames);
                string sourceCode = NamespacedProps.GenerateNamespacedPropsSourceCode(data);
                ctx.AddSource($"NamespacedProps/{fileName}.g.cs", SourceText.From(sourceCode, Encoding.UTF8));
            }
        });

        context.RegisterSourceOutput(realmPropNamespaces, static (ctx, data) =>
        {
            if (data != null && data.ObjProps.Array.Any())
            {
                string fileName = string.Join("/", data.NestedClassNames);
                string sourceCode = NamespaceJsonSchema.GenerateSchemaSourceCode(data);
                ctx.AddSource($"GeneratedJsonSchema/realm-properties/{fileName}.json", SourceText.From(sourceCode, Encoding.UTF8));
            }
        });


        IncrementalValueProvider<ImmutableArray<NamespaceData>> combinedNamespaces = realmPropNamespaces.Where(static (x) => x != null).Collect();
        var namespaceMetadataForRootSchema = combinedNamespaces.Select(static (allNamespaces, cancellationToken) =>
        {
            return allNamespaces.Select(static x => x.AsStub()).ToImmutableArray();
        });


        context.RegisterSourceOutput(namespaceMetadataForRootSchema, static (ctx, data) =>
        {
            string sourceCode = RootGeneratedPropsSchema.GenerateCombinedSchemaSourceCode(data);
            ctx.AddSource($"GeneratedJsonSchema/realm-properties/realm-properties-root.json", SourceText.From(sourceCode, Encoding.UTF8));
        });

        // And for all the properties combined, regardless of namespace, we create the core enums
        // These will be equivalent to the RealmProperty enums which were hand-maintained before ACRealms v2.2
        IncrementalValueProvider<ImmutableArray<ObjPropInfo>> combinedProps =
           combinedNamespaces.Select(static (x, cancellationToken) => {

                // Here we add some undef props so that at least 1 prop of each type is made, and so that we have a value of 0 for the undefined prop
                IEnumerable<ObjPropInfo> undefProps = new List<PropType> { PropType.integer, PropType.@string, PropType.boolean, PropType.int64, PropType.@float }.Select(type =>
                {
                    return new ObjPropInfo("__NONE__", "Undef")
                    {
                        Description = "",
                        Type = type
                    };
                });
                return x.SelectMany(x => x.ObjProps.Array).Concat(undefProps).ToImmutableArray();
            });
        var types = ObjPropInfo.PropMap.Select(static kvp => ((PropType propType, string enumType))(kvp.Key, kvp.Value)).ToImmutableArray();
        IncrementalValuesProvider<string> typesProvider = combinedProps.SelectMany(static (_, cancellationToken) => ObjPropInfo.PropMap.Values.Distinct());

        IncrementalValuesProvider<(string TargetEnumTypeName, ImmutableArray<ObjPropInfo> AllProps)> corePropsSourceUnfiltered = typesProvider.Combine(combinedProps);
        IncrementalValuesProvider<(string TargetEnumTypeName, ImmutableArray<ObjPropInfo> PropsOfType)> corePropsSource = corePropsSourceUnfiltered.Select((data, cancellationToken) =>
            ((string TargetEnumTypeName, ImmutableArray<ObjPropInfo> PropsOfType))(
                data.TargetEnumTypeName,
                data.AllProps.Where(p =>
                    p.TargetEnumTypeName == data.TargetEnumTypeName
                ).OrderBy(p => p.CoreKey).ToImmutableArray()
            ));

        context.RegisterSourceOutput(corePropsSource, static (ctx, data) =>
        {
            if (data.PropsOfType.IsEmpty)
                return;
            //if (data.TargetEnumTypeName.Contains("Float") && !Debugger.IsAttached)
            //    Debugger.Launch();
            string? sourceCode = Builders.Phase1.CoreRealmProperties.GenerateCoreEnumClass(data.TargetEnumTypeName, data.PropsOfType);
            ctx.AddSource($"CoreProps/{data.TargetEnumTypeName}.g.cs", SourceText.From(sourceCode, Encoding.UTF8));
        });


        IncrementalValuesProvider<string> entities = combinedProps.SelectMany(static (_, cancellationToken) => EntityToContextEntityMapping.BuildEntityList());
        context.RegisterSourceOutput(entities, static (ctx, entityType) =>
        {
            string sourceCode = EntitySchema.GenerateEntitySchemaSourceCode(entityType);
            ctx.AddSource($"GeneratedJsonSchema/entities/{entityType}.json", SourceText.From(sourceCode, Encoding.UTF8));
        });
    }
}
