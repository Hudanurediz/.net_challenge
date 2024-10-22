using Enoca_Challenge.Application.Features.Carriers.Commands.CreateCarrier;
using Enoca_Challenge.Domain.Entities;

namespace Enoca_Challenge.Application.Abstractions
{
    public interface ICarrierWriteRepository : IWriteRepository<Carrier>
    {
        Task<bool> AddCarrier(CreateCarrierQueryRequest request);
    }
}
