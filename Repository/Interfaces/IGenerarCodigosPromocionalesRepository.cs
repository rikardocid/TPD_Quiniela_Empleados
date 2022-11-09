using Quinela_TPD.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Quinela_TPD.Repository.Interfaces
{
    public interface IGenerarCodigosPromocionalesRepository : IGenericRepository<GenerarCodigosPromocionalesModel>
    {
        Task<List<GenerarCodigosPromocionalesModel>> UpdateRangeAsync(List<GenerarCodigosPromocionalesModel> generarCodigosPromocionalesModelList);
    }
}
