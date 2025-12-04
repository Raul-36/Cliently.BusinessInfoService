using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Users.Services.Base
{
    public interface IAccessCheckService
    {
        public bool ToBusiness(Guid userId, Guid businessId);
        public bool ToInfoList(Guid userId, Guid infoListId);
        public bool ToDynamicItem(Guid userId, Guid dynamicItemId);
        public bool ToInfoText(Guid userId, Guid infoTextId);
    }
}