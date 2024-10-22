using Enoca_Challenge.Application.Dtos;
using Enoca_Challenge.Domain.Entities;

namespace Enoca_Challenge.Application.Features.Orders.Commands.UpdateOrder
{
    public class UpdateOrderQueryResponse:BaseResponseDto
    {
        public Order Order { get; set; }
    }
}
