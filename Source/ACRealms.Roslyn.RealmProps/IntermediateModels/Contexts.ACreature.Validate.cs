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
public readonly partial struct Contexts
{
    /// <summary>
    /// Generated from JSON Schema.
    /// </summary>
    /// <remarks>
    /// <para>
    /// A creature
    /// </para>
    /// </remarks>
    public readonly partial struct ACreature
    {
        /// <summary>
        /// Gets the const instance.
        /// </summary>
        public static ACreature ConstInstance => CorvusValidation.Const.As<ACreature>();

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

                result = result.PushSchemaLocation("contexts.json#/definitions/entities/definitions/Creature");
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
            public static readonly JsonString Const = JsonString.ParseValue("\"Creature\"");

            /// <summary>
            /// A constant for the <c>const</c> keyword.
            /// </summary>
            public static ReadOnlySpan<byte> ConstUtf8 => "\"Creature\""u8;

            /// <summary>
            /// Constant value validation.
            /// </summary>
            /// <param name="value">The value to validate.</param>
            /// <param name="validationContext">The current validation context.</param>
            /// <param name="level">The current validation level.</param>
            /// <returns>The resulting validation context after validation.</returns>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            internal static ValidationContext ConstValidationHandler(
                in ACreature value,
                in ValidationContext validationContext,
                ValidationLevel level = ValidationLevel.Flag)
            {
                return ValidateConst(value, validationContext, level);

                static ValidationContext ValidateConst(
                    in ACreature value,
                    in ValidationContext validationContext,
                    ValidationLevel level = ValidationLevel.Flag)
                {
                    ValidationContext result = validationContext;

                    if (value.Equals(CorvusValidation.Const))
                    {
                        if (level == ValidationLevel.Verbose)
                        {
                            result = result.WithResult(isValid: true, validationLocationReducedPathModifier: new JsonReference("const"), $"Validation const - the value '{value}' matched '\"Creature\'.");
                        }

                        return result;
                    }

                    if (level == ValidationLevel.Flag)
                    {
                        return ValidationContext.InvalidContext;
                    }
                    else if (level >= ValidationLevel.Detailed)
                    {
                        result = result.WithResult(isValid: false, validationLocationReducedPathModifier: new JsonReference("const"), $"Validation const - the value '{value}' did not match '\"Creature\'.");
                    }
                    else
                    {
                        result = result.WithResult(isValid: false, validationLocationReducedPathModifier: new JsonReference("const"), "Validation const - the value did not match '\"Creature\'.");
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
                in ACreature value,
                in ValidationContext validationContext,
                ValidationLevel level = ValidationLevel.Flag)
            {
                ValidationContext result = validationContext;

                return result;
            }
        }
    }
}
