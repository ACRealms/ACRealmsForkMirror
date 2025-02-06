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
/// An object representing the realm property definition&#39;s attributes
/// </para>
/// </remarks>
public readonly partial struct PropObj
{
    /// <summary>
    /// Generated from JSON Schema.
    /// </summary>
    public readonly partial struct ExtendedUntypedAttrs
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

                result = result.PushSchemaLocation("realm-props/group/prop-obj.json#/allOf/0");
            }

            JsonValueKind valueKind = this.ValueKind;

            result = CorvusValidation.ObjectValidationHandler(this, valueKind, result, level);

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
            /// Object validation.
            /// </summary>
            /// <param name="value">The value to validate.</param>
            /// <param name="valueKind">The <see cref="JsonValueKind" /> of the value to validate.</param>
            /// <param name="validationContext">The current validation context.</param>
            /// <param name="level">The current validation level.</param>
            /// <returns>The resulting validation context after validation.</returns>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            internal static ValidationContext ObjectValidationHandler(
                in ExtendedUntypedAttrs value,
                JsonValueKind valueKind,
                in ValidationContext validationContext,
                ValidationLevel level = ValidationLevel.Flag)
            {
                ValidationContext result = validationContext;
                if (valueKind != JsonValueKind.Object)
                {
                    if (level == ValidationLevel.Verbose)
                    {
                        ValidationContext ignoredResult = validationContext;
                        ignoredResult = ignoredResult.WithResult(isValid: true, "Validation properties - ignored because the value is not an object", "properties");

                        return ignoredResult;
                    }

                    return validationContext;
                }

                int propertyCount = 0;
                foreach (JsonObjectProperty property in value.EnumerateObject())
                {
                    if (property.NameEquals(JsonPropertyNames.DescriptionUtf8, JsonPropertyNames.Description))
                    {
                        result = result.WithLocalProperty(propertyCount);
                        if (level > ValidationLevel.Basic)
                        {
                            result = result.PushValidationLocationReducedPathModifierAndProperty(new("#/properties/description/$ref"), JsonPropertyNames.Description);
                        }

                        ValidationContext propertyResult = property.Value.As<ACRealms.Roslyn.RealmProps.IntermediateModels.Description>().Validate(result.CreateChildContext(), level);
                        if (level == ValidationLevel.Flag && !propertyResult.IsValid)
                        {
                            return propertyResult;
                        }

                        result = result.MergeResults(propertyResult.IsValid, level, propertyResult);

                        if (level > ValidationLevel.Basic)
                        {
                            result = result.PopLocation();
                        }
                    }

                    propertyCount++;
                }

                return result;
            }
        }
    }
}
