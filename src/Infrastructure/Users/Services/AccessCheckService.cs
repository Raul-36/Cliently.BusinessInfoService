using System;
using System.Linq;
using Application.Users.Services.Base;
using Infrastructure.Data;

namespace Infrastructure.Users.Services
{
    public class AccessCheckService : IAccessCheckService
    {
        private readonly BusinessInfoEFPostgreContext context;
        public AccessCheckService(BusinessInfoEFPostgreContext context)
        {
            this.context = context;
        }
        public bool ToBusiness(Guid userId, Guid businessId)
        {
            return this.context.Businesses.Any(b => b.Id == businessId && b.UserId == userId);
        }

        public bool ToInfoList(Guid userId, Guid infoListId)
        {
            return context.InfoLists
                .Join(context.Businesses,
                      il => il.BusinessId,
                      b => b.Id,
                      (il, b) => new { InfoList = il, Business = b })
                .Any(joined => joined.InfoList.Id == infoListId && joined.Business.UserId == userId);
        }
        public bool ToDynamicItem(Guid userId, Guid dynamicItemId)
        {
            return context.DynamicItems
                .Join(context.InfoLists,
                      di => di.ListId,
                      il => il.Id,
                      (di, il) => new { DynamicItem = di, InfoList = il })
                .Join(context.Businesses,
                      joined1 => joined1.InfoList.BusinessId,
                      b => b.Id,
                      (joined1, b) => new { joined1.DynamicItem, joined1.InfoList, Business = b })
                .Any(joined2 => joined2.DynamicItem.Id == dynamicItemId && joined2.Business.UserId == userId);
        }

        public bool ToInfoText(Guid userId, Guid infoTextId)
        {
            return context.InfoTexts
                .Join(context.Businesses,
                      it => it.BusinessId,
                      b => b.Id,
                      (it, b) => new { InfoText = it, Business = b })
                .Any(joined => joined.InfoText.Id == infoTextId && joined.Business.UserId == userId);
        }
    }
}