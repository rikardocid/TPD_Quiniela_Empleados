using ASPNetCore6Identity.Models;
using System.Collections.Generic;

namespace Quinela_TPD.Models.ModelView
{
    public class QuinielaMV
    {
        public List<QuinielaModel> ListQuinielas { get; set; }
        public List<CodigoPromocionalModel> ListCodigosPromocionales { get; set; }
    }
}
