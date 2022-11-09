
using ASPNetCore6Identity.Data;
using ASPNetCore6Identity.Models;
using Microsoft.EntityFrameworkCore;
using Quinela_TPD.Models;
using Quinela_TPD.Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quinela_TPD.Repository.Implementations
{
    public class CodigoPromocionalRepository : GenericRepository<CodigoPromocionalModel>, ICodigoPromocionalRepository
    {
        private readonly ApplicationDbContext context;
        public CodigoPromocionalRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<List<CodigoPromocionalModel>> AddRangeAsync(List<CodigoPromocionalModel> codigoPromocionalModelList)
        {
            await this.context.Set<CodigoPromocionalModel>().AddRangeAsync(codigoPromocionalModelList);
            await SaveAllAsync();
            return codigoPromocionalModelList;
        }

        public async Task<List<CodigoPromocionalModel>> UpdateRangeAsync(List<CodigoPromocionalModel> codigoPromocionalModelList)
        {
            this.context.Set<CodigoPromocionalModel>().UpdateRange(codigoPromocionalModelList);
            await SaveAllAsync();
            return codigoPromocionalModelList;
        }

        public List<CodigoPromocionalModel> GetAllIncludes()
        {
            return this.context.Set<CodigoPromocionalModel>()
                .Include(i => i.QuinielaModel)
                .ThenInclude(j => j.JuegoModel)
                .ThenInclude(m => m.MarcardorModel)
                .Include(iu => iu.UsuarioModel)
                .ToList();
        }

        public async Task<CodigoPromocionalModel> GetIncludesByClaveAsync(string clave)
        {
            return await this.context.Set<CodigoPromocionalModel>()
                .Include(i=>i.UsuarioModel)
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Clave.Equals(clave));
        }
    }
}
