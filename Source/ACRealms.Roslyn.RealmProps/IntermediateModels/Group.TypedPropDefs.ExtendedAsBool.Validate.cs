//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

#nullable enable

using System.Runtime.CompilerServices;
using System.Text.Json;
using Corvus.Json;

namespace ACRealms.Roslyn.RealmProps.IntermediateModels;

/// <summary>
/// Generated from JSON Schema.
/// </summary>
/// <remarks>
/// <para>
/// Examples:
/// <example>
/// <code>
/// { }
/// </code>
/// </example>
/// </para>
/// </remarks>
public readonly partial struct Group
{
    /// <summary>
    /// Generated from JSON Schema.
    /// </summary>
    public readonly partial struct TypedPropDefs
    {
        /// <summary>
        /// Generated from JSON Schema.
        /// </summary>
        public readonly partial struct ExtendedAsBool
        {
            /// <inheritdoc/>
            public ValidationContext Validate(in ValidationContext validationContext, ValidationLevel level = ValidationLevel.Flag)
            {
                ValidationContext result = validationContext;
                if (level > ValidationLevel.Flag && !result.IsUsingResults)
                {
                    result = result.UsingResults();
                }

                if (level > ValidationLevel.Basic)
                {
                    if (!result.IsUsingStack)
                    {
                        result = result.UsingStack();
                    }

                    result = result.PushSchemaLocation("realm-props/group.json#/definitions/typedPropDefs/definitions/bool");
                }

                result = CorvusValidation.CompositionAllOfValidationHandler(this, result, level);

                if (level == ValidationLevel.Flag && !result.IsValid)
                {
                    return result;
                }

                if (level > ValidationLevel.Basic)
                {
                    result = result.PopLocation();
                }

                return result;
            }

            /// <summary>
            /// Validation constants for the type.
            /// </summary>
            public static partial class CorvusValidation
            {
                /// <summary>
                /// Composition validation (all-of).
                /// </summary>
                /// <param name="value">The value to validate.</param>
                /// <param name="validationContext">The current validation context.</param>
                /// <param name="level">The current validation level.</param>
                /// <returns>The resulting validation context after validation.</returns>
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                internal static ValidationContext CompositionAllOfValidationHandler(
                    in ExtendedAsBool value,
                    in ValidationContext validationContext,
                    ValidationLevel level = ValidationLevel.Flag)
                {
                    ValidationContext result = validationContext;
                    ValidationContext childContextBase = result;

                    ValidationContext allOfResult0 = childContextBase.CreateChildContext();
                    if (level > ValidationLevel.Basic)
                    {
                        allOfResult0 = allOfResult0.PushValidationLocationReducedPathModifier(new("#/allOf/0"));
                    }

                    allOfResult0 = value.As<ACRealms.Roslyn.RealmProps.IntermediateModels.Group.TypedPropDefs.ExtendedAsBool.AllOf0Entity>().Validate(allOfResult0, level);

                    if (!allOfResult0.IsValid)
                    {
                        if (level >= ValidationLevel.Basic)
                        {
                            result = result.MergeChildContext(allOfResult0, true).PushValidationLocationProperty("allOf").WithResult(isValid: false, "Validation - allOf failed to validate against the schema.").PopLocation();
                        }
                        else
                        {
                            result = result.MergeChildContext(allOfResult0, false).WithResult(isValid: false);
                            return result;
                        }
                    }
                    else
                    {
                        result = result.MergeChildContext(allOfResult0, level >= ValidationLevel.Detailed);
                    }

                    return result;
                }
            }
        }
    }
}
