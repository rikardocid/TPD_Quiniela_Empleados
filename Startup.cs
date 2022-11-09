using ASPNetCore6Identity.Data;
using ASPNetCore6Identity.Models.Auth;
using ASPNetCore6Identity.Services;
using ASPNetCore6Identity.Services.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quinela_TPD.Repository.Implementations;
using Quinela_TPD.Repository.Interfaces;
using System;

namespace ASPNetCore6Identity
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Configurar las reglas de los usuarios y agregar identity
            services.AddControllersWithViews();

            services.AddIdentity<ApplicationUser, IdentityRole>(cfg =>
            {
                //cfg.Tokens.AuthenticatorTokenProvider = TokenOptions.DefaultAuthenticatorProvider;
                cfg.SignIn.RequireConfirmedEmail = false;
                cfg.User.RequireUniqueEmail = false;
                cfg.Password.RequireDigit = false;
                cfg.Password.RequiredUniqueChars = 0;
                cfg.Password.RequireLowercase = false;
                cfg.Password.RequireNonAlphanumeric = false;
                cfg.Password.RequireUppercase = false;
            })
            .AddDefaultTokenProviders()
            .AddEntityFrameworkStores<ApplicationDbContext>();


            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddDatabaseDeveloperPageExceptionFilter();

            //services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
            //    .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddControllersWithViews();

            //Agregar la configuracion del http
            services.AddHttpClient();

            // Inyectar para poder acceder al usuario actual
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddTransient<SeedDB>();

            services.AddScoped<IUserHelper, UserHelper>();

            services.AddControllers();

            services.AddHealthChecks();

            services.AddSession(o =>
            {
                o.IdleTimeout = TimeSpan.FromDays(1);
                o.Cookie.HttpOnly = true;
                o.Cookie.IsEssential = true;

            });

            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IJuegoRepository, JuegoRepository>();
            services.AddScoped<IEquipoRepository, EquipoRepository>();
            services.AddScoped<IEstadioRepository, EstadioRepository>();
            services.AddScoped<IGenerarCodigosPromocionalesRepository, GenerarCodigosPromocionalesRepository>();
            services.AddScoped<ICodigoPromocionalRepository, CodigoPromocionalRepository>();
            services.AddScoped<IQuinielaRepository, QuinielaRepository>();
            services.AddScoped<IMarcadorRepository, MarcadorRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Usuarios/LogIn");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseSession();
            app.UseAuthentication();//se utiliza para autenticacion
            app.UseAuthorization();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllerRoute(
            //        name: "default",
            //        pattern: "{controller=Home}/{action=Index}/{id?}");
            //});

            //app.MapRazorPages();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Usuario}/{action=LogIn}/{id?}");
                // endpoints.MapRazorPages();
            });

        }
    }
}
