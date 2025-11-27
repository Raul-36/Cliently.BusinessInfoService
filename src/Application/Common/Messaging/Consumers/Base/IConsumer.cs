using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Common.Messaging.Consumers.Base
{
    public interface IConsumer
    {
        public Task ExecuteAsync();
    }
}