using ASPNetCore6Identity.Data;
using ASPNetCore6Identity.Models.Auth;
using ASPNetCore6Identity.Models.Helper;
using ASPNetCore6Identity.Services.Helpers;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace ASPNetCore6Identity.Services
{
    public class SeedDB
    {
        private readonly ApplicationDbContext context;
        private readonly IUserHelper userHelper;

        public SeedDB(ApplicationDbContext context, IUserHelper userHelper)
        {
            this.context = context;
            this.userHelper = userHelper;
        }

        public async Task SeedAsync()
        {
            // Principalmente cargar usuarios y roles

            await this.context.Database.EnsureCreatedAsync();
            //Recorre todos los roles, si no estan agrega los que faltan previamente configurados en los enums.roles
            foreach (var item in Enum.GetValues(typeof(Enums.Roles)))
            {
                await this.userHelper.CheckRoleAsync(item.ToString());//Enums.Roles.Administrador.ToString());
            }
            #region //busca que exista el usuario administrador, si no esta lo crea con un password predeterminado
            var user = await this.userHelper.GetUserByUserNameAsync("Administrador");
            if (user == null)
            {
                user = new ApplicationUser
                {
                    Nombre = "Administrador",
                    ApellidoPaterno = "Administrador",
                    ApellidoMaterno = "Administrador",
                    Email = "email@email.com",
                    UserName = "Administrador",
                    PhoneNumber = "222 222 2222",
                    Domicilio = ""
                    //UserRoles = new List<string>()
                };
                //crea usuario con datos predeterminados
                var result = await this.userHelper.AddUserAsync(user, "Admin123#");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("No fue posible crear el usuario en los datos de primer uso");
                }
                //añade el rol administrador al usuario creado predeterminadamnete
                await this.userHelper.AddUserToRoleAsync(user, Enums.Roles.Administrador.ToString());

                var token = await this.userHelper.GenerateEmailConfirmationTokenAsync(user);
                await this.userHelper.ConfirmEmailAsync(user, token);

                var isInRole = await this.userHelper.IsUserInRoleAsync(user, Enums.Roles.Administrador.ToString());
                if (!isInRole)
                {
                    await this.userHelper.AddUserToRoleAsync(user, Enums.Roles.Administrador.ToString());
                }
            }
            #endregion
        }
    }
}
