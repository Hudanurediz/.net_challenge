using Enoca_Challenge.Application.Abstractions;
using Enoca_Challenge.Domain.Entities;
using MediatR;

namespace Enoca_Challenge.Application.Features.Orders.Queries.GetAllOrders
{
    public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQueryRequest, GetAllOrdersQueryResponse>
    {
        readonly private IOrderReadRepository _orderReadRepository;

        public GetAllOrdersQueryHandler(IOrderReadRepository orderReadRepository)
        {
            _orderReadRepository = orderReadRepository;
        }

        public async Task<GetAllOrdersQueryResponse> Handle(GetAllOrdersQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var orders = await GetAllOrders();

                if (orders == null || !orders.Any())
                {
                    return new GetAllOrdersQueryResponse
                    {
                        Orders = new List<Order>(),
                        Success = false,
                        Message = "Kayıtlı veri bulunamadı."
                    };
                }

                return new GetAllOrdersQueryResponse
                {
                    Orders = orders.ToList(),
                    Success = true,
                    Message = "Başarılı istek"
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Beklenmeyen bir hata oluştu.", ex);
            }
        }

        private async Task<List<Order>> GetAllOrders()
        {
            return (await _orderReadRepository.GetAllAsync()).ToList();
        }
    }
}
