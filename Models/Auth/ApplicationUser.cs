using ASPNetCore6Identity.Models.Helper;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASPNetCore6Identity.Models.Auth
{
    public class ApplicationUser : IdentityUser
    {
        [Column("estatus"), Required]
        public Enums.EstatusAI Estatus { get; set; }

        [Column("nombre"), Required]
        public string Nombre { get; set; }

        [Column("Apellido_paterno"), Required]
        public string ApellidoPaterno { get; set; }

        [Column("apellido_materno")]
        public string ApellidoMaterno { get; set; }

        [Column("domicilio"), Required]
        public string Domicilio { get; set; }

        [NotMapped]
        public IList<string> UserRoles { get; set; }

    }
}
