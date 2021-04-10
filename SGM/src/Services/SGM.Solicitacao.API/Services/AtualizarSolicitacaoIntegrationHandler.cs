using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SGM.Solicitacao.API.Application.Commands;
using SGM.Core.Mediator;
using SGM.Core.Messages.Integration;
using SGM.MessageBus;

namespace SGM.Solicitacao.API.Services
{
    public class AtualizarSolicitacaoIntegrationHandler : BackgroundService
    {
        private readonly IMessageBus _bus;
        private readonly IServiceProvider _serviceProvider;

        public AtualizarSolicitacaoIntegrationHandler(IServiceProvider serviceProvider, IMessageBus bus)
        {
            _serviceProvider = serviceProvider;
            _bus = bus;
        }

        private void SetResponder()
        {
            _bus.RespondAsync<SolicitacaoAtualizadaIntegrationEvent, ResponseMessage>(async request =>
                await AtualizarSolicitacao(request));

            _bus.AdvancedBus.Connected += OnConnect;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            SetResponder();
            return Task.CompletedTask;
        }

        private void OnConnect(object s, EventArgs e)
        {
            SetResponder();
        }

        private async Task<ResponseMessage> AtualizarSolicitacao(SolicitacaoAtualizadaIntegrationEvent message)
        {

            var clienteCommand = new AtualizarSolicitacaoCommand(message.Id,message.CodDepartamento,message.Status);

            ValidationResult sucesso;

            using (var scope = _serviceProvider.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();
                sucesso = await mediator.EnviarComando(clienteCommand);
            }

            return new ResponseMessage(sucesso);
        }
    }
}