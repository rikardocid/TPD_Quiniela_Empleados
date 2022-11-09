using ASPNetCore6Identity.Data;
using Microsoft.EntityFrameworkCore;
using Quinela_TPD.Models.Helper;
using Quinela_TPD.Repository.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace Quinela_TPD.Repository.Implementations
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class, IEntity<string>
    {
        private readonly ApplicationDbContext context;

        public GenericRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IQueryable<T> GetAll()
        {
            return this.context.Set<T>().AsNoTracking();
        }

        public async Task<T> GetByClaveAsync(string clave)
        {
            return await this.context.Set<T>()
                //.AsNoTracking()
                .FirstOrDefaultAsync(e => e.Clave.Equals(clave));
        }

        public async Task<T> CreateAsync(T entity)
        {
            await this.context.Set<T>().AddAsync(entity);
            await SaveAllAsync();
            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            this.context.Set<T>().Update(entity);
            await SaveAllAsync();
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            this.context.Set<T>().Remove(entity);
            await SaveAllAsync();
        }

        public async Task<bool> ExistAsync(string clave)
        {
            return await this.context.Set<T>().AnyAsync(e => e.Clave.Equals(clave));

        }

        public async Task<bool> SaveAllAsync()
        {
            return await this.context.SaveChangesAsync() > 0;
        }
    }
}
