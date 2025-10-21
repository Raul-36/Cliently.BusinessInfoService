using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
        public InfoTextMongoRepository(IMapper mapper, IMongoDatabase database, string collectionName)
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
            var business = await businessCollection.AsQueryable()
                .FirstOrDefaultAsync(b => b.Id == businessId);
            if (business == null)
                throw new Exception($"Business with Id {businessId} not found.");

            return business.Texts.Select(it => new InfoTextFlexDto
            {
                Id = options.Id ? it.Id : null,
                Name = options.Name ? it.Name : null,
                Text = options.Text ? it.Text : null
            });
        }

        public async Task<InfoText> GetByIdAsync(Guid id)
        {
            var businessWithText = await businessCollection.AsQueryable()
                .FirstOrDefaultAsync(b => b.Texts.Any(t => t.Id == id));

            if (businessWithText == null)
                throw new KeyNotFoundException($"InfoText with Id {id} not found.");

            var result = businessWithText.Texts.FirstOrDefault(t => t.Id == id);
            if (result == null)
                throw new KeyNotFoundException($"InfoText with Id {id} not found.");

            return result;
        }

        public async Task<InfoTextFlexDto> GetByIdAsync(Guid id, InfoTextQueryOptions options)
        {
            var businessWithText = await businessCollection
            .AsQueryable()
            .FirstOrDefaultAsync(b => b.Texts.Any(t => t.Id == id));

            if (businessWithText == null)
                throw new KeyNotFoundException($"InfoText with Id {id} not found.");

            var text = businessWithText.Texts
                .FirstOrDefault(t => t.Id == id);

            if (text == null)
                throw new KeyNotFoundException($"InfoText with Id {id} not found.");

            var infoTextDto = new InfoTextFlexDto
            {
                Id = options.Id ? text.Id : null,
                Name = options.Name ? text.Name : null,
                Text = options.Text ? text.Text : null
            };

            return infoTextDto;
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