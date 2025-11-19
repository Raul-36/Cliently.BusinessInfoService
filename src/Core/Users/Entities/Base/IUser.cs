using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Users.Entities.Base
{
    public interface IUser
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public Guid BusinessId { get; set; }
    }
}