using ASPNetCore6Identity.Models.Auth;
using ASPNetCore6Identity.Models.Helper;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNetCore6Identity.Services.Helpers
{
    public class UserHelper : IUserHelper
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;


        public UserHelper(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }

        //Agregar usuario 
        public async Task<IdentityResult> AddUserAsync(ApplicationUser user, string password)
        {
            return await this.userManager.CreateAsync(user, password);
        }

        //Regresa los roles
        public IQueryable<IdentityRole> ListRoles()
        {
            var roles = roleManager.Roles;
            return roles;
        }

        //Obtener usuario por email
        public async Task<ApplicationUser> GetUserByUserNameAsync(string userName)
        {

            var user = this.userManager.Users
                .Where(w => w.UserName.Equals(userName) &&
                w.Estatus == Enums.EstatusAI.Activo) //traemos el usuario si coincide el username y que cuente con estatus activo
                .OrderBy(u => u.UserName)
                .ThenBy(u => u.Email)
                .FirstOrDefault();

            if (user != null)
                user.UserRoles = await this.userManager.GetRolesAsync(user);
            return user;

        }

        //Iniciar sesiòn un usuario (Sirve para el manejo en aplicaciones MVC)
        public async Task<SignInResult> LoginAsync(LoginModel model)
        {
            return await this.signInManager.PasswordSignInAsync(
                model.Username,
                model.Password,
                model.RememberMe,
                false);
        }

        //Cerrar sesiòn un usuario (Sirve para el manejo en aplicaciones MVC)
        public async Task LogoutAsync()
        {
            await this.signInManager.SignOutAsync();
        }

        //Actualizar usuario
        public async Task<IdentityResult> UpdateUserAsync(ApplicationUser user)
        {
            return await this.userManager.UpdateAsync(user);
        }

        // Cambio de contraseña
        public async Task<IdentityResult> ChangePasswordAsync(ApplicationUser user, string oldPassword, string newPassword)
        {
            return await this.userManager.ChangePasswordAsync(user, oldPassword, newPassword);
        }

        public async Task<IdentityResult> OverridePasswordAsync(ApplicationUser user, string newPassword)
        {
            await this.userManager.RemovePasswordAsync(user);
            return await this.userManager.AddPasswordAsync(user, newPassword);
        }

        // Validar usuario (Funciona en las API)
        public async Task<SignInResult> ValidatePasswordAsync(ApplicationUser user, string password)
        {
            return await this.signInManager.CheckPasswordSignInAsync(
                user,
                password,
                false);
        }

        public async Task CheckRoleAsync(string roleName)
        {
            //var Roles = await
            var roleExists = await this.roleManager.RoleExistsAsync(roleName);
            if (!roleExists)
            {
                await this.roleManager.CreateAsync(new IdentityRole
                {
                    Name = roleName
                });
            }
        }

        public async Task AddRole(string roleName)
        {
            var roleExists = await this.roleManager.RoleExistsAsync(roleName);
            if (!roleExists)
            {
                await this.roleManager.CreateAsync(new IdentityRole
                {
                    Name = roleName
                });
            }
        }

        public async Task AddUserToRoleAsync(ApplicationUser user, string roleName)
        {
            await this.userManager.AddToRoleAsync(user, roleName);
        }

        public async Task<IList<string>> GetUserRolesAsync(ApplicationUser user)
        {
            return await this.userManager.GetRolesAsync(user);
        }

        public async Task<bool> IsUserInRoleAsync(ApplicationUser user, string roleName)
        {
            return await this.userManager.IsInRoleAsync(user, roleName);
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(ApplicationUser user)
        {
            return await this.userManager.GenerateEmailConfirmationTokenAsync(user);
        }

        public async Task<IdentityResult> ConfirmEmailAsync(ApplicationUser user, string token)
        {
            return await this.userManager.ConfirmEmailAsync(user, token);
        }

        public async Task<ApplicationUser> GetUserByIdAsync(string userId)
        {

            /*
            var user = await this.userManager.FindByIdAsync(userId);
            if (user != null)
                user.UserRoles = await this.userManager.GetRolesAsync(user);
            return user;
            */

            var user = this.userManager.Users
                .Where(w => w.Id.Equals(userId))
                .OrderBy(u => u.UserName)
                .ThenBy(u => u.Email)
                .FirstOrDefault();

            if (user != null)
                user.UserRoles = await this.userManager.GetRolesAsync(user);
            return user;

        }

        public async Task<string> GeneratePasswordResetTokenAsync(ApplicationUser user)
        {
            return await this.userManager.GeneratePasswordResetTokenAsync(user);
        }

        public async Task<IdentityResult> ResetPasswordAsync(ApplicationUser user, string token, string password)
        {
            return await this.userManager.ResetPasswordAsync(user, token, password);
        }

        public async Task<List<ApplicationUser>> GetAllUsersAsync()
        {
            var users = this.userManager.Users
                .OrderBy(u => u.UserName)
                .ThenBy(u => u.Email)
                .ToList();

            var SHOW = users;

            foreach (var user in users)
            {
                user.UserRoles = await userManager.GetRolesAsync(user);
            }

            return users;
        }

        public async Task RemosUserFromRoleAsync(ApplicationUser user, string roleName)
        {
            await this.userManager.RemoveFromRoleAsync(user, roleName);
        }

        public async Task DeleteUserAsync(ApplicationUser user)
        {
            await this.userManager.DeleteAsync(user);
        }

    }
}
