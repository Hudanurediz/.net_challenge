using Enoca_Challenge.Application.Abstractions;
using Enoca_Challenge.Domain.Entities.Common;
using Enoca_Challenge.Persistance.Context;
using Microsoft.EntityFrameworkCore;

namespace Enoca_Challenge.Persistance.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> Table;


        public ReadRepository(ApplicationDbContext context)
        {
            _context = context;
            Table = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            try
            {
                return await _context.Set<T>()
                                     .Where(data => data.DeletedDate == null)
                                     .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<T> GetByIdAsync(int id)
        {
            try
            {
                var query = Table.AsQueryable()
                                 .Where(data => data.DeletedDate == null)
                                 .AsNoTracking();

                return await query.FirstOrDefaultAsync(data => data.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
