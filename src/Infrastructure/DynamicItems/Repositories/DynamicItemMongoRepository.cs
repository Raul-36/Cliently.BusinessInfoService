using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Businesses.Models;
using Core.DynamicItems.DTOs;
using Core.DynamicItems.Models;
using Core.DynamicItems.Options;
using Core.DynamicItems.Repositories.Base;
using Infrastructure.DynamicItems.Helpers;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infrastructure.DynamicItems.Repositories
{
    public class DynamicItemMongoRepository : IDynamicItemRepository
    {
        private readonly IMongoCollection<Business> businessCollection;
        public DynamicItemMongoRepository(IMongoDatabase database, string collectionName)
        {
            this.businessCollection = database.GetCollection<Business>(collectionName);
        }
        public async Task<DynamicItem> AddAsync(DynamicItem DynamicItem, Guid listId)
        {
            var filter = Builders<Business>.Filter.ElemMatch(
                b => b.Lists,
                list => list.Id == listId
            );
            var update = Builders<Business>.Update.Push(
                "Lists.$.Items",
                DynamicItem
            );
            var result = await this.businessCollection.UpdateOneAsync(filter, update);
            if (result.MatchedCount == 0)
                throw new Exception($"List with Id {listId} not found.");

            return DynamicItem;
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            var filter = Builders<Business>.Filter.ElemMatch(
                b => b.Lists,
                list => list.Items.Any(i => i.Id == id)
            );
            var update = Builders<Business>.Update.PullFilter(
                "Lists.$.Items",
                Builders<DynamicItem>.Filter.Eq(i => i.Id, id)
            );
            await this.businessCollection.UpdateOneAsync(filter, update);
        }

        public async Task<IEnumerable<DynamicItemFlexDto>> GetAllByListIdAsync(Guid listId, DynamicItemQueryOptions options)
        {
            var filter = Builders<Business>.Filter.ElemMatch(
                b => b.Lists,
                list => list.Id == listId
            );
            var pipeline = businessCollection.Aggregate()
            .Match(filter)
            .Project(new BsonDocument{
                {"Lists.Items", DynamicItemBsonHelper.ToBsonDocument(options)}
            });

            var rawBusinessAsBsonDoc = await pipeline.FirstOrDefaultAsync();
            if (rawBusinessAsBsonDoc == null)
                throw new KeyNotFoundException($"Lists in business containing List with Id {listId} not found.");
            
            var dynamicItemsAsBsonArray = rawBusinessAsBsonDoc.GetValue("Lists", new BsonArray()).AsBsonArray
                .SelectMany(list => list.AsBsonDocument.GetValue("Items", new BsonArray()).AsBsonArray);
            if (dynamicItemsAsBsonArray.Count() == 0)
                return Enumerable.Empty<DynamicItemFlexDto>();

            var dynamicItems = DynamicItemBsonHelper.ToDynamicItemFlexDto(dynamicItemsAsBsonArray, options);
            return dynamicItems;
        }

        public async Task<DynamicItem> GetByIdAsync(Guid id)
        {
            var filter = Builders<Business>.Filter.ElemMatch(
                b => b.Lists,
                list => list.Items.Any(i => i.Id == id)
            );
            var projection = Builders<Business>.Projection.Expression(b =>
                b.Lists.SelectMany(list => list.Items)
                .FirstOrDefault(i => i.Id == id)
            );
            var result = await this.businessCollection
                .Find(filter)
                .Project(projection)
                .FirstOrDefaultAsync();
            if (result == null)
                throw new KeyNotFoundException($"DynamicItem with Id {id} not found.");
            return result;
        }

        public async Task<DynamicItemFlexDto> GetByIdAsync(Guid id, DynamicItemQueryOptions options)
        {
            var filter = Builders<Business>.Filter.ElemMatch(
                b => b.Lists,
                list => list.Items.Any(i => i.Id == id)
            );
            var pipeline = businessCollection.Aggregate()
            .Match(filter)
            .Project(new BsonDocument{
                {"Lists.Items", DynamicItemBsonHelper.ToBsonDocument(options)}
            });
            var rawBusinessAsBsonDoc = await pipeline.FirstOrDefaultAsync();
            if (rawBusinessAsBsonDoc == null)
                throw new KeyNotFoundException($"Lists in business containing List with DynamicItem Id {id} not found.");
            var dynamicItemsAsBsonArray = rawBusinessAsBsonDoc.GetValue("Lists", new BsonArray()).AsBsonArray
                .Select(list => list.AsBsonDocument.GetValue("Items", new BsonDocument()).AsBsonDocument)
                .Where(item => item.AsBsonDocument.GetValue("id").AsGuid == id);
            if (dynamicItemsAsBsonArray.Count() == 0)
                throw new KeyNotFoundException($"DynamicItem with Id {id} not found.");
            var dtoItemAsBsonDoc = dynamicItemsAsBsonArray.First();
            var dynamicItem = DynamicItemBsonHelper.ToDynamicItemFlexDto(dtoItemAsBsonDoc, options);
            return dynamicItem;
        }

        public Task<DynamicItem> UpdateAsync(DynamicItem DynamicItem)
        {
            var filter = Builders<Business>.Filter.ElemMatch(
                b => b.Lists,
                list => list.Items.Any(i => i.Id == DynamicItem.Id)
            );
            var update = Builders<Business>.Update
                .Set("Lists.$[].Items.$[item]", DynamicItem);
            var result = businessCollection.UpdateOneAsync(filter, update);

            return Task.FromResult(DynamicItem);
        }
    }
}