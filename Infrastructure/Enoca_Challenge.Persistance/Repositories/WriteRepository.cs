using Enoca_Challenge.Application.Abstractions;
using Enoca_Challenge.Domain.Entities.Common;
using Enoca_Challenge.Persistance.Context;
using Microsoft.EntityFrameworkCore;

namespace Enoca_Challenge.Persistance.Repositories
{
    public class WriteRepository<T> : IWriteRepository<T> where T : BaseEntity
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> Table;

        public WriteRepository(ApplicationDbContext context)
        {
            _context = context;
            Table = _context.Set<T>();
        }

        public async Task<T> AddAsync(T entity)
        {
            try
            {
                var newEntityEntry = await Table.AddAsync(entity);
                await _context.SaveChangesAsync();
                return newEntityEntry.Entity;
            }
            catch (Exception ex)
            {
                throw new Exception("Öğe eklenirken bir hata oluştu: " + ex.Message, ex);
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var existingEntity = Table.SingleOrDefault(e => e.Id == id && e.DeletedDate == null);
                if (existingEntity == null)
                {
                    throw new ArgumentException($"{id} ID'ye sahip öğe mevcut değil.");
                }

                existingEntity.DeletedDate = DateTime.UtcNow;
                _context.Entry(existingEntity).State = EntityState.Modified;

                var changes = _context.SaveChanges();
                return changes > 0;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Geçersiz argüman: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Bir hata oluştu: {ex.Message}");
                throw new Exception(ex.Message, ex);
            }
        }

        public bool Update(int id, T entity)
        {
            try
            {
                var existingEntity = Table.Find(id);
                if (existingEntity == null)
                {
                    throw new ArgumentException("Öğe mevcut değil.");
                }
                entity.Id = existingEntity.Id;

                _context.Entry(existingEntity).CurrentValues.SetValues(entity);

                var updated = _context.SaveChanges();
                if (updated > 0)
                {
                    existingEntity.UpdatedDate = DateTime.UtcNow;
                    _context.SaveChanges();
                }
                return updated > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

        }
    }
}
