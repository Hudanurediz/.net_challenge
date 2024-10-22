namespace Enoca_Challenge.Application.Abstractions
{
    public interface IWriteRepository<T> where T : class
    {
        Task<T> AddAsync(T entity);

        bool Update(int id, T entity);

        bool Delete(int id);
    }
}
