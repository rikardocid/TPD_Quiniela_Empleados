using Quinela_TPD.Models;
using System.Collections.Generic;

namespace Quinela_TPD.Repository.Interfaces
{
    public interface IUsuarioRepository : IGenericRepository<UsuarioModel>
    {
        void GetExtraerCodigosPromocionales();

    }
}
