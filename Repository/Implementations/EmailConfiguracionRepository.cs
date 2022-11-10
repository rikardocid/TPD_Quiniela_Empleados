using ASPNetCore6Identity.Data;
using Quinela_TPD.Models;
using Quinela_TPD.Repository.Interfaces;

namespace Quinela_TPD.Repository.Implementations
{
    public class EmailConfiguracionRepository : GenericRepository<EmailConfiguracionModel>, IEmailConfiguracionRepository
    {
        private readonly ApplicationDbContext _context;
        public EmailConfiguracionRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }
    }
}