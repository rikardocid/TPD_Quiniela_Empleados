using ASPNetCore6Identity.Data;
using Quinela_TPD.Models;
using Quinela_TPD.Repository.Interfaces;

namespace Quinela_TPD.Repository.Implementations
{
    public class MarcadorRepository : GenericRepository<MarcardorModel>, IMarcadorRepository
    {
        private readonly ApplicationDbContext _context;
        public MarcadorRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }
    }
}