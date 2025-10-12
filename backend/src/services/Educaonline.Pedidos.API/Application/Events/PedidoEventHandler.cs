using EasyNetQ;
using EducaOnline.Core.Messages.Integration;
using EducaOnline.MessageBus;

using MediatR;

namespace EducaOnLine.Pedidos.API.Application.Events
{
    public class PedidoEventHandler : INotificationHandler<PedidoRealizadoEvent>
    {
        private readonly IMessageBus _bus;

        public PedidoEventHandler(IMessageBus bus)
        {
            _bus = bus;
        }

        public async Task Handle(PedidoRealizadoEvent message, CancellationToken cancellationToken)
        {
            await _bus.PublishAsync(new PedidoRealizadoIntegrationEvent(message.ClienteId));
        }

        //public async Task Handle(PedidoPagoEvent message, CancellationToken cancellationToken)
        //{
        //    await _bus.PublishAsync(new PedidoPagoIntegrationEvent(message.ClienteId, message.PedidoId, message.Itens));
        //}

        
    }
}