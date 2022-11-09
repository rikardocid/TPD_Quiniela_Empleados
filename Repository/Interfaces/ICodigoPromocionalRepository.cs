
using ASPNetCore6Identity.Models;
using Quinela_TPD.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Quinela_TPD.Repository.Interfaces
{
    public interface ICodigoPromocionalRepository : IGenericRepository<CodigoPromocionalModel>
    {
        Task<List<CodigoPromocionalModel>> AddRangeAsync(List<CodigoPromocionalModel> codigoPromocionalModelList);
        Task<List<CodigoPromocionalModel>> UpdateRangeAsync(List<CodigoPromocionalModel> codigoPromocionalModelList);
        List<CodigoPromocionalModel> GetAllIncludes();

        Task<CodigoPromocionalModel> GetIncludesByClaveAsync(string clave);
       
    }
}
