using ASPNetCore6Identity.Models.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Quinela_TPD.Models;
using Quinela_TPD.Repository.Interfaces;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Quinela_TPD.Controllers
{
    public class EstadiosController : Controller
    {
        private readonly IEstadioRepository estadioRepository;
        public EstadiosController(IEstadioRepository estadioRepository)
        {
            this.estadioRepository = estadioRepository;
        }

        public IActionResult Index()
        {
            var EstadiosList = estadioRepository.GetAll().ToList();
            return View(EstadiosList);
        }

        //[Route("{nombre}")]
        public async Task<IActionResult> Create(string nombre, IFormFile imagen)
        {
            var estadiosList = estadioRepository.GetAll().ToList();
            EstadioModel estadioModel = new();


            if (imagen != null)
            {
                UploadImagen(imagen);
                estadioModel.Imagen = imagen.FileName;
            }


            estadioModel.Clave = (estadiosList.Count() + 1).ToString();
            estadioModel.Nombre = nombre;

            await estadioRepository.CreateAsync(estadioModel);

            return RedirectToAction("index", "estadios");
        }

        public async Task<IActionResult> Edit(string clave, string nombre, IFormFile imagen)
        {
            var estadio = await estadioRepository.GetByClaveAsync(clave);

            if (imagen != null)
            {
                UploadImagen(imagen);
                estadio.Imagen = imagen.FileName;
            }

            estadio.Clave = clave;
            estadio.Nombre = nombre;

            await estadioRepository.UpdateAsync(estadio);

            return RedirectToAction("index", "estadios");
        }


        public IActionResult UploadImagen(IFormFile formFile)
        {
            string filePath = @"wwwroot\img\Estadios";
            string rutaDocumento = Path.Combine(filePath, formFile.FileName);

            try
            {
                //System.IO

                using (FileStream newFile = System.IO.File.Create(rutaDocumento))
                {
                    formFile.CopyTo(newFile);
                    newFile.Flush();
                }

                return StatusCode(StatusCodes.Status200OK, new Response() { Message = "Documento cargado", IsSuccess = true, Result = formFile.FileName.ToString() });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response() { Message = error.Message, IsSuccess = false });
            }
        }

    }
}
