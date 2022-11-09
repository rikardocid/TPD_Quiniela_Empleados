using ASPNetCore6Identity.Models.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Quinela_TPD.Models;
using Quinela_TPD.Models.ModelView;
using Quinela_TPD.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Quinela_TPD.Controllers
{
    public class JuegosController : Controller
    {
        private readonly IJuegoRepository juegoRepository;
        private readonly IEquipoRepository equipoRepository;
        private readonly IEstadioRepository estadioRepository;
        public JuegosController(IJuegoRepository juegoRepository,
            IEquipoRepository equipoRepository,
            IEstadioRepository estadioRepository)
        {
            this.juegoRepository = juegoRepository;
            this.equipoRepository = equipoRepository;
            this.estadioRepository = estadioRepository;
        }

        public IActionResult IndexAsync()
        {

            List<EquipoModel> equipoModelList = this.equipoRepository.GetAll().ToList();
            List<EstadioModel> estadiosModelList = this.estadioRepository.GetAll().ToList();
            var juegosList = juegoRepository.GetAllIncludes().OrderBy(o => o.FechaJuego).ToList();

            JuegosVM juegoVM = new()
            {

                JuegoList = juegosList,
                JuegoModel = new(),
                Equipo1 = equipoModelList.Select(a => new SelectListItem
                {
                    Text = a.Nombre,
                    Value = a.Clave
                }),
                Equipo2 = equipoModelList.Select(a => new SelectListItem
                {
                    Text = a.Nombre,
                    Value = a.Clave
                }),
                Estadio = estadiosModelList.Select(a => new SelectListItem
                {
                    Text = a.Nombre,
                    Value = a.Clave
                })
            };

            return View(juegoVM);
        }

        public async Task<IActionResult> Create(string estadio, DateTime fechaJuego, int fase, string equipo1, string equipo2, bool editable)
        {
            //var equipos = juegoRepository.GetAll().ToList();

            JuegoModel juegoModel = new();
            juegoModel.Clave = Guid.NewGuid().ToString("N");
            juegoModel.Estatus = Enums.EstatusJuego.Pendiente;
            juegoModel.FechaJuego = fechaJuego;
            juegoModel.ClaveEquipo1 = equipo1;
            juegoModel.ClaveEquipo2 = equipo2;
            juegoModel.ClaveEstadio = estadio;
            juegoModel.Fase = fase;

            await juegoRepository.CreateAsync(juegoModel);

            return RedirectToAction("Index", "Juegos");
        }

        public async Task<IActionResult> Edit(string claveEditar, string estadio, DateTime fechaJuegoEditar, int faseEditar, string equipo1, string equipo2, bool editable)
        {
            //var equipos = juegoRepository.GetAll().ToList();

            JuegoModel juegoModel = new();
            juegoModel.Clave = claveEditar;
            juegoModel.Estatus = Enums.EstatusJuego.Pendiente;
            juegoModel.FechaJuego = fechaJuegoEditar;
            juegoModel.ClaveEquipo1 = equipo1;
            juegoModel.ClaveEquipo2 = equipo2;
            juegoModel.ClaveEstadio = estadio;
            juegoModel.Fase = faseEditar;
            juegoModel.Editable = editable;

            await juegoRepository.UpdateAsync(juegoModel);

            return RedirectToAction("Index", "Juegos");
        }
    }
}