using Enoca_Challenge.Application.Dtos;
using Enoca_Challenge.Domain.Entities;

namespace Enoca_Challenge.Application.Features.Orders.Commands.CreateOrder
{
    public class CreateOrderQueryResponse:BaseResponseDto
    {
        public Order Order { get; set; }
    }
}
