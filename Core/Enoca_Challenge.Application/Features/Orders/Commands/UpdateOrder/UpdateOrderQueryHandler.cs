using Enoca_Challenge.Application.Abstractions;
using Enoca_Challenge.Domain.Entities;
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
                var entity = await GetOrder(request);

                if (entity == null)
                {
                    return new UpdateOrderQueryResponse
                    {
                        Success = false,
                        Message = "Veri bulunamadı."
                    };
                }

                await UpdateOrderDetails(entity, request);

                var result=UpdateOrder(request.Id, entity);

                return CreateResponse(result, result ? "Veri başarıyla güncellendi." : "Güncelleme işlemi başarısız.");
            }
            catch (Exception ex)
            {
                throw new Exception("Beklenmeyen bir hata oluştu.", ex);
            }
        }

        private async Task<Order> GetOrder(UpdateOrderQueryRequest request)
        {
            return await _orderReadRepository.GetByIdAsync(request.Id);
        }

        private bool UpdateOrder(int id,Order entity)
        {
            return _orderWriteRepository.Update(id, entity);
        }

        private async Task UpdateOrderDetails(Order entity, UpdateOrderQueryRequest request)
        {
            var oldOrderDesi = entity.OrderDesi;

            if (request.OrderDesi.HasValue && request.OrderDesi > 0)
            {
                entity.OrderDesi = request.OrderDesi.Value;

                if (oldOrderDesi != entity.OrderDesi)
                {
                    var updatedCost = await _orderWriteRepository.CalculateCarrierCost(entity.OrderDesi);
                    entity.OrderCarrierCost = updatedCost.OrderCarrierCost;
                    entity.OrderDate = DateTime.UtcNow;
                    entity.CarrierId = updatedCost.CarrierId;
                }
            }
        }

        private UpdateOrderQueryResponse CreateResponse(bool success, string message)
        {
            return new UpdateOrderQueryResponse
            {
                Success = success,
                Message = message
            };
        }
    }
}
