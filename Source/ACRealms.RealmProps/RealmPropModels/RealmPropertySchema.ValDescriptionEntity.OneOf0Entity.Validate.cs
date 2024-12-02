//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
#nullable enable
using System.Text.Json;
using Corvus.Json;

namespace ACRealms.RealmProps.IntermediateModels;
public readonly partial struct RealmPropertySchema
{
    public readonly partial struct ValDescriptionEntity
    {
        /// <summary>
        /// Generated from JSON Schema.
        /// </summary>
        public readonly partial struct OneOf0Entity
        {
            /// <inheritdoc/>
            public ValidationContext Validate(in ValidationContext validationContext, ValidationLevel level = ValidationLevel.Flag)
            {
                ValidationContext result = validationContext;
                if (level > ValidationLevel.Flag)
                {
                    result = result.UsingResults();
                }

                if (level > ValidationLevel.Basic)
                {
                    result = result.UsingStack();
                    result = result.PushSchemaLocation("https://realm.ac/schema/v1/realm-property-schema.json#/definitions/valDescription/oneOf/0");
                }

                result = this.ValidateRef(result, level);
                if (level == ValidationLevel.Flag && !result.IsValid)
                {
                    return result;
                }

                result = this.ValidateAllOf(result, level);
                if (level == ValidationLevel.Flag && !result.IsValid)
                {
                    return result;
                }

                if (level != ValidationLevel.Flag)
                {
                    result = result.PopLocation();
                }

                return result;
            }
        }
    }
}