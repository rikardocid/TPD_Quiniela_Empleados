using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Quinela_TPD.Models;
using Quinela_TPD.Models.ModelView;
using Quinela_TPD.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quinela_TPD.Controllers
{
    public class QuinielasController : Controller
    {
        private readonly IQuinielaRepository quinielaRepository;
        private readonly IJuegoRepository juegoRepository;
        private readonly ICodigoPromocionalRepository codigoPromocionalRepository;

        public QuinielasController(IQuinielaRepository quinielaRepository,
            IJuegoRepository juegoRepository,
            ICodigoPromocionalRepository codigoPromocionalRepository)
        {
            this.quinielaRepository = quinielaRepository;
            this.juegoRepository = juegoRepository;
            this.codigoPromocionalRepository = codigoPromocionalRepository;
        }

        public async Task<IActionResult> Index(string codigoPromocional)
        {
            if (codigoPromocional == null)
            {
                codigoPromocional = HttpContext.Session.GetString("CodigoPromocional");
            }

            HttpContext.Session.SetString("CodigoPromocional", codigoPromocional);

            var quinielaByCodigo = quinielaRepository.GetAllIncludes().Where(w =>
                w.CodigoPromocionalModel.Clave == codigoPromocional).ToList();

            List<QuinielaModel> quinielaModelList = new();

            var juegosList = juegoRepository.GetAllIncludes().ToList();

            if (quinielaByCodigo.Count() == 0)
            {
                foreach (var item in juegosList)
                {
                    QuinielaModel quinielaModel = new QuinielaModel();

                    quinielaModel.Clave = Guid.NewGuid().ToString("N");
                    quinielaModel.ClaveJuego = item.Clave;
                    quinielaModel.ClaveCodigoPromocional = codigoPromocional;

                    quinielaModelList.Add(quinielaModel);
                }
                await this.quinielaRepository.CreateRangeAsync(quinielaModelList);
            }
            else
            {
                foreach (var item in juegosList)
                {
                    QuinielaModel quinielaModel = new QuinielaModel();

                    quinielaModel.Clave = Guid.NewGuid().ToString("N");
                    quinielaModel.ClaveJuego = item.Clave;
                    quinielaModel.ClaveCodigoPromocional = codigoPromocional;

                    quinielaModelList.Add(quinielaModel);
                }

                foreach (var item in quinielaByCodigo.Select(s => s.ClaveJuego))
                {
                    quinielaModelList.Remove(quinielaModelList.Where(w => w.ClaveJuego == item).FirstOrDefault());
                }

                await this.quinielaRepository.CreateRangeAsync(quinielaModelList);
            }

            QuinielaMV quinielaMV = new QuinielaMV();
            quinielaMV.ListCodigosPromocionales = codigoPromocionalRepository
                .GetAll()
                .Where(w => w.PuntajeQuinela != 0)
                .OrderBy(o => o.posicion)
                .Take(20)
                .ToList();

            quinielaMV.ListQuinielas = quinielaRepository.GetAllIncludes()
                .Where(w => w.CodigoPromocionalModel.Clave == codigoPromocional)
                .OrderBy(od => od.JuegoModel.FechaJuego)
                .ToList();

            return View(quinielaMV);
        }

        public async Task<IActionResult> Edit(string clave, int prediccion1, int prediccion3)
        {
            var quiniela = await quinielaRepository.GetByClaveAsync(clave);

            quiniela.Prediccion1 = prediccion1;
            quiniela.Prediccion3 = prediccion3;

            await quinielaRepository.UpdateAsync(quiniela);

            return RedirectToAction("Index", "Quinielas");
        }

        public async Task<IActionResult> Info(string codigoPromocional)
        {
            if (codigoPromocional == null)
            {
                codigoPromocional = HttpContext.Session.GetString("CodigoPromocional");
            }

            HttpContext.Session.SetString("CodigoPromocional", codigoPromocional);

            var quinielaByCodigo = quinielaRepository.GetAllIncludes().Where(w =>
                w.CodigoPromocionalModel.Clave == codigoPromocional).ToList();

            List<QuinielaModel> quinielaModelList = new();

            var juegosList = juegoRepository.GetAllIncludes().ToList();

            if (quinielaByCodigo.Count() == 0)
            {
                foreach (var item in juegosList)
                {
                    QuinielaModel quinielaModel = new QuinielaModel();

                    quinielaModel.Clave = Guid.NewGuid().ToString("N");
                    quinielaModel.ClaveJuego = item.Clave;
                    quinielaModel.ClaveCodigoPromocional = codigoPromocional;

                    quinielaModelList.Add(quinielaModel);
                }
                await this.quinielaRepository.CreateRangeAsync(quinielaModelList);
            }
            else
            {
                foreach (var item in juegosList)
                {
                    QuinielaModel quinielaModel = new QuinielaModel();

                    quinielaModel.Clave = Guid.NewGuid().ToString("N");
                    quinielaModel.ClaveJuego = item.Clave;
                    quinielaModel.ClaveCodigoPromocional = codigoPromocional;

                    quinielaModelList.Add(quinielaModel);
                }

                foreach (var item in quinielaByCodigo.Select(s => s.ClaveJuego))
                {
                    quinielaModelList.Remove(quinielaModelList.Where(w => w.ClaveJuego == item).FirstOrDefault());
                }

                await this.quinielaRepository.CreateRangeAsync(quinielaModelList);
            }

            QuinielaMV quinielaMV = new QuinielaMV();
            quinielaMV.ListCodigosPromocionales = codigoPromocionalRepository.GetAll().Where(w => w.PuntajeQuinela != 0).OrderByDescending(o => o.PuntajeQuinela).ToList();
            quinielaMV.ListQuinielas = quinielaRepository.GetAllIncludes().Where(w => w.CodigoPromocionalModel.Clave == codigoPromocional).OrderBy(od => od.JuegoModel.FechaJuego).ToList();
            return View(quinielaMV);
        }

    }
}
