using System.Diagnostics.CodeAnalysis;
using Lab07.UnitTesting.DAL.Models;
using System.Threading.Tasks;

namespace Lab07.UnitTesting.DAL.Interfaces.Repositories
{
    public interface IBaseRepository<T>
        where T : BaseEntity
    {
        Task CreateAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);

        Task<T> GetByIdAsync(int id);
    }
}