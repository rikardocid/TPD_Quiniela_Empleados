using Quinela_TPD.Models.Helper;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Quinela_TPD.Models
{
    [Table("equipo")]
    public class EquipoModel : IEntity<string>
    {
        [Key]
        public string Clave { get; set; }
        [Required]
        public string Bandera { get; set; }
        [Required]
        public string Escudo { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Grupo { get; set; }

    }
}