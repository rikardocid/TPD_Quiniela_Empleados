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
    public class EquiposController : Controller
    {
        private readonly IEquipoRepository equipoRepository;
        public EquiposController(IEquipoRepository equipoRepository)
        {
            this.equipoRepository = equipoRepository;
        }

        public IActionResult Index()
        {
            var EstadiosList = equipoRepository.GetAll().ToList();
            return View(EstadiosList);
        }

        //[Route("{nombre}")]
        public async Task<IActionResult> Create(string nombre, IFormFile bandera, IFormFile escudo, string grupo)
        {
            var equipos = equipoRepository.GetAll().ToList();

            EquipoModel equipoModel = new();

            equipoModel.Nombre = nombre;
            equipoModel.Bandera = bandera.FileName;
            equipoModel.Escudo = escudo.FileName;
            equipoModel.Grupo = grupo;
            equipoModel.Clave = (equipos.Count() + 1).ToString();

            if (bandera != null)
            {
                UploadBandera(bandera, 1);
            }

            if (escudo != null)
            {
                UploadBandera(escudo, 2);
            }


            await equipoRepository.CreateAsync(equipoModel);

            return RedirectToAction("Index", "Equipos");
        }

        public async Task<IActionResult> Edit(string claveEdit, string nombreEdit, IFormFile banderaEdit, IFormFile escudoEdit, string grupoEdit)
        {
            var equipo = await equipoRepository.GetByClaveAsync(claveEdit);

            //equipo.Clave = clave;
            equipo.Nombre = nombreEdit;
            equipo.Bandera = banderaEdit.FileName;
            equipo.Escudo = escudoEdit.FileName;
            equipo.Grupo = grupoEdit;

            if (banderaEdit != null)
            {
                UploadBandera(banderaEdit, 1);
            }

            if (escudoEdit != null)
            {
                UploadBandera(escudoEdit, 2);
            }

            await equipoRepository.UpdateAsync(equipo);

            return RedirectToAction("Index", "Equipos");
        }

        //[HttpPost("UploadDocumento")]
        //[DisableRequestSizeLimit, RequestFormLimits(MultipartBodyLengthLimit = int.MaxValue, ValueLengthLimit = int.MaxValue)] //OMITIR ESTO
        public IActionResult UploadBandera(IFormFile formFile, int identificador)
        {
            string filePath = "";

            if (identificador == 1)
            {
                filePath = @"wwwroot\img\Banderas";
            }
            else
            {
                filePath = @"wwwroot\img\Escudos";
            }

            string rutaDocumento = Path.Combine(filePath, formFile.FileName);

            try
            {
                //System.IO
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