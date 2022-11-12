using ASPNetCore6Identity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Quinela_TPD.Models;
using Quinela_TPD.Repository.Interfaces;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace ASPNetCore6Identity.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly ICodigoPromocionalRepository codigoPromocionalRepository;
        private readonly IGenerarCodigosPromocionalesRepository generarCodigosPromocionalesRepository;
        private readonly IEmailConfiguracionRepository emailConfiguracionRepository;


        public HomeController(ILogger<HomeController> logger,
            ICodigoPromocionalRepository codigoPromocionalRepository,
            IGenerarCodigosPromocionalesRepository generarCodigosPromocionalesRepository,
            IEmailConfiguracionRepository emailConfiguracionRepository)
        {
            _logger = logger;
            this.codigoPromocionalRepository = codigoPromocionalRepository;
            this.generarCodigosPromocionalesRepository = generarCodigosPromocionalesRepository;
            this.emailConfiguracionRepository = emailConfiguracionRepository;
        }

        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("Rol") == "ClienteDependiente")//Evia a quiniela si es cliente dependiente
            {
                var puntaje = await codigoPromocionalRepository.GetIncludesByClaveAsync(HttpContext.Session.GetString("Usuario"));
                int puntos = 0;

                if (puntaje == null)
                {
                    puntos = 0;
                }
                else
                {
                    puntos = (int)puntaje.PuntajeQuinela;
                }

                HttpContext.Session.SetInt32("Puntaje", puntos);
                HttpContext.Session.SetString("NombreQuiniela", puntaje.UsuarioModel.Nombre);
                HttpContext.Session.SetInt32("Posicion", (int)puntaje.posicion);

                return RedirectToAction("Index", "Quinielas", new { codigoPromocional = HttpContext.Session.GetString("Usuario") });
            }

            var codigosPromocionales = codigoPromocionalRepository.GetAllIncludes();

            if (HttpContext.Session.GetString("Rol") != null)//Evia a codigos si es cliente
            {
                if (HttpContext.Session.GetString("Rol") != "Administrador")
                {
                    var calculos =
                        codigosPromocionales.Cast<CodigoPromocionalModel>()
                        .Where(w => w.Cliente == HttpContext.Session.GetString("Usuario")
                            && w.Utilizado == Models.Helper.Enums.EstatusCodigoPromocional.Utilizado)
                        .OrderByDescending(o => o.PuntajeQuinela)
                       .FirstOrDefault();

                    if (calculos != null)
                    {
                        HttpContext.Session.SetInt32("Puntaje", (int)calculos.PuntajeQuinela);
                        //HttpContext.Session.SetString("NombreQuiniela", );
                        HttpContext.Session.SetInt32("Posicion", (int)calculos.posicion);
                    }
                    else
                    {
                        HttpContext.Session.SetInt32("Puntaje", 0);
                        //HttpContext.Session.SetString("NombreQuiniela", );
                        HttpContext.Session.SetInt32("Posicion", 0);
                    }

                    codigosPromocionales = codigosPromocionales.Cast<CodigoPromocionalModel>().Where(w => w.Cliente == HttpContext.Session.GetString("Usuario")).OrderBy(o => o.Consecutivo).ToList();

                    var cantidadfaltante = generarCodigosPromocionalesRepository
                      .GetAll()
                      .Where(w => w.Cliente == HttpContext.Session.GetString("UsuarioNombre"))
                      .Select(s => s.CantidadFaltante)
                      .FirstOrDefault();

                    HttpContext.Session.SetString("FaltanteQuiniela", cantidadfaltante.ToString());

                }
                else
                {
                    HttpContext.Session.SetString("FaltanteQuiniela", "0");

                    codigosPromocionales = codigosPromocionales.Cast<CodigoPromocionalModel>().OrderBy(o => o.Cliente).ThenBy(t => t.Consecutivo).ToList();

                }
                return View(codigosPromocionales);
            }
            else
            {
                return RedirectToAction("LogIn", "Usuario");
            }

        }

        public async Task<IActionResult> CompartirCodigo(string clave, string email, string comentario, string nombre)
        {

            var codigoPromocional = await codigoPromocionalRepository.GetByClaveAsync(clave);

            codigoPromocional.EmailCliente = email;
            codigoPromocional.Comentarios = comentario;
            codigoPromocional.Nombre = nombre;
            codigoPromocional.PuntajeQuinela = 0;
            codigoPromocional.Utilizado = Models.Helper.Enums.EstatusCodigoPromocional.Compartido;

            await codigoPromocionalRepository.UpdateAsync(codigoPromocional);

            SendEmail(email, clave, comentario);

            return RedirectToAction("Index", "Home");

        }

        //[Route("{claveCodigo}")]
        public async Task<IActionResult> UtilizarCodigo(string claveCodigo, string nombre, string comentario)
        {

            var codigoPromocional = await codigoPromocionalRepository.GetByClaveAsync(claveCodigo);

            if (codigoPromocional == null)
            {
                return RedirectToAction("Index", "Home");
            }

            codigoPromocional.PuntajeQuinela = 0;
            codigoPromocional.Nombre = codigoPromocional.Codigo;
            codigoPromocional.Utilizado = Models.Helper.Enums.EstatusCodigoPromocional.Utilizado;
            codigoPromocional.Nombre = nombre;
            codigoPromocional.Comentarios = comentario;

            await codigoPromocionalRepository.UpdateAsync(codigoPromocional);

            return RedirectToAction("Index", "Home");

        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Premios()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public void SendEmail(string emailDestino, string codigoPromocional, string comentario)
        {
            try
            {
                //Get variables del correo
                EmailConfiguracionModel email = emailConfiguracionRepository.GetAll().Where(w => w.Clave == "1").FirstOrDefault();

                //Get variables del correo
                string emailOrigen = email.Email;//"avisos@tractopartesdiamante.com.mx";
                string password = email.Password;//"r_$/kld_1H34@_d%m&3xS";
                int port = Convert.ToInt32(email.Port);//587;
                string host = email.Host; //"servidor3315.tl.controladordns.com";

                string asunto = "TractoPartes Diamante Invita";
                string mensjae = "Código Promocional " + codigoPromocional + " " + comentario;

                //1 origen 2 destino 3 asunto 4 mensaje
                MailMessage mailMessage = new MailMessage(emailOrigen, emailDestino, asunto, mensjae);
                //configuracionde correo
                mailMessage.IsBodyHtml = true;
                mailMessage.Priority = MailPriority.High;
                //mailMessage.Attachments.Add()// agrega archivos

                SmtpClient smtpClient = new SmtpClient(); //("servidor3315.tl.controladordns.com", 587);//("mail.tractopartesdiamante.com.mx", 587);

                //Configuracion de comunicaciones
                smtpClient.Credentials = new System.Net.NetworkCredential(emailOrigen, password);
                smtpClient.Port = port;
                smtpClient.Host = host;
                smtpClient.EnableSsl = true;
                smtpClient.Timeout = 10000;

                // Ahora creamos la vista para clientes que 
                // pueden mostrar contenido HTML...
                string html = "<h2>Te han compartido un Código promocional <strong>" + codigoPromocional + "</strong> para participar en la quiniela del mundial 2022 de Tracto Partes Diamante de Puebla</h2>" +
                              "</hr>" +
                              "<h2>" + comentario + "</h2>" +
                              "<h3>Accesa a esta liga https://diamante.dyndns-ip.com:5000/con tu código como usuario, escribe una contraseña y ¡listo!</h3>";

                AlternateView htmlView =
                    AlternateView.CreateAlternateViewFromString(html,
                                            Encoding.UTF8,
                                            MediaTypeNames.Text.Html);

                // Creamos el recurso a incrustar. Observad
                // que el ID que le asignamos (arbitrario) está
                // referenciado desde el código HTML como origen
                // de la imagen (resaltado en amarillo)...
                //var path = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);



                // Por último, vinculamos ambas vistas al mensaje...
                mailMessage.AlternateViews.Add(htmlView);


                //Envia y finaliza el correo
                smtpClient.Send(mailMessage);
                smtpClient.Dispose();
            }
            catch (Exception ex)
            {
                var ms = ex.Message;
            }
        }

        public IActionResult Posiciones()
        {
            var codigosPromocionales = codigoPromocionalRepository.GetAllIncludes();

            if (HttpContext.Session.GetString("Rol") != null)
            {
                codigosPromocionales = codigosPromocionales.Cast<CodigoPromocionalModel>().OrderBy(o => o.posicion).ToList();

                return View(codigosPromocionales);
            }
            else
            {
                return RedirectToAction("LogIn", "Usuario");
            }

        }
    }
}
