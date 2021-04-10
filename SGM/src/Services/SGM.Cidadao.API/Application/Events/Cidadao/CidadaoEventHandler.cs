using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace SGM.Cidadao.API.Application.Events
{
    public class CidadaoEventHandler : INotificationHandler<CidadaoRegistradoEvent>
    {
        public Task Handle(CidadaoRegistradoEvent notification, CancellationToken cancellationToken)
        {
            // Enviar evento de confirmação
            return Task.CompletedTask;
        }
    }
}