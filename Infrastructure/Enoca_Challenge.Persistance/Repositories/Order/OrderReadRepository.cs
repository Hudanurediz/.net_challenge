using Enoca_Challenge.Application.Abstractions;
using Enoca_Challenge.Domain.Entities;
using Enoca_Challenge.Persistance.Context;

namespace Enoca_Challenge.Persistance.Repositories
{
    public class OrderReadRepository : ReadRepository<Order>, IOrderReadRepository
    {
        public OrderReadRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
