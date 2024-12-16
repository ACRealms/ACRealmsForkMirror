//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

#nullable enable

using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using Corvus.Json;
using Corvus.Json.Internal;

namespace ACRealms.Roslyn.RealmProps.IntermediateModels;

/// <summary>
/// Generated from JSON Schema.
/// </summary>
public readonly partial struct Contexts
{
    /// <summary>
    /// Generated from JSON Schema.
    /// </summary>
    public readonly partial struct ContextObj
    {
        /// <summary>
        /// Generated from JSON Schema.
        /// </summary>
        /// <remarks>
        /// <para>
        /// If true, a context is required when fetching this realm property.
        /// </para>
        /// <para>
        /// Examples:
        /// <example>
        /// <code>
        /// true
        /// </code>
        /// </example>
        /// </para>
        /// </remarks>
        public readonly partial struct RequiredEntity
            : IJsonBoolean<ACRealms.Roslyn.RealmProps.IntermediateModels.Contexts.ContextObj.RequiredEntity>
        {
            /// <summary>
            /// Conversion from <see cref="bool"/>.
            /// </summary>
            /// <param name="value">The value from which to convert.</param>
            public static implicit operator RequiredEntity(bool value)
            {
                return new(value);
            }

            /// <summary>
            /// Conversion from JsonBoolean.
            /// </summary>
            /// <param name="value">The value from which to convert.</param>
            public static implicit operator RequiredEntity(JsonBoolean value)
            {
                if (value.HasDotnetBacking && (value.ValueKind == JsonValueKind.False || value.ValueKind == JsonValueKind.True))
                {
                    return new(
                        (bool)value);
                }

                return new(value.AsJsonElement);
            }

            /// <summary>
            /// Conversion to JsonBoolean.
            /// </summary>
            /// <param name="value">The value from which to convert.</param>
            public static implicit operator JsonBoolean(RequiredEntity value)
            {
                return
                    value.AsBoolean;
            }

            /// <summary>
            /// Conversion to <see langword="bool"/>.
            /// </summary>
            /// <param name="value">The value from which to convert.</param>
            /// <exception cref="InvalidOperationException">The value was not a boolean.</exception>
            public static implicit operator bool(RequiredEntity value)
            {
                return value.GetBoolean() ?? throw new InvalidOperationException();
            }

            /// <summary>
            /// Try to retrieve the value as a boolean.
            /// </summary>
            /// <param name="result"><see langword="true"/> if the value was true, otherwise <see langword="false"/>.</param>
            /// <returns><see langword="true"/> if the value was representable as a boolean, otherwise <see langword="false"/>.</returns>
            public bool TryGetBoolean([NotNullWhen(true)] out bool result)
            {
                switch (this.ValueKind)
                {
                    case JsonValueKind.True:
                        result = true;
                        return true;
                    case JsonValueKind.False:
                        result = false;
                        return true;
                    default:
                        result = default;
                        return false;
                }
            }

            /// <summary>
            /// Get the value as a boolean.
            /// </summary>
            /// <returns>The value of the boolean, or <see langword="null"/> if the value was not representable as a boolean.</returns>
            public bool? GetBoolean()
            {
                if (this.TryGetBoolean(out bool result))
                {
                    return result;
                }

                return null;
            }
        }
    }
}
