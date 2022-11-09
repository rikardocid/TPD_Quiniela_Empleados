using System.Linq;
using System.Threading.Tasks;

namespace Quinela_TPD.Repository.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> GetAll();

        Task<T> GetByClaveAsync(string clave);

        Task<T> CreateAsync(T entity);

        Task<T> UpdateAsync(T entity);

        Task DeleteAsync(T entity);

        Task<bool> ExistAsync(string clave);

        Task<bool> SaveAllAsync();
    }
}
