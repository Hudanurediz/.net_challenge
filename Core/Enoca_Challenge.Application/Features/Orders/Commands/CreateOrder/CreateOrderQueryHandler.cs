using Enoca_Challenge.Application.Abstractions;
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

                var result = await _orderWriteRepository.CalculateCarrierCost(request.OrderDesi);
                var order = new Order(
                    request.OrderDesi,
                    DateTime.UtcNow,
                    result.OrderCarrierCost,
                    result.CarrierId
                );

                var newOrder = await _orderWriteRepository.AddAsync(order);
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
    }
}
