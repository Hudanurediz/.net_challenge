namespace Enoca_Challenge.Application.Abstractions
{
    public interface IReadRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<T> GetByIdAsync(int id);
    }
}
