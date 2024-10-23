using Enoca_Challenge.Application.Abstractions;
using MediatR;

namespace Enoca_Challenge.Application.Features.Orders.Commands.DeleteOrder
{
    public class DeleteOrderQueryHandler : IRequestHandler<DeleteOrderQueryRequest, DeleteOrderQueryResponse>
    {
        readonly private IOrderWriteRepository _orderWriteRepository;

        public DeleteOrderQueryHandler(IOrderWriteRepository orderWriteRepository)
        {
            _orderWriteRepository = orderWriteRepository;
        }

        public async Task<DeleteOrderQueryResponse> Handle(DeleteOrderQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var deletionResult = DeleteOrder(request);
                var response = new DeleteOrderQueryResponse
                {
                    Success = deletionResult,
                    Message = deletionResult ? $"{request.Id} başarı ile silindi." : "Silme işlemi başarısız."
                };

                return response;
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException("Sipariş silinirken bir hata oluştu.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Beklenmeyen bir hata oluştu.", ex);
            }
        }

        private bool DeleteOrder(DeleteOrderQueryRequest request)
        {
            return _orderWriteRepository.Delete(request.Id);
        }
    }
}
