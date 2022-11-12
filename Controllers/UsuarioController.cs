using ASPNetCore6Identity.Models;
using ASPNetCore6Identity.Models.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Quinela_TPD.Models;
using Quinela_TPD.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace Quinela_TPD.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioRepository usuarioRepository;
        private readonly IGenerarCodigosPromocionalesRepository generarCodigosPromocionalesRepository;
        private readonly ICodigoPromocionalRepository codigoPromocionalRepository;
        private readonly IEmailConfiguracionRepository emailConfiguracionRepository;
        string AlertaMensaje = "";

        public UsuarioController(IUsuarioRepository usuarioRepository,
            IGenerarCodigosPromocionalesRepository generarCodigosPromocionalesRepository,
            ICodigoPromocionalRepository codigoPromocionalRepository,
            IEmailConfiguracionRepository emailConfiguracionRepository)
        {
            this.usuarioRepository = usuarioRepository;
            this.generarCodigosPromocionalesRepository = generarCodigosPromocionalesRepository;
            this.codigoPromocionalRepository = codigoPromocionalRepository;
            this.emailConfiguracionRepository = emailConfiguracionRepository;
        }

        [HttpGet]
        public async Task<IActionResult> LogIn()
        {
            try
            {
                /*aca se tiene que ejecutar el SP y al final enviar el correo a los clientes*/
                //usuarioRepository.GetExtraerCodigosPromocionales();

                var generarCodigosPromocionalesList = generarCodigosPromocionalesRepository
                    .GetAll()
                    .Where(w => w.Codigos > 0);

                if (generarCodigosPromocionalesList.Count() != 0)
                {
                    GenerarCodigosEmail(generarCodigosPromocionalesList.ToList());
                }

                int ContadorCodigos = 0;

                List<CodigoPromocionalModel> codigosPromocionalesModelList = new();
                List<GenerarCodigosPromocionalesModel> generarCodigosPromocionalesModelList = new();

                foreach (var item in generarCodigosPromocionalesList)
                {

                    int codigosProm = codigoPromocionalRepository.GetAll().Where(w => w.Cliente == item.Cliente).Count();
                    for (int i = 0; i < item.Codigos; i++)
                    {
                        codigosProm++;
                        CodigoPromocionalModel codigoPromocionalModel = new();
                        var codigo = Guid.NewGuid().ToString("N").Substring(0, 10);
                        codigoPromocionalModel.Clave = codigo;
                        codigoPromocionalModel.EmailCliente = item.EmailCliente;
                        codigoPromocionalModel.Cliente = item.Cliente;
                        codigoPromocionalModel.Codigo = codigo;
                        codigoPromocionalModel.PuntajeQuinela = 0;
                        codigoPromocionalModel.posicion = 0;
                        codigoPromocionalModel.Consecutivo = codigosProm;

                        codigosPromocionalesModelList.Add(codigoPromocionalModel);

                        ContadorCodigos = i + 1;
                    }

                    item.Codigos = item.Codigos - ContadorCodigos;
                    item.CodigosGenerados = item.CodigosGenerados + ContadorCodigos;
                    generarCodigosPromocionalesModelList.Add(item);
                }

                await generarCodigosPromocionalesRepository.UpdateRangeAsync(generarCodigosPromocionalesModelList);
                await codigoPromocionalRepository.AddRangeAsync(codigosPromocionalesModelList);

                HttpContext.Session.Clear();

                //Thread Hilo = new Thread(new ThreadStart(() => GenerarCodigosEmail(generarCodigosPromocionalesList.ToList())));
            }
            catch (Exception)
            {

                throw;
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(string username, string password)
        {
            HttpContext.Session.Clear();

            var existe = usuarioRepository
                .GetAll()
                .Where(w => w.Usuario == username &&
                    //w.Password == password &&
                    w.Estatus == Enums.EstatusAI.Activo)
                .FirstOrDefault();

            if (existe != null)
            {
                // existe = usuarioRepository
                //.GetAll()
                //.Where(w => w.Usuario == username &&
                //    w.Password == password &&
                //    w.Estatus == Enums.EstatusAI.Activo)
                //.FirstOrDefault();

                if (existe.Password.ToUpper().Contains(password.ToUpper()))
                {
                    existe.UltimoAcceso = DateTime.Now;
                    await usuarioRepository.UpdateAsync(existe);

                    HttpContext.Session.SetString("Usuario", existe.Usuario);
                    HttpContext.Session.SetString("UsuarioNombre", existe.Nombre);
                    HttpContext.Session.SetString("Rol", existe.Rol.ToString());

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    AlertaMensaje = "Datos de usuario invalidos";
                }
            }
            else
            {
                var promocionaleVigente = codigoPromocionalRepository
                        .GetAll()
                        .Where(w => w.Utilizado == Enums.EstatusCodigoPromocional.Compartido &&
                            w.Codigo == username)
                        .FirstOrDefault();

                if (promocionaleVigente == null)//Crea cliente 
                {
                    var cliente = codigoPromocionalRepository
                        .GetAll()
                        .Where(w => w.Cliente == username)
                        .FirstOrDefault();

                    if (cliente != null)
                    {
                        if (password == "")
                        {
                            AlertaMensaje = "Agregue una contraseña para continuar";
                        }
                        else
                        {
                            var rfc = generarCodigosPromocionalesRepository.GetAll().Where(w => w.Cliente == username).FirstOrDefault();

                            if (rfc.RFC == password)
                            {
                                UsuarioModel usuarioModel = new();
                                usuarioModel.Clave = Guid.NewGuid().ToString("N");
                                usuarioModel.Usuario = username;
                                usuarioModel.Password = rfc.RFC;
                                usuarioModel.Cabecera = true;
                                usuarioModel.Estatus = Enums.EstatusAI.Activo;
                                usuarioModel.Rol = Enums.Roles.Cliente;
                                usuarioModel.Email = cliente.EmailCliente;
                                usuarioModel.Nombre = cliente.Cliente;
                                usuarioModel.UltimoAcceso = DateTime.Now;

                                var UsuarioNuevo = await usuarioRepository.CreateAsync(usuarioModel);

                                //promocionaleVigente.Utilizado = Enums.EstatusCodigoPromocional.Enviado;
                                //promocionaleVigente.ClaveUsuario = usuarioModel.Clave;
                                //await codigoPromocionalRepository.UpdateAsync(promocionaleVigente);

                                HttpContext.Session.SetString("Usuario", UsuarioNuevo.Usuario);
                                HttpContext.Session.SetString("Rol", UsuarioNuevo.Rol.ToString());
                                HttpContext.Session.SetString("UsuarioNombre", UsuarioNuevo.Nombre);

                                return RedirectToAction("Index", "Home");
                            }
                            else
                            {
                                AlertaMensaje = "RFC incorrecto";
                            }
                        }
                    }
                    else
                    {
                        AlertaMensaje = "Los datos son incorrectos ó el código promocional ya ha sido utilizado";
                    }
                }
                else
                {//crea cliente depnediente
                    if (password == "")
                    {
                        AlertaMensaje = "Agregue una contraseña para continuar";
                    }
                    else
                    {
                        UsuarioModel usuarioModel = new();
                        usuarioModel.Clave = Guid.NewGuid().ToString("N");
                        usuarioModel.Usuario = username;
                        usuarioModel.Password = password;
                        usuarioModel.Cabecera = false;
                        usuarioModel.Estatus = Enums.EstatusAI.Activo;
                        usuarioModel.Rol = Enums.Roles.ClienteDependiente;
                        usuarioModel.Email = promocionaleVigente.EmailCliente;//TODO se agrega cuando se enviar el email
                        usuarioModel.Nombre = promocionaleVigente.Nombre;//TODO se agrega cuando se enviar el email
                        usuarioModel.UltimoAcceso = DateTime.Now;

                        var UsuarioNuevo = await usuarioRepository.CreateAsync(usuarioModel);

                        promocionaleVigente.Utilizado = Enums.EstatusCodigoPromocional.Compartido;
                        promocionaleVigente.ClaveUsuario = usuarioModel.Clave;
                        promocionaleVigente.PuntajeQuinela = 0;

                        await codigoPromocionalRepository.UpdateAsync(promocionaleVigente);

                        HttpContext.Session.SetString("Usuario", UsuarioNuevo.Usuario);
                        HttpContext.Session.SetString("Rol", UsuarioNuevo.Rol.ToString());
                        HttpContext.Session.SetString("UsuarioNombre", UsuarioNuevo.Nombre);

                        return RedirectToAction("Index", "Home");//TODO este solo envia a su quiniela
                    }
                }
            }

            TempData["alert"] = AlertaMensaje;//"Los datos son increctos";
            return View();
        }

        void GenerarCodigosEmail(List<GenerarCodigosPromocionalesModel> generarCodigosPromocionalesList)
        {
            foreach (var item in generarCodigosPromocionalesList)
            {
                SendEmail(item.EmailCliente, item.Cliente);
            }
        }

        public void SendEmail(string emailDestino, string noCliente)
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
                string asunto = "Tracto Partes Diamante Invita";
                string mensjae = "Código Promocional ";

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
                string html = "<h2>Felicidades" + noCliente + " ha ganado más códigos promocionales para participar en la quiniela de Tracto Partes Diamante de Puebla  </h2>" +
                              "</hr>" +
                              "<h2>¡Hay increibles premios!</h2>" +
                              "</hr>" +
                              "<h1>¡Mucha suerte!</h1>" +
                              "<h3>Accesa a esta liga https://quiniela2022.clienteleal.com/ con tu código como usuario <strong>" + noCliente + "</strong>, tu RFC como contraseña y ¡listo!</h3>";

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
                throw;
            }
        }

        public ActionResult OlvidoPass(string cliente)
        {
            try
            {
                var emailDestino = usuarioRepository.GetAll().Where(w => w.Usuario == cliente).FirstOrDefault();

                if (emailDestino == null)
                {
                    UsuarioModel userEmail = new();
                    emailDestino = userEmail;
                    var codigoGenerado = generarCodigosPromocionalesRepository.GetAll().Where(w => w.Cliente == cliente).FirstOrDefault();
                    emailDestino.Email = codigoGenerado.EmailCliente;
                    emailDestino.Password = codigoGenerado.RFC;
                    emailDestino.Usuario = codigoGenerado.Cliente;
                }


                if (emailDestino != null)
                {
                    //Get variables del correo
                    EmailConfiguracionModel email = emailConfiguracionRepository.GetAll().Where(w => w.Clave == "1").FirstOrDefault();

                    //Get variables del correo
                    string emailOrigen = email.Email;//"avisos@tractopartesdiamante.com.mx";
                    string password = email.Password;//"r_$/kld_1H34@_d%m&3xS";
                    int port = Convert.ToInt32(email.Port);//587;
                    string host = email.Host; //"servidor3315.tl.controladordns.com";
                    string asunto = "Recuperar contraseña";
                    string mensjae = "Código y contraseña";

                    //1 origen 2 destino 3 asunto 4 mensaje
                    MailMessage mailMessage = new MailMessage(emailOrigen, emailDestino.Email, asunto, mensjae);
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

                    string html = "<h2>Su usuario es: " + emailDestino.Usuario + "<br/> Su contraseña es: " + emailDestino.Password + "</h2>" +
                                  "</hr>" +
                                  "<h1>¡Mucha suerte!</h1>" +
                                  "<h3>Accesa a esta liga https://quiniela2022.clienteleal.com/ con tu código como usuario <strong>" + emailDestino.Usuario + "</strong>, escribe tu contraseña " + emailDestino.Password + " y ¡listo!</h3>" +
                                  "</hr>";

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
                    AlertaMensaje = "Se ha enviado al email registrado: " + emailDestino.Email;
                    TempData["alert"] = AlertaMensaje;
                }

            }
            catch (Exception ex)
            {
                var ms = ex.Message;
                throw;
            }
            return RedirectToAction("Login", "Usuario");
        }

        public async Task<ActionResult> CambiarPass(string passActual, string nuevoPassword)
        {
            var passExiste = usuarioRepository.GetAll().Where(w => w.Usuario == HttpContext.Session.GetString("Usuario")).FirstOrDefault();
            string AlertaMensaje = "";
            try
            {
                if (passExiste != null)
                {
                    if (passExiste.Password == passActual)
                    {
                        passExiste.Password = nuevoPassword;
                        await usuarioRepository.UpdateAsync(passExiste);
                        AlertaMensaje = "Se cambio la contrasña correctamente";
                    }
                    else
                    {
                        AlertaMensaje = "La contraseña actual es incorrecta";
                    }
                }
                else
                {
                    return RedirectToAction("Login", "Usuario");
                }

            }
            catch (Exception)
            {

                throw;
            }
            TempData["alert"] = AlertaMensaje;

            return RedirectToAction("Index", "Home");
        }
    }
}