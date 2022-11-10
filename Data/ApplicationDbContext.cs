using ASPNetCore6Identity.Models;
using ASPNetCore6Identity.Models.Auth;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Quinela_TPD.Models;

namespace ASPNetCore6Identity.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<EquipoModel> equipoModels { get; set; }
        public DbSet<CodigoPromocionalModel> codigoPromocionalModels { get; set; }
        public DbSet<EstadioModel> estadioModels { get; set; }
        public DbSet<GenerarCodigosPromocionalesModel> generarCodigosPromocionales { get; set; }
        public DbSet<JuegoModel> juegoModels { get; set; }
        public DbSet<MarcardorModel> marcardorModels { get; set; }
        public DbSet<QuinielaModel> quinielaModels { get; set; }
        public DbSet<UsuarioModel> usuarioModels { get; set; }
        public DbSet<EmailConfiguracionModel> emailConfiguracionModels { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
    }
}
