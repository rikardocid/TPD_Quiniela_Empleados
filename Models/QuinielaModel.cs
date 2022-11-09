using ASPNetCore6Identity.Models;
using Quinela_TPD.Models.Helper;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Quinela_TPD.Models
{
    [Table("quiniela")]
    public class QuinielaModel : IEntity<string>
    {
        [Key]
        public string Clave { get; set; }
        public int? Prediccion1 { get; set; }
        public int? Prediccion3 { get; set; }
        public int? Puntaje { get; set; }

        [ForeignKey("JuegoModel")]
        [Column("ClaveJuego")]
        public string ClaveJuego { get; set; }
        public JuegoModel JuegoModel { get; set; }

        [ForeignKey("CodigoPromocionalModel")]
        [Column("ClaveCodigoPromocional")]
        public string ClaveCodigoPromocional { get; set; }
        public CodigoPromocionalModel CodigoPromocionalModel { get; set; }
    }
}
