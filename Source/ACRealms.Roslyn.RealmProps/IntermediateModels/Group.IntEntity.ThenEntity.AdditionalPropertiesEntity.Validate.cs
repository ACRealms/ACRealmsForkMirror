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
    public readonly partial struct IntEntity
    {
        /// <summary>
        /// Generated from JSON Schema.
        /// </summary>
        public readonly partial struct ThenEntity
        {
            /// <summary>
            /// Generated from JSON Schema.
            /// </summary>
            public readonly partial struct AdditionalPropertiesEntity
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

                        result = result.PushSchemaLocation("realm-props/group.json#/definitions/props/definitions/int/then/additionalProperties");
                    }

                    result = CorvusValidation.TernaryIfValidationHandler(this, result, level);

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
                    /// If/then/else composition validation.
                    /// </summary>
                    /// <param name="value">The value to validate.</param>
                    /// <param name="validationContext">The current validation context.</param>
                    /// <param name="level">The current validation level.</param>
                    /// <returns>The resulting validation context after validation.</returns>
                    [MethodImpl(MethodImplOptions.AggressiveInlining)]
                    internal static ValidationContext TernaryIfValidationHandler(
                        in AdditionalPropertiesEntity value,
                        in ValidationContext validationContext,
                        ValidationLevel level = ValidationLevel.Flag)
                    {
                        ValidationContext result = validationContext;

                        if (level > ValidationLevel.Basic)
                        {
                            result = result.PushValidationLocationReducedPathModifier(new("#/if/$ref"));
                        }

                        ValidationContext ifResult = value.As<Corvus.Json.JsonObject>().Validate(validationContext.CreateChildContext(), level);

                        if (!ifResult.IsValid)
                        {
                            if (level >= ValidationLevel.Verbose)
                            {
                                result = validationContext.MergeResults(true, level, ifResult);
                            }
                        }
                        else
                        {
                            if (level >= ValidationLevel.Verbose)
                            {
                                result = result.MergeChildContext(ifResult, true);
                            }
                            else
                            {
                                result = result.MergeChildContext(ifResult, false);
                            }
                        }

                        if (level > ValidationLevel.Basic)
                        {
                            result = result.PopLocation();
                        }

                        if (ifResult.IsValid)
                        {
                            if (level > ValidationLevel.Basic)
                            {
                                result = result.PushValidationLocationReducedPathModifier(new("#/then"));
                            }

                            ValidationContext thenResult = value.As<ACRealms.Roslyn.RealmProps.IntermediateModels.Group.IntEntity.ThenEntity.AdditionalPropertiesEntity.ThenEntity>().Validate(validationContext.CreateChildContext(), level);

                            if (!thenResult.IsValid)
                            {
                                if (level >= ValidationLevel.Basic)
                                {
                                    result = validationContext.MergeResults(false, level, ifResult, thenResult);
                                    result = result.WithResult(isValid: false, "Validation then - failed to validate against the then schema");
                                }
                                else
                                {
                                    result = validationContext.WithResult(isValid: false);
                                }
                            }
                            else
                            {
                                if (level >= ValidationLevel.Basic)
                                {
                                    result = result.MergeChildContext(thenResult, true);
                                }
                                else
                                {
                                    result = result.MergeChildContext(thenResult, false);
                                }
                            }

                            if (level > ValidationLevel.Basic)
                            {
                                result = result.PopLocation();
                            }
                        }

                        return result;
                    }
                }
            }
        }
    }
}
