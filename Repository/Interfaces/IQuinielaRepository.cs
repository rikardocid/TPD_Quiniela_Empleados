using Quinela_TPD.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Quinela_TPD.Repository.Interfaces
{
    public interface IQuinielaRepository : IGenericRepository<QuinielaModel>
    {
        List<QuinielaModel> GetAllIncludes();
        Task<List<QuinielaModel>> CreateRangeAsync(List<QuinielaModel> quinielaModelList);
        Task<List<QuinielaModel>> UpdateRangeAsync(List<QuinielaModel> quinielaModelList);
    }
}
