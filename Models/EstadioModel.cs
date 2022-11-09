using Quinela_TPD.Models.Helper;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Quinela_TPD.Models
{
    [Table("estadio")]
    public class EstadioModel : IEntity<string>
    {
        [Key]
        [Required]
        public string Clave { get; set; }
        [Required]
        public string Nombre { get; set; }

        public string Imagen { get; set; }
    }
}
