using Enoca_Challenge.Application.Abstractions;
using MediatR;

namespace Enoca_Challenge.Application.Features.CarrierConfigurations.Commands.DeleteCarrierConfiguration
{
    public class DeleteCarrierConfigurationQueryHandler : IRequestHandler<DeleteCarrierConfigurationQueryRequest, DeleteCarrierConfigurationQueryResponse>
    {
        readonly private ICarrierConfigurationWriteRepository _carrierConfigurationWriteRepository;

        public DeleteCarrierConfigurationQueryHandler(ICarrierConfigurationWriteRepository carrierConfigurationWriteRepository)
        {
            _carrierConfigurationWriteRepository = carrierConfigurationWriteRepository;
        }

        public async Task<DeleteCarrierConfigurationQueryResponse> Handle(DeleteCarrierConfigurationQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var deletionResult = DeleteCarrierConfiguration(request);
                var response = new DeleteCarrierConfigurationQueryResponse
                {
                    Success = deletionResult,
                    Message = deletionResult ? $"{request.Id} başarı ile silindi." : "Silme işlemi başarısız."
                };

                return response;
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException("Taşıyıcı yapılandırması silinirken bir hata oluştu.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Beklenmeyen bir hata oluştu.", ex);
            }
        }

        private bool DeleteCarrierConfiguration(DeleteCarrierConfigurationQueryRequest request)
        {
            return _carrierConfigurationWriteRepository.Delete(request.Id);
        }
    }
}
