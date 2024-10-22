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
                await _carrierWriteRepository.AddCarrier(request);
                return new CreateCarrierQueryResponse
                {
                    Success = true,
                    Message = "Taşıyıcı öğesi başarıyla oluşturuldu."
                };
            }
            catch (Exception ex)
            {
                return new CreateCarrierQueryResponse
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }
    }
}
