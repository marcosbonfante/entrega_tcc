using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SGM.Solicitacao.API.Services;
using SGM.Core.Utils;
using SGM.MessageBus;

namespace SGM.Solicitacao.API.Configuration
{
    public static class MessageBusConfig
    {
        public static void AddMessageBusConfiguration(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddMessageBus(configuration.GetMessageQueueConnection("MessageBus"))
                .AddHostedService<AtualizarSolicitacaoIntegrationHandler>();
        }
    }
}