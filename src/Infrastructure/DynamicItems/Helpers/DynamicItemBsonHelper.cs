using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.DynamicItems.DTOs;
using Core.DynamicItems.Options;
using Microsoft.EntityFrameworkCore.Query.Internal;
using MongoDB.Bson;

namespace Infrastructure.DynamicItems.Helpers
{
    public class DynamicItemBsonHelper
    {
        static public BsonDocument ToBsonDocument(DynamicItemQueryOptions options)
        {
            var bsonDoc = new BsonDocument();
            if (options == null)
                throw new ArgumentException("options cannot be null", nameof(options));

            bsonDoc.Add("id", options.Id);
            foreach (var field in options.Properties)
            {
                bsonDoc.Add($"Properties.{field.Key}", field.Value);
            }
            return bsonDoc;
        }
        static public DynamicItemFlexDto ToDynamicItemFlexDto(BsonDocument bsonDoc, DynamicItemQueryOptions options)
        {
            var dto = new DynamicItemFlexDto();
            if (options.Id && bsonDoc.Contains("id"))
                dto.Id = bsonDoc["id"].AsGuid;
            if (options.Properties == null)
                return dto;
            foreach (var field in options.Properties)
            {
                if (bsonDoc.Contains(field.Key))
                {
                    dto.Properties![field.Key] = bsonDoc[field.Key].ToString();
                }
            }
            return dto;
        }
        public static IEnumerable<DynamicItemFlexDto> ToDynamicItemFlexDto(IEnumerable<BsonValue> bsonValues, DynamicItemQueryOptions options)
        {
            var result = new List<DynamicItemFlexDto>();

            foreach (var value in bsonValues)
            {
                if (!value.IsBsonDocument)
                    throw new ArgumentException("BsonValue is not a BsonDocument", nameof(bsonValues));

                var dtoItem = ToDynamicItemFlexDto(value.AsBsonDocument, options);
                result.Add(dtoItem);
            }

            return result;
        }
    }
}