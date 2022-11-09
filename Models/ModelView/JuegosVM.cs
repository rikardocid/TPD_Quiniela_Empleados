using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Quinela_TPD.Models.ModelView
{
    public class JuegosVM
    {
        public List<JuegoModel> JuegoList { get; set; }

        public JuegoModel JuegoModel { get; set; }

        public IEnumerable<SelectListItem> Equipo1 { get; set; }

        public IEnumerable<SelectListItem> Equipo2 { get; set; }

        public IEnumerable<SelectListItem> Estadio { get; set; }

    }
}
