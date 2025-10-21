using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Businesses;
using Core.Businesses.Models;
using Core.InfoTexts.DTOs;
using Core.InfoTexts.Models;
using Core.InfoTexts.Options;
using Core.InfoTexts.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using MongoDB.Driver;

namespace Infrastructure.InfoTexts.Repositories
{
    public class InfoTextMongoRepository : IInfoTextRepository
    {
        private readonly IMongoCollection<Business> businessCollection;
        public InfoTextMongoRepository(IMongoDatabase database, string collectionName)
        {
            this.businessCollection = database.GetCollection<Business>(collectionName);
        }
        public async Task<InfoText> AddAsync(InfoText infoText, Guid businessId)
        {
            var filter = Builders<Business>.Filter.Eq(b => b.Id, businessId);
            var update = Builders<Business>.Update.Push(b => b.Texts, infoText);

            var result = await businessCollection.UpdateOneAsync(filter, update);

            if (result.MatchedCount == 0)
                throw new Exception($"Business with Id {businessId} not found.");

            return infoText;
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            var filter = Builders<Business>.Filter.ElemMatch(b => b.Texts, t => t.Id == id);
            var update = Builders<Business>.Update.PullFilter(b => b.Texts, t => t.Id == id);

            var result = await businessCollection.UpdateOneAsync(filter, update);
        }

        public async Task<IEnumerable<InfoTextFlexDto>> GetAllByBusinessIdAsync(Guid businessId, InfoTextQueryOptions options)
        {
            var filter = Builders<Business>.Filter.Eq(b => b.Id, businessId);

            var projection = Builders<Business>.Projection.Expression(b =>
                b.Texts.Select(t => new InfoTextFlexDto
                {
                    Id = options.Id ? t.Id : null,
                    Name = options.Name ? t.Name : null,
                    Text = options.Text ? t.Text : null
                })
            );

            var infoTexts = await this.businessCollection
                                     .Find(filter)
                                     .Project(projection)
                                     .FirstOrDefaultAsync();
            if (infoTexts == null)
                throw new KeyNotFoundException($"Texts in business with Id {businessId} not found.");
            return infoTexts ?? Enumerable.Empty<InfoTextFlexDto>();
        }

        public async Task<InfoText> GetByIdAsync(Guid id)
        {
            var filter = Builders<Business>.Filter.ElemMatch(b => b.Texts, t => t.Id == id);

            var projection = Builders<Business>.Projection.Expression(b =>
                b.Texts.FirstOrDefault(t => t.Id == id)
            );

            var result = await businessCollection
                .Find(filter)
                .Project(projection)
                .FirstOrDefaultAsync();

            if (result == null)
                throw new KeyNotFoundException($"InfoText with Id {id} not found.");

            return result;

        }

        public async Task<InfoTextFlexDto> GetByIdAsync(Guid id, InfoTextQueryOptions options)
        {
            var filter = Builders<Business>.Filter.ElemMatch(
                b => b.Texts,
                t => t.Id == id
            );

            var projection = Builders<Business>.Projection.Expression(b =>
                b.Texts
                .Where(t => t.Id == id)
                .Select(t => new InfoTextFlexDto
                {
                    Id = options.Id ? t.Id : null,
                    Name = options.Name ? t.Name : null!,
                    Text = options.Text ? t.Text : null!
                })
                .FirstOrDefault()
            );

            var infoText = await this.businessCollection
                                     .Find(filter)
                                     .Project(projection)
                                     .FirstOrDefaultAsync();

            if (infoText == null)
                throw new KeyNotFoundException($"InfoText with Id {id} not found.");

            return infoText;
        }


        public Task<InfoText> UpdateAsync(InfoText infoText)
        {
            var filter = Builders<Business>.Filter.ElemMatch(b => b.Texts, t => t.Id == infoText.Id);
            var update = Builders<Business>.Update
                .Set(b => "Texts.$.Name", infoText.Name)
                .Set(b => "Texts.$.Text", infoText.Text);

            var result = businessCollection.UpdateOneAsync(filter, update);

            return Task.FromResult(infoText);
        }
    }
}