using Enoca_Challenge.Application.Abstractions;
using Enoca_Challenge.Domain.Entities;
using MediatR;

namespace Enoca_Challenge.Application.Features.CarrierConfigurations.Queries.GetAllCarrierConfigurations
{
    public class GetAllCarrierConfigurationsQueryHandler : IRequestHandler<GetAllCarrierConfigurationsQueryRequest, GetAllCarrierConfigurationsQueryResponse>
    {
        readonly private ICarrierConfigurationReadRepository _carrierConfigurationReadRepository;

        public GetAllCarrierConfigurationsQueryHandler(ICarrierConfigurationReadRepository carrierConfigurationReadRepository)
        {
            _carrierConfigurationReadRepository = carrierConfigurationReadRepository;
        }

        public async Task<GetAllCarrierConfigurationsQueryResponse> Handle(GetAllCarrierConfigurationsQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var carrierConfigurations = await GetAllCarrierConfiguration();

                if (carrierConfigurations == null || !carrierConfigurations.Any())
                {
                    return new GetAllCarrierConfigurationsQueryResponse
                    {
                        CarrierConfigurations = new List<CarrierConfiguration>(),
                        Success = false,
                        Message = "Kayıtlı veri bulunamadı."
                    };
                }

                return new GetAllCarrierConfigurationsQueryResponse
                {
                    CarrierConfigurations = carrierConfigurations.ToList(),
                    Success = true,
                    Message = "Başarılı istek"
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Beklenmedik bir hata oluştu.", ex);
            }
        }

        public async Task<List<CarrierConfiguration>> GetAllCarrierConfiguration()
        {
            return (await _carrierConfigurationReadRepository.GetAllAsync()).ToList();
        }

    }
}
