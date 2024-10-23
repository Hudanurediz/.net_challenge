using Azure.Core;
using Enoca_Challenge.Application.Abstractions;
using Enoca_Challenge.Application.Dtos;
using Enoca_Challenge.Domain.Entities;
using MediatR;

namespace Enoca_Challenge.Application.Features.Orders.Commands.CreateOrder
{
    public class CreateOrderQueryHandler : IRequestHandler<CreateOrderQueryRequest, CreateOrderQueryResponse>
    {
        readonly private IOrderWriteRepository _orderWriteRepository;
        readonly private ICarrierReadRepository _carrierReadRepository;

        public CreateOrderQueryHandler(IOrderWriteRepository orderWriteRepository, ICarrierReadRepository carrierReadRepository)
        {
            _orderWriteRepository = orderWriteRepository;
            _carrierReadRepository = carrierReadRepository;
        }

        public async Task<CreateOrderQueryResponse> Handle(CreateOrderQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {

                var result = await CalculateCarrierCost(request.OrderDesi);
                var order = CreateOrder(result,request);

                var newOrder = await AddOrder(order);

                if (newOrder != null)
                {
                    return new CreateOrderQueryResponse
                    {
                        Order = newOrder,
                        Success = true,
                        Message = "Sipariş başarıyla oluşturuldu."
                    };
                }

                throw new InvalidOperationException("Sipariş öğesi kaydedilemedi.");
            }
            catch (Exception ex)
            {
                throw new Exception("Sipariş oluşturulurken beklenmeyen bir hata oluştu.", ex);
            }
        }

        private async Task<CalculateCostDto> CalculateCarrierCost(int desi)
        {
            return await _orderWriteRepository.CalculateCarrierCost(desi);
        }

        private Order CreateOrder(CalculateCostDto calculateCostDto, CreateOrderQueryRequest request)
        {
            var order = new Order(
                    request.OrderDesi,
                    DateTime.UtcNow,
                    calculateCostDto.OrderCarrierCost,
                    calculateCostDto.CarrierId
                );
            return order;
        }

        private async Task<Order> AddOrder(Order order)
        {
            return await _orderWriteRepository.AddAsync(order);
        }
    }
}
