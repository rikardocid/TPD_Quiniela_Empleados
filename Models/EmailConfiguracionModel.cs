using Quinela_TPD.Models.Helper;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Quinela_TPD.Models
{
    [Table("EmailConfiguracion")]
    public class EmailConfiguracionModel : IEntity<string>
    {
        [Key]
        public string Clave { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Port { get; set; }
        public string Host { get; set; }
        public string Descripcion { get; set; }
        public bool Seguridad { get; set; }
    }
}
