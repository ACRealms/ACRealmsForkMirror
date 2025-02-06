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
public readonly partial struct PropBool
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

            result = result.PushSchemaLocation("realm-props/common/prop-typed.json#/definitions/propBool");
        }

        JsonValueKind valueKind = this.ValueKind;

        result = CorvusValidation.CompositionAllOfValidationHandler(this, result, level);

        if (level == ValidationLevel.Flag && !result.IsValid)
        {
            return result;
        }

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
        /// Composition validation (all-of).
        /// </summary>
        /// <param name="value">The value to validate.</param>
        /// <param name="validationContext">The current validation context.</param>
        /// <param name="level">The current validation level.</param>
        /// <returns>The resulting validation context after validation.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static ValidationContext CompositionAllOfValidationHandler(
            in PropBool value,
            in ValidationContext validationContext,
            ValidationLevel level = ValidationLevel.Flag)
        {
            ValidationContext result = validationContext;
            ValidationContext childContextBase = result;

            ValidationContext allOfResult0 = childContextBase.CreateChildContext();
            if (level > ValidationLevel.Basic)
            {
                allOfResult0 = allOfResult0.PushValidationLocationReducedPathModifier(new("#/allOf/0/$ref"));
            }

            allOfResult0 = value.As<ACRealms.Roslyn.RealmProps.IntermediateModels.PropBase>().Validate(allOfResult0, level);

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
            in PropBool value,
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
                if (property.NameEquals(JsonPropertyNames.DefaultUtf8, JsonPropertyNames.Default))
                {
                    result = result.WithLocalProperty(propertyCount);
                    if (level > ValidationLevel.Basic)
                    {
                        result = result.PushValidationLocationReducedPathModifierAndProperty(new("#/properties/default/$ref"), JsonPropertyNames.Default);
                    }

                    ValidationContext propertyResult = property.Value.As<ACRealms.Roslyn.RealmProps.IntermediateModels.ValBool>().Validate(result.CreateChildContext(), level);
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
