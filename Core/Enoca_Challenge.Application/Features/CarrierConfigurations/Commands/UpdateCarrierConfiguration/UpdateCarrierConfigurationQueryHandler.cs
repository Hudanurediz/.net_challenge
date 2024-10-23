using Azure.Core;
using Enoca_Challenge.Application.Abstractions;
using Enoca_Challenge.Application.Features.CarrierConfigurations.Commands.CreateCarrierConfiguration;
using Enoca_Challenge.Domain.Entities;
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
                var entity = await GetCarrierConfiguration(request.Id);

                if (entity == null)
                {
                    return CreateResponse(false, "Veri bulunamadı.");
                }

                UpdateIfValid(entity, request);

                var result= UpdateCarrierConfiguration(request.Id, entity);

                return CreateResponse(result, result ? "Veri başarıyla güncellendi." : "Güncelleme işlemi başarısız.");

            }
            catch (Exception ex)
            {
                throw new Exception("Beklenmeyen bir hata oluştu.", ex);
            }
        }

        private async Task<CarrierConfiguration> GetCarrierConfiguration(int id)
        {
            return await _carrierConfigurationReadRepository.GetByIdAsync(id);
        }

        private bool UpdateCarrierConfiguration(int id,CarrierConfiguration entity)
        {
            return _carrierConfigurationWriteRepository.Update(id, entity);
        }

        private void UpdateIfValid(CarrierConfiguration entity, UpdateCarrierConfigurationQueryRequest request)
        {
            if (request.CarrierMaxDesi.HasValue && request.CarrierMaxDesi.Value > 0)
                entity.CarrierMaxDesi = request.CarrierMaxDesi.Value;

            if (request.CarrierMinDesi.HasValue && request.CarrierMinDesi.Value > 0)
                entity.CarrierMinDesi = request.CarrierMinDesi.Value;

            if (request.CarrierCost.HasValue && request.CarrierCost.Value > 0)
                entity.CarrierCost = request.CarrierCost.Value;

            if (request.CarrierId.HasValue && request.CarrierId.Value > 0)
                entity.CarrierId = request.CarrierId.Value;
        }

        private UpdateCarrierConfigurationQueryResponse CreateResponse(bool success, string message)
        {
            return new UpdateCarrierConfigurationQueryResponse
            {
                Success = success,
                Message = message
            };
        }

    }
}
