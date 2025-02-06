//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

#nullable enable

using System.Buffers;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text.Json;
using Corvus.Json;
using Corvus.Json.Internal;

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
    public readonly partial struct Extended
    {
        /// <summary>
        /// Generated from JSON Schema.
        /// </summary>
        [System.Text.Json.Serialization.JsonConverter(typeof(Corvus.Json.Internal.JsonValueConverter<PropAttrs>))]
        public readonly partial struct PropAttrs
            : IJsonValue<ACRealms.Roslyn.RealmProps.IntermediateModels.Group.Extended.PropAttrs>
        {
            private readonly Backing backing;
            private readonly JsonElement jsonElementBacking;
            private readonly ImmutableList<JsonObjectProperty> objectBacking;

            /// <summary>
            /// Initializes a new instance of the <see cref="PropAttrs"/> struct.
            /// </summary>
            public PropAttrs()
            {
                this.jsonElementBacking = default;
                this.backing = Backing.JsonElement;
                this.objectBacking = ImmutableList<JsonObjectProperty>.Empty;
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="PropAttrs"/> struct.
            /// </summary>
            /// <param name="value">The value from which to construct the instance.</param>
            public PropAttrs(in JsonElement value)
            {
                this.jsonElementBacking = value;
                this.backing = Backing.JsonElement;
                this.objectBacking = ImmutableList<JsonObjectProperty>.Empty;
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="PropAttrs"/> struct.
            /// </summary>
            /// <param name="value">The value from which to construct the instance.</param>
            public PropAttrs(ImmutableList<JsonObjectProperty> value)
            {
                this.backing = Backing.Object;
                this.jsonElementBacking = default;
                this.objectBacking = value;
            }

            /// <summary>
            /// Gets the schema location from which this type was generated.
            /// </summary>
            public static string SchemaLocation { get; } = "realm-props/group.json#/definitions/groupPropSelection/allOf/1";

            /// <summary>
            /// Gets a Null instance.
            /// </summary>
            public static PropAttrs Null { get; } = new(JsonValueHelpers.NullElement);

            /// <summary>
            /// Gets an Undefined instance.
            /// </summary>
            public static PropAttrs Undefined { get; }

            /// <summary>
            /// Gets the default instance.
            /// </summary>
            public static PropAttrs DefaultInstance { get; }

            /// <inheritdoc/>
            public JsonAny AsAny
            {
                get
                {
                    if ((this.backing & Backing.JsonElement) != 0)
                    {
                        return new(this.jsonElementBacking);
                    }

                    if ((this.backing & Backing.Object) != 0)
                    {
                        return new(this.objectBacking);
                    }

                    if ((this.backing & Backing.Null) != 0)
                    {
                        return JsonAny.Null;
                    }

                    return JsonAny.Undefined;
                }
            }

            /// <inheritdoc/>
            public JsonElement AsJsonElement
            {
                get
                {
                    if ((this.backing & Backing.JsonElement) != 0)
                    {
                        return this.jsonElementBacking;
                    }

                    if ((this.backing & Backing.Object) != 0)
                    {
                        return JsonValueHelpers.ObjectToJsonElement(this.objectBacking);
                    }

                    if ((this.backing & Backing.Null) != 0)
                    {
                        return JsonValueHelpers.NullElement;
                    }

                    return default;
                }
            }

            /// <inheritdoc/>
            JsonString IJsonValue.AsString
            {
                get
                {
                    if ((this.backing & Backing.JsonElement) != 0)
                    {
                        return new(this.jsonElementBacking);
                    }

                    throw new InvalidOperationException();
                }
            }

            /// <inheritdoc/>
            JsonBoolean IJsonValue.AsBoolean
            {
                get
                {
                    if ((this.backing & Backing.JsonElement) != 0)
                    {
                        return new(this.jsonElementBacking);
                    }

                    throw new InvalidOperationException();
                }
            }

            /// <inheritdoc/>
            JsonNumber IJsonValue.AsNumber
            {
                get
                {
                    if ((this.backing & Backing.JsonElement) != 0)
                    {
                        return new(this.jsonElementBacking);
                    }

                    throw new InvalidOperationException();
                }
            }

            /// <inheritdoc/>
            public JsonObject AsObject
            {
                get
                {
                    if ((this.backing & Backing.JsonElement) != 0)
                    {
                        return new(this.jsonElementBacking);
                    }

                    if ((this.backing & Backing.Object) != 0)
                    {
                        return new(this.objectBacking);
                    }

                    throw new InvalidOperationException();
                }
            }

            /// <inheritdoc/>
            JsonArray IJsonValue.AsArray
            {
                get
                {
                    if ((this.backing & Backing.JsonElement) != 0)
                    {
                        return new(this.jsonElementBacking);
                    }

                    throw new InvalidOperationException();
                }
            }

            /// <summary>
            /// Gets the instance as a <see cref="ACRealms.Roslyn.RealmProps.IntermediateModels.Group.PropDefsAreLongForm" />.
            /// </summary>
            public ACRealms.Roslyn.RealmProps.IntermediateModels.Group.PropDefsAreLongForm AsPropDefsAreLongForm
            {
                get
                {
                    return this.As<ACRealms.Roslyn.RealmProps.IntermediateModels.Group.PropDefsAreLongForm>();
                }
            }

            /// <summary>
            /// Gets a value indicating whether the instance is a <see cref="ACRealms.Roslyn.RealmProps.IntermediateModels.Group.PropDefsAreLongForm" />.
            /// </summary>
            public bool IsPropDefsAreLongForm
            {
                get
                {
                    return this.As<ACRealms.Roslyn.RealmProps.IntermediateModels.Group.PropDefsAreLongForm>().IsValid();
                }
            }

            /// <summary>
            /// Gets the instance as a <see cref="ACRealms.Roslyn.RealmProps.IntermediateModels.Group.TypedPropDefs" />.
            /// </summary>
            public ACRealms.Roslyn.RealmProps.IntermediateModels.Group.TypedPropDefs AsTypedPropDefs
            {
                get
                {
                    return this.As<ACRealms.Roslyn.RealmProps.IntermediateModels.Group.TypedPropDefs>();
                }
            }

            /// <summary>
            /// Gets a value indicating whether the instance is a <see cref="ACRealms.Roslyn.RealmProps.IntermediateModels.Group.TypedPropDefs" />.
            /// </summary>
            public bool IsTypedPropDefs
            {
                get
                {
                    return this.As<ACRealms.Roslyn.RealmProps.IntermediateModels.Group.TypedPropDefs>().IsValid();
                }
            }

            /// <inheritdoc/>
            public bool HasJsonElementBacking
            {
                get
                {
                    return (this.backing & Backing.JsonElement) != 0;
                }
            }

            /// <inheritdoc/>
            public bool HasDotnetBacking
            {
                get
                {
                    return (this.backing & Backing.Dotnet) != 0;
                }
            }

            /// <inheritdoc/>
            public JsonValueKind ValueKind
            {
                get
                {
                    if ((this.backing & Backing.JsonElement) != 0)
                    {
                        return this.jsonElementBacking.ValueKind;
                    }

                    if ((this.backing & Backing.Object) != 0)
                    {
                        return JsonValueKind.Object;
                    }

                    return JsonValueKind.Undefined;
                }
            }

            /// <summary>
            /// Conversion from JsonAny.
            /// </summary>
            /// <param name="value">The value from which to convert.</param>
            public static implicit operator PropAttrs(JsonAny value)
            {
                return value.As<PropAttrs>();
            }

            /// <summary>
            /// Conversion to JsonAny.
            /// </summary>
            /// <param name="value">The value from which to convert.</param>
            public static implicit operator JsonAny(PropAttrs value)
            {
                return value.AsAny;
            }

            /// <summary>
            /// Operator ==.
            /// </summary>
            /// <param name="left">The lhs of the operator.</param>
            /// <param name="right">The rhs of the operator.</param>
            /// <returns>
            /// <c>True</c> if the values are equal.
            /// </returns>
            public static bool operator ==(in PropAttrs left, in PropAttrs right)
            {
                return left.Equals(right);
            }

            /// <summary>
            /// Operator !=.
            /// </summary>
            /// <param name="left">The lhs of the operator.</param>
            /// <param name="right">The rhs of the operator.</param>
            /// <returns>
            /// <c>True</c> if the values are not equal.
            /// </returns>
            public static bool operator !=(in PropAttrs left, in PropAttrs right)
            {
                return !left.Equals(right);
            }

            /// <summary>
            /// Gets an instance of the JSON value from a <see cref="JsonElement"/> value.
            /// </summary>
            /// <param name="value">The <see cref="JsonElement"/> value from which to instantiate the instance.</param>
            /// <returns>An instance of this type, initialized from the <see cref="JsonElement"/>.</returns>
            /// <remarks>The returned value will have a <see cref = "IJsonValue.ValueKind"/> of <see cref = "JsonValueKind.Undefined"/> if the
            /// value cannot be constructed from the given instance (e.g. because they have an incompatible .NET backing type).
            /// </remarks>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static PropAttrs FromJson(in JsonElement value)
            {
                return new(value);
            }

            /// <summary>
            /// Gets an instance of the JSON value from a <see cref="JsonAny"/> value.
            /// </summary>
            /// <param name="value">The <see cref="JsonAny"/> value from which to instantiate the instance.</param>
            /// <returns>An instance of this type, initialized from the <see cref="JsonAny"/> value.</returns>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static PropAttrs FromAny(in JsonAny value)
            {
                if (value.HasJsonElementBacking)
                {
                    return new(value.AsJsonElement);
                }

                return value.ValueKind switch
                {
                    JsonValueKind.Object => new(value.AsObject.AsPropertyBacking()),
                    JsonValueKind.Null => Null,
                    _ => Undefined,
                };
            }

#if NET8_0_OR_GREATER
            /// <summary>
            /// Gets an instance of the JSON value from the provided value.
            /// </summary>
            /// <typeparam name="TValue">The type of the value.</typeparam>
            /// <param name="value">The value from which to instantiate the instance.</param>
            /// <returns>An instance of this type, initialized from the provided value.</returns>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            static PropAttrs IJsonValue<PropAttrs>.FromBoolean<TValue>(in TValue value)
            {
                if (value.HasJsonElementBacking)
                {
                    return new(value.AsJsonElement);
                }

                return Undefined;
            }
#endif

#if NET8_0_OR_GREATER
            /// <summary>
            /// Gets an instance of the JSON value from the provided value.
            /// </summary>
            /// <typeparam name="TValue">The type of the value.</typeparam>
            /// <param name="value">The value from which to instantiate the instance.</param>
            /// <returns>An instance of this type, initialized from the provided value.</returns>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            static PropAttrs IJsonValue<PropAttrs>.FromString<TValue>(in TValue value)
            {
                if (value.HasJsonElementBacking)
                {
                    return new(value.AsJsonElement);
                }

                return Undefined;
            }
#endif

#if NET8_0_OR_GREATER
            /// <summary>
            /// Gets an instance of the JSON value from the provided value.
            /// </summary>
            /// <typeparam name="TValue">The type of the value.</typeparam>
            /// <param name="value">The value from which to instantiate the instance.</param>
            /// <returns>An instance of this type, initialized from the provided value.</returns>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            static PropAttrs IJsonValue<PropAttrs>.FromNumber<TValue>(in TValue value)
            {
                if (value.HasJsonElementBacking)
                {
                    return new(value.AsJsonElement);
                }

                return Undefined;
            }
#endif

            /// <summary>
            /// Gets an instance of the JSON value from the provided value.
            /// </summary>
            /// <typeparam name="TValue">The type of the value.</typeparam>
            /// <param name="value">The value from which to instantiate the instance.</param>
            /// <returns>An instance of this type, initialized from the provided value.</returns>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static PropAttrs FromObject<TValue>(in TValue value)
                where TValue : struct, IJsonObject<TValue>
            {
                if (value.HasJsonElementBacking)
                {
                    return new(value.AsJsonElement);
                }

                return value.ValueKind switch
                {
                    JsonValueKind.Object => new(value.AsPropertyBacking()),
                    JsonValueKind.Null => Null,
                    _ => Undefined,
                };
            }

#if NET8_0_OR_GREATER
            /// <summary>
            /// Gets an instance of the JSON value from the provided value.
            /// </summary>
            /// <typeparam name="TValue">The type of the value.</typeparam>
            /// <param name="value">The value from which to instantiate the instance.</param>
            /// <returns>An instance of this type, initialized from the provided value.</returns>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            static PropAttrs IJsonValue<PropAttrs>.FromArray<TValue>(in TValue value)
            {
                if (value.HasJsonElementBacking)
                {
                    return new(value.AsJsonElement);
                }

                return Undefined;
            }
#endif

            /// <summary>
            /// Parses the PropAttrs.
            /// </summary>
            /// <param name="source">The source of the JSON string to parse.</param>
            /// <param name="options">The (optional) JsonDocumentOptions.</param>
            public static PropAttrs Parse(string source, JsonDocumentOptions options = default)
            {
                using var jsonDocument = JsonDocument.Parse(source, options);
                return new(jsonDocument.RootElement.Clone());
            }

            /// <summary>
            /// Parses the PropAttrs.
            /// </summary>
            /// <param name="source">The source of the JSON string to parse.</param>
            /// <param name="options">The (optional) JsonDocumentOptions.</param>
            public static PropAttrs Parse(Stream source, JsonDocumentOptions options = default)
            {
                using var jsonDocument = JsonDocument.Parse(source, options);
                return new(jsonDocument.RootElement.Clone());
            }

            /// <summary>
            /// Parses the PropAttrs.
            /// </summary>
            /// <param name="source">The source of the JSON string to parse.</param>
            /// <param name="options">The (optional) JsonDocumentOptions.</param>
            public static PropAttrs Parse(ReadOnlyMemory<byte> source, JsonDocumentOptions options = default)
            {
                using var jsonDocument = JsonDocument.Parse(source, options);
                return new(jsonDocument.RootElement.Clone());
            }

            /// <summary>
            /// Parses the PropAttrs.
            /// </summary>
            /// <param name="source">The source of the JSON string to parse.</param>
            /// <param name="options">The (optional) JsonDocumentOptions.</param>
            public static PropAttrs Parse(ReadOnlyMemory<char> source, JsonDocumentOptions options = default)
            {
                using var jsonDocument = JsonDocument.Parse(source, options);
                return new(jsonDocument.RootElement.Clone());
            }

            /// <summary>
            /// Parses the PropAttrs.
            /// </summary>
            /// <param name="source">The source of the JSON string to parse.</param>
            /// <param name="options">The (optional) JsonDocumentOptions.</param>
            public static PropAttrs Parse(ReadOnlySequence<byte> source, JsonDocumentOptions options = default)
            {
                using var jsonDocument = JsonDocument.Parse(source, options);
                return new(jsonDocument.RootElement.Clone());
            }

            /// <summary>
            /// Parses the PropAttrs.
            /// </summary>
            /// <param name="source">The source of the JSON string to parse.</param>
            public static PropAttrs ParseValue(string source)
            {
#if NET8_0_OR_GREATER
                return IJsonValue<PropAttrs>.ParseValue(source);
#else
                return JsonValueHelpers.ParseValue<PropAttrs>(source.AsSpan());
#endif
            }

            /// <summary>
            /// Parses the PropAttrs.
            /// </summary>
            /// <param name="source">The source of the JSON string to parse.</param>
            public static PropAttrs ParseValue(ReadOnlySpan<char> source)
            {
#if NET8_0_OR_GREATER
                return IJsonValue<PropAttrs>.ParseValue(source);
#else
                return JsonValueHelpers.ParseValue<PropAttrs>(source);
#endif
            }

            /// <summary>
            /// Parses the PropAttrs.
            /// </summary>
            /// <param name="source">The source of the JSON string to parse.</param>
            public static PropAttrs ParseValue(ReadOnlySpan<byte> source)
            {
#if NET8_0_OR_GREATER
                return IJsonValue<PropAttrs>.ParseValue(source);
#else
                return JsonValueHelpers.ParseValue<PropAttrs>(source);
#endif
            }

            /// <summary>
            /// Parses the PropAttrs.
            /// </summary>
            /// <param name="source">The source of the JSON string to parse.</param>
            public static PropAttrs ParseValue(ref Utf8JsonReader source)
            {
#if NET8_0_OR_GREATER
                return IJsonValue<PropAttrs>.ParseValue(ref source);
#else
                return JsonValueHelpers.ParseValue<PropAttrs>(ref source);
#endif
            }

            /// <summary>
            /// Gets the value as an instance of the target value.
            /// </summary>
            /// <typeparam name="TTarget">The type of the target.</typeparam>
            /// <returns>An instance of the target type.</returns>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public TTarget As<TTarget>()
                where TTarget : struct, IJsonValue<TTarget>
            {
#if NET8_0_OR_GREATER
                if ((this.backing & Backing.JsonElement) != 0)
                {
                    return TTarget.FromJson(this.jsonElementBacking);
                }

                if ((this.backing & Backing.Object) != 0)
                {
                    return TTarget.FromObject(this.AsObject);
                }

                if ((this.backing & Backing.Null) != 0)
                {
                    return TTarget.Null;
                }

                return TTarget.Undefined;
#else
                return this.As<PropAttrs, TTarget>();
#endif
            }

            /// <inheritdoc/>
            public override bool Equals(object? obj)
            {
                return
                    (obj is IJsonValue jv && this.Equals(jv.As<PropAttrs>())) ||
                    (obj is null && this.IsNull());
            }

            /// <inheritdoc/>
            public bool Equals<T>(in T other)
                where T : struct, IJsonValue<T>
            {
                return this.Equals(other.As<PropAttrs>());
            }

            /// <summary>
            /// Equality comparison.
            /// </summary>
            /// <param name="other">The other item with which to compare.</param>
            /// <returns><see langword="true"/> if the values were equal.</returns>
            public bool Equals(in PropAttrs other)
            {
                JsonValueKind thisKind = this.ValueKind;
                JsonValueKind otherKind = other.ValueKind;
                if (thisKind != otherKind)
                {
                    return false;
                }

                if (thisKind == JsonValueKind.Null || thisKind == JsonValueKind.Undefined)
                {
                    return true;
                }

                if (thisKind == JsonValueKind.Object)
                {
                    JsonObject thisObject = this.AsObject;
                    JsonObject otherObject = other.AsObject;
                    int count = 0;
                    foreach (JsonObjectProperty property in thisObject.EnumerateObject())
                    {
                        if (!otherObject.TryGetProperty(property.Name, out JsonAny value) || !property.Value.Equals(value))
                        {
                            return false;
                        }

                        count++;
                    }

                    int otherCount = 0;
                    foreach (JsonObjectProperty otherProperty in otherObject.EnumerateObject())
                    {
                        otherCount++;
                        if (otherCount > count)
                        {
                            return false;
                        }
                    }

                    return count == otherCount;
                }

                return false;
            }

            /// <inheritdoc/>
            public void WriteTo(Utf8JsonWriter writer)
            {
                if ((this.backing & Backing.JsonElement) != 0)
                {
                    if (this.jsonElementBacking.ValueKind != JsonValueKind.Undefined)
                    {
                        this.jsonElementBacking.WriteTo(writer);
                    }

                    return;
                }

                if ((this.backing & Backing.Object) != 0)
                {
                    JsonValueHelpers.WriteProperties(this.objectBacking, writer);

                    return;
                }

                if ((this.backing & Backing.Null) != 0)
                {
                    writer.WriteNullValue();

                    return;
                }
            }

            /// <inheritdoc/>
            public override int GetHashCode()
            {
                return this.ValueKind switch
                {
                    JsonValueKind.Array => JsonValueHelpers.GetArrayHashCode(((IJsonValue)this).AsArray),
                    JsonValueKind.Object => JsonValueHelpers.GetObjectHashCode(this),
                    JsonValueKind.Number => JsonValueHelpers.GetHashCodeForNumber(((IJsonValue)this).AsNumber),
                    JsonValueKind.String => JsonValueHelpers.GetHashCodeForString(((IJsonValue)this).AsString),
                    JsonValueKind.True => true.GetHashCode(),
                    JsonValueKind.False => false.GetHashCode(),
                    JsonValueKind.Null => JsonValueHelpers.NullHashCode,
                    _ => JsonValueHelpers.UndefinedHashCode,
                };
            }

            /// <inheritdoc/>
            public override string ToString()
            {
                return this.Serialize();
            }

            /// <summary>
            /// Matches the value against the 'if' type, and returns the result of calling the provided match function for
            /// the 'then' type if the match is successful.
            /// </summary>
            /// <typeparam name="TIn">The immutable context to pass in to the match function.</typeparam>
            /// <typeparam name="TOut">The result of calling the match function.</typeparam>
            /// <param name="context">The context to pass to the match function.</param>
            /// <param name="matchAcRealmsRoslynRealmPropsIntermediateModelsGroupTypedPropDefs">Match a <see cref="ACRealms.Roslyn.RealmProps.IntermediateModels.Group.TypedPropDefs"/>.</param>
            /// <param name="defaultMatch">Default match if the 'if' schema did not match.</param>
            /// <returns>An instance of the value returned by the match function.</returns>
            public TOut Match<TIn, TOut>(
                in TIn context,
                Matcher<ACRealms.Roslyn.RealmProps.IntermediateModels.Group.TypedPropDefs, TIn, TOut> matchAcRealmsRoslynRealmPropsIntermediateModelsGroupTypedPropDefs,
                Matcher<PropAttrs, TIn, TOut> defaultMatch)
            {
                ACRealms.Roslyn.RealmProps.IntermediateModels.Group.PropDefsAreLongForm ifValue = this.As<ACRealms.Roslyn.RealmProps.IntermediateModels.Group.PropDefsAreLongForm>();
                if (ifValue.IsValid())
                {
                    return matchAcRealmsRoslynRealmPropsIntermediateModelsGroupTypedPropDefs(this.As<ACRealms.Roslyn.RealmProps.IntermediateModels.Group.TypedPropDefs>(), context);
                }

                return defaultMatch(this, context);
            }

            /// <summary>
            /// Matches the value against the 'if' type, and returns the result of calling the provided match function for
            /// the 'then' type if the match is successful.
            /// </summary>
            /// <typeparam name="TOut">The result of calling the match function.</typeparam>
            /// <param name="matchAcRealmsRoslynRealmPropsIntermediateModelsGroupTypedPropDefs">Match a <see cref="ACRealms.Roslyn.RealmProps.IntermediateModels.Group.TypedPropDefs"/>.</param>
            /// <param name="defaultMatch">Default match if the 'if' schema did not match.</param>
            /// <returns>An instance of the value returned by the match function.</returns>
            public TOut Match<TOut>(
                Matcher<ACRealms.Roslyn.RealmProps.IntermediateModels.Group.TypedPropDefs, TOut> matchAcRealmsRoslynRealmPropsIntermediateModelsGroupTypedPropDefs,
                Matcher<PropAttrs, TOut> defaultMatch)
            {
                ACRealms.Roslyn.RealmProps.IntermediateModels.Group.PropDefsAreLongForm ifValue = this.As<ACRealms.Roslyn.RealmProps.IntermediateModels.Group.PropDefsAreLongForm>();
                if (ifValue.IsValid())
                {
                    return matchAcRealmsRoslynRealmPropsIntermediateModelsGroupTypedPropDefs(this.As<ACRealms.Roslyn.RealmProps.IntermediateModels.Group.TypedPropDefs>());
                }

                return defaultMatch(this);
            }

            /// <summary>
            /// Gets the value as a <see cref="ACRealms.Roslyn.RealmProps.IntermediateModels.Group.PropDefsAreLongForm" />.
            /// </summary>
            /// <param name="result">The result of the conversions.</param>
            /// <returns><see langword="true" /> if the conversion was valid.</returns>
            public bool TryGetAsPropDefsAreLongForm(out ACRealms.Roslyn.RealmProps.IntermediateModels.Group.PropDefsAreLongForm result)
            {
                result = this.As<ACRealms.Roslyn.RealmProps.IntermediateModels.Group.PropDefsAreLongForm>();
                return result.IsValid();
            }

            /// <summary>
            /// Gets the value as a <see cref="ACRealms.Roslyn.RealmProps.IntermediateModels.Group.TypedPropDefs" />.
            /// </summary>
            /// <param name="result">The result of the conversions.</param>
            /// <returns><see langword="true" /> if the conversion was valid.</returns>
            public bool TryGetAsTypedPropDefs(out ACRealms.Roslyn.RealmProps.IntermediateModels.Group.TypedPropDefs result)
            {
                result = this.As<ACRealms.Roslyn.RealmProps.IntermediateModels.Group.TypedPropDefs>();
                return result.IsValid();
            }
        }
    }
}
