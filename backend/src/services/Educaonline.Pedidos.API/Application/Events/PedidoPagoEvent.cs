using EducaOnline.Core.Messages;

namespace EducaOnLine.Pedidos.API.Application.Events
{
    public class PedidoPagoEvent:Event
    {
        public Guid ClienteId { get; private set; }
        public Guid PedidoId { get; private set; }
        public IEnumerable<Guid> Itens { get; private set; }

        public PedidoPagoEvent(Guid clienteId, Guid pedidoId, IEnumerable<Guid> itens)
        {
            ClienteId = clienteId;
            PedidoId = pedidoId;
            Itens = itens;
        }
    }
}
