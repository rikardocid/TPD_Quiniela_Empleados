using Quinela_TPD.Models;
using System.Collections.Generic;

namespace Quinela_TPD.Repository.Interfaces
{
    public interface IJuegoRepository : IGenericRepository<JuegoModel>
    {
        List<JuegoModel> GetAllIncludes();
    }
}
