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
                var carrierConfiguration = new CarrierConfiguration(
                    request.CarrierMaxDesi,
                    request.CarrierMinDesi,
                    request.CarrierCost,
                    request.CarrierId);


                var carrier = await _carrierReadRepository.GetByIdAsync(request.CarrierId);
                if (carrier == null)
                {
                    throw new ArgumentException("Taşıyıcı mevcut değil.");
                }


                var newcarrierConfiguration = await _carrierConfigurationWriteRepository.AddAsync(carrierConfiguration);
                if (newcarrierConfiguration != null)
                {
                    return new CreateCarrierConfigurationQueryResponse
                    {
                        CarrierConfiguration = newcarrierConfiguration,
                        Success = true,
                        Message = "Taşıyıcı Yapılandırması başarıyla oluşturuldu."
                    };
                }

                throw new InvalidOperationException("Taşıyıcı Yapılandırması kaydedilemedi.");
            }
            catch (Exception ex)
            {
                throw new Exception("Taşıyıcı Yapılandırması oluşturulurken beklenmeyen bir hata oluştu.", ex);
            }
        }
    }
}
