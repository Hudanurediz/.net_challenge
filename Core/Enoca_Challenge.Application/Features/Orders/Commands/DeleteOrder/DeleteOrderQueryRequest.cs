using MediatR;

namespace Enoca_Challenge.Application.Features.Orders.Commands.DeleteOrder
{
    public class DeleteOrderQueryRequest:IRequest<DeleteOrderQueryResponse>
    {
        public int Id { get; set; }
    }
}
