using ASPNetCore6Identity.Models.Helper;
using Quinela_TPD.Models;
using Quinela_TPD.Models.Helper;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASPNetCore6Identity.Models
{
    [Table("codigo_promocional")]
    public class CodigoPromocionalModel : IEntity<string>
    {
        [Key]
        public string Clave { get; set; }
        [Required]
        public string Codigo { get; set; }
        [Required]
        public string Cliente { get; set; }
        public string Nombre { get; set; }
        [Required]
        public string EmailCliente { get; set; }
        [Required]
        public Enums.EstatusCodigoPromocional Utilizado { get; set; }
        public string Comentarios { get; set; }

        public int? PuntajeQuinela { get; set; }
        public int? Consecutivo { get; set; }
        public int? posicion { get; set; }

        [ForeignKey("QuinielaModel")]
        [Column("ClaveQuinela")]
        public string ClaveQuinela { get; set; }
        public QuinielaModel QuinielaModel { get; set; }

        [ForeignKey("UsuarioModel")]
        [Column("ClaveUsuario")]
        public string ClaveUsuario { get; set; }
        public UsuarioModel UsuarioModel { get; set; }
    }
}
