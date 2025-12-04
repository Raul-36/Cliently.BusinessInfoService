using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Options
{
    public class JwtOptions
    {
        public required string Key { get; set; }
        public byte[] KeyInBytes => Encoding.ASCII.GetBytes(Key);
        public int LifeTimeInMinutes { get; set; }
        public required string Issuer { get; set; }
        public required string Audience { get; set; }
    }
}