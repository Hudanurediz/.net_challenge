using Enoca_Challenge.Application.Abstractions;
using MediatR;

namespace Enoca_Challenge.Application.Features.Carriers.Commands.UpdateCarrier
{
    public class UpdateCarrierQueryHandler : IRequestHandler<UpdateCarrierQueryRequest, UpdateCarrierQueryResponse>
    {
        readonly private ICarrierReadRepository _carrierReadRepository;
        readonly private ICarrierWriteRepository _carrierWriteRepository;

        public UpdateCarrierQueryHandler(ICarrierReadRepository carrierReadRepository, ICarrierWriteRepository carrierWriteRepository)
        {
            _carrierReadRepository = carrierReadRepository;
            _carrierWriteRepository = carrierWriteRepository;
        }

        public async Task<UpdateCarrierQueryResponse> Handle(UpdateCarrierQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await _carrierReadRepository.GetByIdAsync(request.Id);

                if (entity == null)
                {
                    return new UpdateCarrierQueryResponse
                    {
                        Success = false,
                        Message = "Veri bulunamadı."
                    };
                }

                if (!string.IsNullOrEmpty(request.CarrierName))
                    entity.CarrierName = request.CarrierName;

                if (request.CarrierIsActive.HasValue)
                    entity.CarrierIsActive = request.CarrierIsActive.Value;

                if (request.CarrierPlusDesiCost.HasValue && request.CarrierPlusDesiCost > 0)
                    entity.CarrierPlusDesiCost = request.CarrierPlusDesiCost.Value;

                _carrierWriteRepository.Update(request.Id, entity);
                return new UpdateCarrierQueryResponse
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
