using ASPNetCore6Identity.Models.Helper;
using Quinela_TPD.Models.Helper;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Quinela_TPD.Models
{
    [Table("usuario")]
    public class UsuarioModel : IEntity<string>
    {
        [Key]
        public string Clave { get; set; }
        [Required]
        public string Usuario { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public bool Cabecera { get; set; }
        [Required]
        public Enums.Roles Rol { get; set; }
        [Required]
        public Enums.EstatusAI Estatus { get; set; }
        [Required]
        public string Email { get; set; }

        [Required]
        public string Nombre { get; set; }

        public DateTime? UltimoAcceso { get; set; }
    }
}