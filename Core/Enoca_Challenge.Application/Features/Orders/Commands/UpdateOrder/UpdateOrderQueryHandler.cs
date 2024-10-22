using Enoca_Challenge.Application.Abstractions;
using MediatR;

namespace Enoca_Challenge.Application.Features.Orders.Commands.UpdateOrder
{
    public class UpdateOrderQueryHandler : IRequestHandler<UpdateOrderQueryRequest, UpdateOrderQueryResponse>
    {
        readonly private IOrderReadRepository _orderReadRepository;
        readonly private IOrderWriteRepository _orderWriteRepository;

        public UpdateOrderQueryHandler(IOrderReadRepository orderReadRepository,IOrderWriteRepository orderWriteRepository)
        {
            _orderReadRepository = orderReadRepository;
            _orderWriteRepository = orderWriteRepository;
        }

        public async Task<UpdateOrderQueryResponse> Handle(UpdateOrderQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await _orderReadRepository.GetByIdAsync(request.Id);

                if (entity == null)
                {
                    return new UpdateOrderQueryResponse
                    {
                        Success = false,
                        Message = "Veri bulunamadı."
                    };
                }

                var oldOrderDesi = entity.OrderDesi;

                if (request.OrderDesi.HasValue && request.OrderDesi > 0)
                {
                    entity.OrderDesi = request.OrderDesi.Value;

                    if (oldOrderDesi != entity.OrderDesi)
                    {
                        var updatedCost = await _orderWriteRepository.CalculateCarrierCost(entity.OrderDesi);
                        entity.OrderCarrierCost = updatedCost.OrderCarrierCost;
                        entity.OrderDate = DateTime.Now;
                        entity.CarrierId = updatedCost.CarrierId;
                    }
                }

                _orderWriteRepository.Update(request.Id, entity);
                return new UpdateOrderQueryResponse
                {
                    Success = true,
                    Message = "Veri başarıyla güncellendi."
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Beklenmeyen bir hata oluştu.", ex);
            }
        }
    }
}
