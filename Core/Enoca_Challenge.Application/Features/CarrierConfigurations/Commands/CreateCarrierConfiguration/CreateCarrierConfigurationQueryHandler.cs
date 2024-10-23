using Enoca_Challenge.Application.Abstractions;
using Enoca_Challenge.Domain.Entities;
using MediatR;

namespace Enoca_Challenge.Application.Features.CarrierConfigurations.Commands.CreateCarrierConfiguration
{
    public class CreateCarrierConfigurationQueryHandler : IRequestHandler<CreateCarrierConfigurationQueryRequest, CreateCarrierConfigurationQueryResponse>
    {
        readonly private ICarrierConfigurationWriteRepository _carrierConfigurationWriteRepository;
        readonly private ICarrierReadRepository _carrierReadRepository;

        public CreateCarrierConfigurationQueryHandler(ICarrierConfigurationWriteRepository carrierConfigurationWriteRepository, ICarrierReadRepository carrierReadRepository)
        {
            _carrierConfigurationWriteRepository = carrierConfigurationWriteRepository;
            _carrierReadRepository = carrierReadRepository;

        }

        public async Task<CreateCarrierConfigurationQueryResponse> Handle(CreateCarrierConfigurationQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var carrierConfiguration = CreateCarrierConfiguration(request);

                var carrier = await GetCarrier(request.CarrierId);

                if (carrier == null)
                {
                    throw new ArgumentException("Taşıyıcı mevcut değil.");
                }

                var newCarrierConfiguration = await SaveCarrierConfiguration(carrierConfiguration);

                return CreateSuccessResponse(newCarrierConfiguration);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException("Geçersiz işlem: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Taşıyıcı Yapılandırması oluşturulurken hata oluştu.", ex);
            }
        }

        private CarrierConfiguration CreateCarrierConfiguration(CreateCarrierConfigurationQueryRequest request)
        {
            var carrierConfiguration = new CarrierConfiguration(
                       request.CarrierMaxDesi,
                       request.CarrierMinDesi,
                       request.CarrierCost,
                       request.CarrierId);
            return carrierConfiguration;
        }

        private async Task<CarrierConfiguration> SaveCarrierConfiguration(CarrierConfiguration entity)
        {
            return await _carrierConfigurationWriteRepository.AddAsync(entity);
        }

        private async Task<Carrier> GetCarrier(int id)
        {
            return await _carrierReadRepository.GetByIdAsync(id);
        }

        private CreateCarrierConfigurationQueryResponse CreateSuccessResponse(CarrierConfiguration newCarrierConfiguration)
        {
            if (newCarrierConfiguration == null)
            {
                throw new InvalidOperationException("Taşıyıcı Yapılandırması kaydedilemedi.");
            }

            return new CreateCarrierConfigurationQueryResponse
            {
                CarrierConfiguration = newCarrierConfiguration,
                Success = true,
                Message = "Taşıyıcı Yapılandırması başarıyla oluşturuldu."
            };
        }
    }
}
