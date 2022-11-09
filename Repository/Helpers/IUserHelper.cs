using ASPNetCore6Identity.Models.Auth;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNetCore6Identity.Services.Helpers
{
    public interface IUserHelper
    {
        Task<ApplicationUser> GetUserByUserNameAsync(string userName);

        Task<IdentityResult> AddUserAsync(ApplicationUser applicationUser, string password);

        Task<SignInResult> LoginAsync(LoginModel model);

        Task LogoutAsync();

        Task<IdentityResult> UpdateUserAsync(ApplicationUser applicationUser);

        Task<IdentityResult> ChangePasswordAsync(ApplicationUser applicationUser, string oldPassword, string newPassword);

        Task<IdentityResult> OverridePasswordAsync(ApplicationUser applicationUser, string newPassword);

        Task<SignInResult> ValidatePasswordAsync(ApplicationUser applicationUser, string password);

        Task CheckRoleAsync(string roleName);

        Task AddUserToRoleAsync(ApplicationUser applicationUser, string roleName);

        Task<IList<string>> GetUserRolesAsync(ApplicationUser applicationUser);

        Task<bool> IsUserInRoleAsync(ApplicationUser applicationUser, string roleName);

        Task<string> GenerateEmailConfirmationTokenAsync(ApplicationUser applicationUser);

        Task<IdentityResult> ConfirmEmailAsync(ApplicationUser applicationUser, string token);

        Task<ApplicationUser> GetUserByIdAsync(string applicationUserId);

        Task<string> GeneratePasswordResetTokenAsync(ApplicationUser applicationUser);

        Task<IdentityResult> ResetPasswordAsync(ApplicationUser applicationUser, string token, string password);

        Task<List<ApplicationUser>> GetAllUsersAsync();

        Task RemosUserFromRoleAsync(ApplicationUser applicationUser, string roleName);

        Task DeleteUserAsync(ApplicationUser applicationUser);

        public IQueryable<IdentityRole> ListRoles();

        public Task AddRole(string roleName);
    }
}
