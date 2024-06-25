using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageService.Definition
{
    public interface IMessageService
    {
        bool Publish<T>(T v, Func<T,Guid> databaseResilience,string queue = "default", string routingKey = "default");
    }
}
