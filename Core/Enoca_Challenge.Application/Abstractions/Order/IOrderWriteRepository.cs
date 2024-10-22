using Enoca_Challenge.Application.Dtos;
using Enoca_Challenge.Application.Features.Orders.Commands.CreateOrder;
using Enoca_Challenge.Domain.Entities;

namespace Enoca_Challenge.Application.Abstractions
{
    public interface IOrderWriteRepository : IWriteRepository<Order>
    {
        Task<CalculateCostDto> CalculateCarrierCost(decimal cost);
    }
}
