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
public readonly partial struct PropDefExtensionMinMax
{
    /// <summary>
    /// Generated from JSON Schema.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Restricts the property from randomizing to **At Most** the frequency specified here. For no restrictions, omit this attribute
    /// </para>
    /// </remarks>
    public readonly partial struct RerollRestrictedToEntity
    {
        /// <summary>
        /// Generated from JSON Schema.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Disables randomization (uses the default value)
        /// </para>
        /// </remarks>
        public readonly partial struct Never
        {
            /// <summary>
            /// Gets the const instance.
            /// </summary>
            public static Never ConstInstance => CorvusValidation.Const.As<Never>();

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

                    result = result.PushSchemaLocation("realm-props/common/prop-minmax.json#/properties/reroll_restricted_to/oneOf/0");
                }

                JsonValueKind valueKind = this.ValueKind;

                result = CorvusValidation.TypeValidationHandler(valueKind, result, level);

                if (level == ValidationLevel.Flag && !result.IsValid)
                {
                    return result;
                }

                result = CorvusValidation.ConstValidationHandler(this, result, level);

                if (level == ValidationLevel.Flag && !result.IsValid)
                {
                    return result;
                }

                result = CorvusValidation.CompositionAnyOfValidationHandler(this, result, level);

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
                /// A constant for the <c>const</c> keyword.
                /// </summary>
                public static readonly JsonString Const = JsonString.ParseValue("\"never\"");

                /// <summary>
                /// A constant for the <c>const</c> keyword.
                /// </summary>
                public static ReadOnlySpan<byte> ConstUtf8 => "\"never\""u8;

                /// <summary>
                /// Core type validation.
                /// </summary>
                /// <param name="valueKind">The <see cref="JsonValueKind" /> of the value to validate.</param>
                /// <param name="validationContext">The current validation context.</param>
                /// <param name="level">The current validation level.</param>
                /// <returns>The resulting validation context after validation.</returns>
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                internal static ValidationContext TypeValidationHandler(
                    JsonValueKind valueKind,
                    in ValidationContext validationContext,
                    ValidationLevel level = ValidationLevel.Flag)
                {
                    return Corvus.Json.ValidateWithoutCoreType.TypeString(valueKind, validationContext, level, "type");
                }

                /// <summary>
                /// Constant value validation.
                /// </summary>
                /// <param name="value">The value to validate.</param>
                /// <param name="validationContext">The current validation context.</param>
                /// <param name="level">The current validation level.</param>
                /// <returns>The resulting validation context after validation.</returns>
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                internal static ValidationContext ConstValidationHandler(
                    in Never value,
                    in ValidationContext validationContext,
                    ValidationLevel level = ValidationLevel.Flag)
                {
                    return ValidateConst(value, validationContext, level);

                    static ValidationContext ValidateConst(
                        in Never value,
                        in ValidationContext validationContext,
                        ValidationLevel level = ValidationLevel.Flag)
                    {
                        ValidationContext result = validationContext;

                        if (value.Equals(CorvusValidation.Const))
                        {
                            if (level == ValidationLevel.Verbose)
                            {
                                result = result.WithResult(isValid: true, validationLocationReducedPathModifier: new JsonReference("const"), $"Validation const - the value '{value}' matched '\"never\'.");
                            }

                            return result;
                        }

                        if (level == ValidationLevel.Flag)
                        {
                            return ValidationContext.InvalidContext;
                        }
                        else if (level >= ValidationLevel.Detailed)
                        {
                            result = result.WithResult(isValid: false, validationLocationReducedPathModifier: new JsonReference("const"), $"Validation const - the value '{value}' did not match '\"never\'.");
                        }
                        else
                        {
                            result = result.WithResult(isValid: false, validationLocationReducedPathModifier: new JsonReference("const"), "Validation const - the value did not match '\"never\'.");
                        }

                        return result;
                    }
                }

                /// <summary>
                /// Composition validation (any-of).
                /// </summary>
                /// <param name="value">The value to validate.</param>
                /// <param name="validationContext">The current validation context.</param>
                /// <param name="level">The current validation level.</param>
                /// <returns>The resulting validation context after validation.</returns>
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                internal static ValidationContext CompositionAnyOfValidationHandler(
                    in Never value,
                    in ValidationContext validationContext,
                    ValidationLevel level = ValidationLevel.Flag)
                {
                    ValidationContext result = validationContext;

                    return result;
                }
            }
        }
    }
}
