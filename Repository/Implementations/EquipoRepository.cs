using ASPNetCore6Identity.Data;
using Quinela_TPD.Models;
using Quinela_TPD.Repository.Interfaces;

namespace Quinela_TPD.Repository.Implementations
{
    public class EquipoRepository : GenericRepository<EquipoModel>, IEquipoRepository
    {
        private readonly ApplicationDbContext _context;
        public EquipoRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }
    }
}