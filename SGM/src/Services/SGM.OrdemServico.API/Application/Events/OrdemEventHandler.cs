using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace SGM.OrdemServico.API.Application.Events
{
    public class OrdemEventHandler : INotificationHandler<OrdemRegistradaEvent>, INotificationHandler<OrdemAtualizadaEvent>
    {
        public Task Handle(OrdemRegistradaEvent notification, CancellationToken cancellationToken)
        {
            // Enviar evento de confirmação
            return Task.CompletedTask;
        }

        public Task Handle(OrdemAtualizadaEvent notification, CancellationToken cancellationToken)
        {
            // Enviar evento de confirmação
            return Task.CompletedTask;
        }

    }
}