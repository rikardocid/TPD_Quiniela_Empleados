using Quinela_TPD.Models.Helper;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Quinela_TPD.Models
{
    [Table("marcador")]
    public class MarcardorModel : IEntity<string>
    {
        [Key]
        [Required]
        public string Clave { get; set; }

        [ForeignKey("JuegoModel")]
        [Column("ClaveJuego")]
        public string ClaveJuego { get; set; }
        public JuegoModel JuegoModel { get; set; }

        public int? Equipo1 { get; set; }

        public int? Equipo2 { get; set; }
    }
}
