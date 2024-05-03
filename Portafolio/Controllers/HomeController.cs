using Microsoft.AspNetCore.Mvc;
using Portafolio.Models;
using Portafolio.Services;
using System.Diagnostics;

namespace Portafolio.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRepositorioProyectos _repositorioProyectos;
        private readonly ServicioTransitorio servicioTransitorio;
        private readonly ServicioDelimitado servicioDelimitado;
        private readonly ServicioUnico servicioUnico;
        private readonly ServicioTransitorio servicioTransitorio2;
        private readonly ServicioDelimitado servicioDelimitado2;
        private readonly ServicioUnico servicioUnico2;

        public HomeController(ILogger<HomeController> logger, 
            IRepositorioProyectos repositorioProyectos,

            ServicioTransitorio servicioTransitorio,
            ServicioDelimitado servicioDelimitado,
            ServicioUnico servicioUnico,

            ServicioTransitorio servicioTransitorio2,
            ServicioDelimitado servicioDelimitado2,
            ServicioUnico servicioUnico2
            )
        {
            _logger = logger;
            _repositorioProyectos = repositorioProyectos;
            this.servicioTransitorio = servicioTransitorio;
            this.servicioDelimitado = servicioDelimitado;
            this.servicioUnico = servicioUnico;
            this.servicioTransitorio2 = servicioTransitorio2;
            this.servicioDelimitado2 = servicioDelimitado2;
            this.servicioUnico2 = servicioUnico2;
        }

        public IActionResult Index()
        {
            //Categorías de ILogger
            //    - Critical
            //    - Error
            //    - Warning
            //    - Information
            //    - Debug
            //    - Trace

            _logger.LogTrace("Este es un mensaje de trace");
            _logger.LogDebug("Este es un mensaje de debug");
            _logger.LogInformation("Este es un mensaje de information");
            _logger.LogWarning("Este es un mensaje de warning");
            _logger.LogError("Este es un mensaje de error");
            _logger.LogCritical("Este es un mensaje de critical");

            //ViewBag.Nombre = "Jimmy Grados";
            //var persona = new Persona()
            //{
            //    Nombre = "Jimmy",
            //    Edad = 78
            //};
            //return View("Index", "Jimmy");
            //return View(persona);

            //var repositorioProyectos = new RepositorioProyectos();
            var proyectos = _repositorioProyectos.ObtenerProyectos().Take(3).ToList();

            var guidViewModel = new EjemploGUIDViewModel()
            {
                Transitorio = servicioTransitorio.ObtenerGuid,
                Delimitado = servicioDelimitado.ObtenerGuid,
                Unico = servicioUnico.ObtenerGuid
            };

            var guidViewModel2 = new EjemploGUIDViewModel()
            {
                Transitorio = servicioTransitorio2.ObtenerGuid,
                Delimitado = servicioDelimitado2.ObtenerGuid,
                Unico = servicioUnico2.ObtenerGuid
            };

            var modelo = new HomeIndexViewModel() 
            { 
                Proyectos = proyectos,
                EjemploGUID_1 = guidViewModel,
                EjemploGUID_2 = guidViewModel2
            };

            return View(modelo);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
