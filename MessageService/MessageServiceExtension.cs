using MessageService.Definition;
using MessageService.Implementation;
using Microsoft.Extensions.DependencyInjection;

namespace MessageService
{
    public static class MessageServiceExtension
    {
        public static void AddRabbitMqService(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IMessageService, RabbitMqService>();
        }
    }
}
