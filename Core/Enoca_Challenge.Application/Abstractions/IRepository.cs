using Microsoft.EntityFrameworkCore;

namespace Enoca_Challenge.Application.Abstractions
{
    public interface IRepository<T> where T : class
    {
        DbSet<T> Table { get; }
    }
}
