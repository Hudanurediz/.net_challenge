using Azure.Core;
using Enoca_Challenge.Application.Abstractions;
using Enoca_Challenge.Application.Features.CarrierConfigurations.Commands.UpdateCarrierConfiguration;
using Enoca_Challenge.Domain.Entities;
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
                var entity = await GetCarrier(request.Id);

                if (entity == null)
                {
                    return new UpdateCarrierQueryResponse
                    {
                        Success = false,
                        Message = "Veri bulunamadı."
                    };
                }

                UpdateIfValid(entity, request);

                var result = UpdateCarrier(request.Id, entity);

                return CreateResponse(result, result ? "Veri başarıyla güncellendi." : "Güncelleme işlemi başarısız.");

            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException("Taşıyıcı güncellenirken bir hata oluştu.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Beklenmeyen bir hata oluştu.", ex);
            }
        }

        private async Task<Carrier> GetCarrier(int id)
        {
            return await _carrierReadRepository.GetByIdAsync(id);
        }

        private void UpdateIfValid(Carrier entity,UpdateCarrierQueryRequest request)
        {
            if (!string.IsNullOrEmpty(request.CarrierName))
                entity.CarrierName = request.CarrierName;

            if (request.CarrierIsActive.HasValue)
                entity.CarrierIsActive = request.CarrierIsActive.Value;

            if (request.CarrierPlusDesiCost.HasValue && request.CarrierPlusDesiCost > 0)
                entity.CarrierPlusDesiCost = request.CarrierPlusDesiCost.Value;
        }

        private bool UpdateCarrier(int id,Carrier entity)
        {
            return _carrierWriteRepository.Update(id, entity);
        }

        private UpdateCarrierQueryResponse CreateResponse(bool success, string message)
        {
            return new UpdateCarrierQueryResponse
            {
                Success = success,
                Message = message
            };
        }

    }
}
