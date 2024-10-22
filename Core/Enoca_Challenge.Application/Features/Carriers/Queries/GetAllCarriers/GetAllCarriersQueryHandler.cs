using Enoca_Challenge.Application.Abstractions;
using Enoca_Challenge.Domain.Entities;
using MediatR;

namespace Enoca_Challenge.Application.Features.Carriers.Queries.GetAllCarriers
{
    public class GetAllCarriersQueryHandler : IRequestHandler<GetAllCarriersQueryRequest, GetAllCarriersQueryResponse>
    {
        readonly private ICarrierReadRepository _carrierReadRepository;

        public GetAllCarriersQueryHandler(ICarrierReadRepository carrierReadRepository)
        {
            _carrierReadRepository = carrierReadRepository;
        }

        public async Task<GetAllCarriersQueryResponse> Handle(GetAllCarriersQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var carriers = await _carrierReadRepository.GetAllAsync();

                if (carriers == null || !carriers.Any())
                {
                    return new GetAllCarriersQueryResponse
                    {
                        Carriers = new List<Carrier>(),
                        Success = false,
                        Message = "Kayıtlı veri bulunamadı."
                    };
                }

                return new GetAllCarriersQueryResponse
                {
                    Carriers = carriers.ToList(),
                    Success = true,
                    Message = "Başarılı istek"
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Beklenmeyen bir hata oluştu.", ex);
            }
        }
    }
}
