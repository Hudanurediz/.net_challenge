using Enoca_Challenge.Application.Abstractions;
using MediatR;

namespace Enoca_Challenge.Application.Features.Carriers.Commands.CreateCarrier
{
    public class CreateCarrierQueryHandler : IRequestHandler<CreateCarrierQueryRequest, CreateCarrierQueryResponse>
    {
        private readonly ICarrierWriteRepository _carrierWriteRepository;

        public CreateCarrierQueryHandler(ICarrierWriteRepository carrierWriteRepository)
        {
            _carrierWriteRepository = carrierWriteRepository;
        }

        public async Task<CreateCarrierQueryResponse> Handle(CreateCarrierQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var carrier = await CreateCarrier(request);

                return new CreateCarrierQueryResponse
                {
                    Success = true,
                    Message = "Taşıyıcı öğesi başarıyla oluşturuldu."
                };
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException("Taşıyıcı silinirken bir hata oluştu.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Beklenmeyen bir hata oluştu.", ex);
            }
        }

        private async Task<bool> CreateCarrier(CreateCarrierQueryRequest request)
        {
            return await _carrierWriteRepository.AddCarrier(request);
        }
    }
}
