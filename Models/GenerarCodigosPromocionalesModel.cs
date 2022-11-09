using Quinela_TPD.Models.Helper;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Quinela_TPD.Models
{
    [Table("generar_codigos_promocionales")]
    public class GenerarCodigosPromocionalesModel : IEntity<string>
    {
        [Key]
        public string Clave { get; set; }
        public int Codigos { get; set; }
        public int CodigosGenerados { get; set; }
        public string Cliente { get; set; }
        public string EmailCliente { get; set; }
        public string RFC { get; set; }
        public double? CantidadFaltante { get; set; }

    }
}
