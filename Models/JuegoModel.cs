using ASPNetCore6Identity.Models.Helper;
using Quinela_TPD.Models.Helper;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Quinela_TPD.Models
{
    [Table("juego")]
    public class JuegoModel : IEntity<string>
    {
        [Key]
        [Required]
        public string Clave { get; set; }

        [Required]
        public DateTime FechaJuego { get; set; }

        [Required]
        public int Fase { get; set; }

        [Required]
        public bool Editable { get; set; }

        [Required]
        public Enums.EstatusJuego Estatus { get; set; }

        [ForeignKey("EstadioModel")]
        [Column("ClaveEstadio")]
        public string ClaveEstadio { get; set; }
        public EstadioModel EstadioModel { get; set; }

        [ForeignKey("EquipoModel1")]
        [Column("ClaveEquipo1")]
        public string ClaveEquipo1 { get; set; }
        public EquipoModel EquipoModel1 { get; set; }

        [ForeignKey("EquipoModel2")]
        [Column("ClaveEquipo2")]
        public string ClaveEquipo2 { get; set; }
        public EquipoModel EquipoModel2 { get; set; }

        [ForeignKey("MarcardorModel")]
        [Column("ClaveMarcador")]
        public string ClaveMarcador { get; set; }
        public MarcardorModel MarcardorModel { get; set; }
    }
}
