using ASPNetCore6Identity.Data;
using Microsoft.EntityFrameworkCore;
using Quinela_TPD.Models;
using Quinela_TPD.Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quinela_TPD.Repository.Implementations
{
    public class QuinielaRepository : GenericRepository<QuinielaModel>, IQuinielaRepository
    {
        private readonly ApplicationDbContext context;
        public QuinielaRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<List<QuinielaModel>> CreateRangeAsync(List<QuinielaModel> quinielaModelList)
        {
            await this.context.Set<QuinielaModel>().AddRangeAsync(quinielaModelList);
            await this.SaveAllAsync();
            return quinielaModelList;
        }

        public List<QuinielaModel> GetAllIncludes()
        {
            return this.context.Set<QuinielaModel>()
                .Include(i => i.JuegoModel)
                .ThenInclude(m => m.MarcardorModel)
                    .Include(i => i.JuegoModel)
                    .ThenInclude(m => m.EquipoModel1)
                    .Include(i => i.JuegoModel)
                    .ThenInclude(m => m.EquipoModel2)
                .Include(ic => ic.CodigoPromocionalModel)
                .ThenInclude(us => us.UsuarioModel)
                .ToList();
            /* https://codepen.io/hkmDesigner/pen/zdPrVp */
        }

        public async Task<List<QuinielaModel>> UpdateRangeAsync(List<QuinielaModel> quinielaModelList)
        {
            this.context.Set<QuinielaModel>().UpdateRange(quinielaModelList);

            await this.SaveAllAsync();

            return quinielaModelList;
        }
    }
}