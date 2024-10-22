using MediatR;

namespace Enoca_Challenge.Application.Features.Orders.Commands.UpdateOrder
{
    public class UpdateOrderQueryRequest:IRequest<UpdateOrderQueryResponse>
    {
        public int Id { get; set; }

        public int? OrderDesi { get; set; }

        public DateTime? OrderDate { get; set; }

        public decimal? OrderCarrierCost { get; set; }

        public int? CarrierId { get; set; }
    }
}
