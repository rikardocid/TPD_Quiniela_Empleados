using ASPNetCore6Identity.Data;
using Quinela_TPD.Models;
using Quinela_TPD.Repository.Interfaces;

namespace Quinela_TPD.Repository.Implementations
{
    public class EstadioRepository : GenericRepository<EstadioModel>, IEstadioRepository
    {
        private readonly ApplicationDbContext _context;
        public EstadioRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }
    }
}