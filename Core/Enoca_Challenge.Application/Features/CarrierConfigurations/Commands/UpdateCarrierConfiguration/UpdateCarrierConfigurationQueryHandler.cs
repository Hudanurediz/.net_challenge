using Enoca_Challenge.Application.Abstractions;
using MediatR;

namespace Enoca_Challenge.Application.Features.CarrierConfigurations.Commands.UpdateCarrierConfiguration
{
    public class UpdateCarrierConfigurationQueryHandler : IRequestHandler<UpdateCarrierConfigurationQueryRequest, UpdateCarrierConfigurationQueryResponse>
    {
        readonly private ICarrierConfigurationReadRepository _carrierConfigurationReadRepository;
        readonly private ICarrierConfigurationWriteRepository _carrierConfigurationWriteRepository;

        public UpdateCarrierConfigurationQueryHandler(ICarrierConfigurationReadRepository carrierConfigurationReadRepository, ICarrierConfigurationWriteRepository carrierConfigurationWriteRepository)
        {
            _carrierConfigurationReadRepository = carrierConfigurationReadRepository;
            _carrierConfigurationWriteRepository = carrierConfigurationWriteRepository;
        }

        public async Task<UpdateCarrierConfigurationQueryResponse> Handle(UpdateCarrierConfigurationQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await _carrierConfigurationReadRepository.GetByIdAsync(request.Id);

                if (entity == null)
                {
                    return new UpdateCarrierConfigurationQueryResponse
                    {
                        Success = false,
                        Message = "Veri bulunamadı."
                    };
                }

                if (request.CarrierMaxDesi.HasValue && request.CarrierMaxDesi > 0)
                    entity.CarrierMaxDesi = request.CarrierMaxDesi.Value;

                if (request.CarrierMinDesi.HasValue && request.CarrierMinDesi > 0)
                    entity.CarrierMinDesi = request.CarrierMinDesi.Value;

                if (request.CarrierCost.HasValue && request.CarrierCost > 0)
                    entity.CarrierCost = request.CarrierCost.Value;

                if (request.CarrierId.HasValue && request.CarrierId > 0)
                    entity.CarrierId = request.CarrierId.Value;


                _carrierConfigurationWriteRepository.Update(request.Id, entity);
                return new UpdateCarrierConfigurationQueryResponse
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
