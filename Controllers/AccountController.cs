using ASPNetCore6Identity.Models.Auth;
using ASPNetCore6Identity.Models.Helper;
using ASPNetCore6Identity.Services.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ASPNetCore6Identity.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserHelper userHelper;

        public AccountController(IUserHelper userHelper)
        {
            this.userHelper = userHelper;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> IniciarSesion(LoginModel model)
        {
            var user = await this.userHelper.GetUserByUserNameAsync(model.Username);

            if (this.ModelState.IsValid)
            {

                if (user != null)
                {
                    var result = await this.userHelper.ValidatePasswordAsync(
                        user,
                        model.Password);

                    if (result.ToString().Contains("Failed"))
                    {
                        return RedirectToAction("login", "account");
                    }

                    //Crea las variables de sesion para el uso del sistema

                    return RedirectToAction("Index", "Home");
                }

                if (user.Estatus == Enums.EstatusAI.Inactivo)
                {
                    return RedirectToAction("login", "account");
                }
            }

            return View();
            ;
        }
    }
}
