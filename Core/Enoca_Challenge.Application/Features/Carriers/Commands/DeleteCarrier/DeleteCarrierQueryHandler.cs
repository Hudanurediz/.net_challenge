using Enoca_Challenge.Application.Abstractions;
using Enoca_Challenge.Application.Features.CarrierConfigurations.Commands.DeleteCarrierConfiguration;
using MediatR;

namespace Enoca_Challenge.Application.Features.Carriers.Commands.DeleteCarrier
{
    public class DeleteCarrierQueryHandler : IRequestHandler<DeleteCarrierQueryRequest, DeleteCarrierQueryResponse>
    {
        readonly private ICarrierWriteRepository _carrierWriteRepository;

        public DeleteCarrierQueryHandler(ICarrierWriteRepository carrierWriteRepository)
        {
            _carrierWriteRepository = carrierWriteRepository;
        }

        public async Task<DeleteCarrierQueryResponse> Handle(DeleteCarrierQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var deletionResult = _carrierWriteRepository.Delete(request.Id);
                var response = new DeleteCarrierQueryResponse
                {
                    Success = deletionResult,
                    Message = deletionResult ? $"{request.Id} başarı ile silindi." : "Silme işlemi başarısız."
                };

                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("Beklenmeyen bir hata oluştu.", ex);
            }
        }
    }
}
