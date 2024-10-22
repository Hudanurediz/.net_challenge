using Enoca_Challenge.Application.Dtos;
using Enoca_Challenge.Domain.Entities;

namespace Enoca_Challenge.Application.Features.Orders.Queries.GetAllOrders
{
    public class GetAllOrdersQueryResponse:BaseResponseDto
    {
        public List<Order> Orders { get; set; }
    }
}
