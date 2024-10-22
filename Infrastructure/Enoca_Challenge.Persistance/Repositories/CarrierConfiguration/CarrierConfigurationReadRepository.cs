using Enoca_Challenge.Application.Abstractions;
using Enoca_Challenge.Domain.Entities;
using Enoca_Challenge.Persistance.Context;

namespace Enoca_Challenge.Persistance.Repositories
{
    public class CarrierConfigurationReadRepository : ReadRepository<CarrierConfiguration>, ICarrierConfigurationReadRepository
    {
        public CarrierConfigurationReadRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
