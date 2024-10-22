using Enoca_Challenge.Application.Abstractions;
using Enoca_Challenge.Domain.Entities;
using Enoca_Challenge.Persistance.Context;

namespace Enoca_Challenge.Persistance.Repositories
{
    public class CarrierReadRepository : ReadRepository<Carrier>, ICarrierReadRepository
    {
        public CarrierReadRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
