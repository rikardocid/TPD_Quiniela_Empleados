using ASPNetCore6Identity.Data;
using Quinela_TPD.Models;
using Quinela_TPD.Models.Helper;
using Quinela_TPD.Repository.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Quinela_TPD.Repository.Implementations
{
    public class GenerarCodigosPromocionalesRepository : GenericRepository<GenerarCodigosPromocionalesModel>, IGenerarCodigosPromocionalesRepository
    {
        private readonly ApplicationDbContext context;
        public GenerarCodigosPromocionalesRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<List<GenerarCodigosPromocionalesModel>> UpdateRangeAsync(List<GenerarCodigosPromocionalesModel> generarCodigosPromocionalesModelList)
        {
            this.context.Set<GenerarCodigosPromocionalesModel>().UpdateRange(generarCodigosPromocionalesModelList);
            await SaveAllAsync();
            return generarCodigosPromocionalesModelList;
        }
    }
}
