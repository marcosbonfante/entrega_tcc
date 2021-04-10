using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace SGM.Solicitacao.API.Application.Events
{
    public class SolicitacaoEventHandler : INotificationHandler<SolicitacaoRegistradoEvent>, INotificationHandler<SolicitacaoAtualizadaEvent>
    {
        public Task Handle(SolicitacaoRegistradoEvent notification, CancellationToken cancellationToken)
        {
            // Enviar evento de confirmação
            return Task.CompletedTask;
        }

        public Task Handle(SolicitacaoAtualizadaEvent notification, CancellationToken cancellationToken)
        {
            // Enviar evento de confirmação
            return Task.CompletedTask;
        }

    }
}