using ASPNetCore6Identity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Quinela_TPD.Models;
using Quinela_TPD.Models.ModelView;
using Quinela_TPD.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quinela_TPD.Controllers
{
    public class MarcadoresController : Controller
    {
        private readonly IJuegoRepository juegoRepository;
        private readonly IEquipoRepository equipoRepository;
        private readonly IEstadioRepository estadioRepository;
        private readonly IMarcadorRepository marcadorRepository;
        private readonly IQuinielaRepository quinielaRepository;
        private readonly ICodigoPromocionalRepository codigoPromocionalRepository;
        public MarcadoresController(IJuegoRepository juegoRepository,
            IEquipoRepository equipoRepository,
            IEstadioRepository estadioRepository,
            IMarcadorRepository marcadorRepository,
            IQuinielaRepository quinielaRepository,
            ICodigoPromocionalRepository codigoPromocionalRepository)
        {
            this.juegoRepository = juegoRepository;
            this.equipoRepository = equipoRepository;
            this.estadioRepository = estadioRepository;
            this.marcadorRepository = marcadorRepository;
            this.quinielaRepository = quinielaRepository;
            this.codigoPromocionalRepository = codigoPromocionalRepository;
        }

        public IActionResult Index()
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

        public async Task<IActionResult> Create(string clave, int local, int visitante)
        {
            //var equipos = juegoRepository.GetAll().ToList();

            MarcardorModel marcador = new();
            marcador.Clave = Guid.NewGuid().ToString("N");
            marcador.ClaveJuego = clave;
            marcador.Equipo1 = local;
            marcador.Equipo2 = visitante;

            await marcadorRepository.CreateAsync(marcador);

            var juego = await juegoRepository.GetByClaveAsync(clave);
            juego.ClaveMarcador = marcador.Clave;
            await juegoRepository.UpdateAsync(juego);

            await ActualizaPuntaje(marcador);

            return RedirectToAction("Index", "Marcadores");
        }

        async Task<bool> ActualizaPuntaje(MarcardorModel marcador)
        {

            var quinielasList = quinielaRepository.GetAll().Where(w => w.ClaveJuego == marcador.ClaveJuego);

            List<QuinielaModel> QuinielaModelList = new List<QuinielaModel>();
            List<CodigoPromocionalModel> codigoPromocionalModelList = new List<CodigoPromocionalModel>();
            int puntaje = 0;

            //Calcula puntajes quiniela
            foreach (var item in quinielasList)
            {
                puntaje = 0;

                if (item.Prediccion1 != null)
                {

                    //puntaje por atinar a local
                    if (item.Prediccion1 == marcador.Equipo1)
                    {
                        puntaje = puntaje + 2;
                    }

                    //puntaje por atinar a visitante
                    if (item.Prediccion3 == marcador.Equipo2)
                    {
                        puntaje = puntaje + 2;
                    }

                    //puntaje por atinar a empate
                    if (marcador.Equipo1 == marcador.Equipo2)
                    {
                        if (item.Prediccion1 == item.Prediccion3)
                        {
                            puntaje = puntaje + 5;
                        }
                    }

                    //puntaje por atinar a ganador visitante
                    if (marcador.Equipo1 < marcador.Equipo2)
                    {
                        if (item.Prediccion1 < item.Prediccion3)
                        {
                            puntaje = puntaje + 5;
                        }
                    }

                    //puntaje por atinar a ganador local
                    if (marcador.Equipo1 > marcador.Equipo2)
                    {
                        if (item.Prediccion1 > item.Prediccion3)
                        {
                            puntaje = puntaje + 5;
                        }
                    }

                    //puntaje por todo
                    if (item.Prediccion1 == marcador.Equipo1 && item.Prediccion3 == marcador.Equipo2)
                    {
                        puntaje = 12;
                    }

                }

                item.Puntaje = puntaje;
                QuinielaModelList.Add(item);




                ////suma a puntaje de codigo promocional--C-157	JOSE EDMUNDO ESPINO GONZALEZ   refamundo@hotmail.com
                CodigoPromocionalModel codigoPromocionalModel =
                            await codigoPromocionalRepository
                                .GetByClaveAsync(item.ClaveCodigoPromocional);

                var puntajetotalQuiniela = quinielaRepository.GetAll()
                    .Where(w => w.ClaveCodigoPromocional == item.ClaveCodigoPromocional && w.Puntaje != null).Sum(s => s.Puntaje);

                //Se genera bug apra controlar los resultados se tiene que agregar dos veces el mismo marcador
                codigoPromocionalModel.PuntajeQuinela = puntajetotalQuiniela;// + puntaje; //codigoPromocionalModel.PuntajeQuinela + puntaje;

                codigoPromocionalModelList.Add(codigoPromocionalModel);



                ////suma a puntaje de codigo promocional--C-157	JOSE EDMUNDO ESPINO GONZALEZ   refamundo@hotmail.com
                //CodigoPromocionalModel codigoPromocionalModel =
                //            await codigoPromocionalRepository
                //                .GetByClaveAsync(item.ClaveCodigoPromocional);

                //codigoPromocionalModel.PuntajeQuinela = codigoPromocionalModel.PuntajeQuinela + puntaje;

                //codigoPromocionalModelList.Add(codigoPromocionalModel);

            }

            int posicion = 1;
            int? posicionAnterior = 0;
            int? puntajeAnterior = 0;

            foreach (var item in codigoPromocionalModelList.OrderByDescending(o => o.PuntajeQuinela))
            {
                if (item.PuntajeQuinela == 0)
                {
                    item.posicion = codigoPromocionalModelList.Count() + 1;
                }
                else
                {
                    if (item.PuntajeQuinela == puntajeAnterior)
                    {
                        item.posicion = posicionAnterior;//posicion++;
                    }
                    else
                    {
                        item.posicion = posicion++;
                        posicionAnterior = item.posicion;
                        puntajeAnterior = item.PuntajeQuinela;
                    }
                }
            }

            await quinielaRepository.UpdateRangeAsync(QuinielaModelList);
            await codigoPromocionalRepository.UpdateRangeAsync(codigoPromocionalModelList);


            return true;
        }
    }
}