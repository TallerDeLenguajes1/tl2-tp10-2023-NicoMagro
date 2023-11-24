using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_NicoMagro.Models;
using tl2_tp10_2023_NicoMagro.Repositories;
using System.Diagnostics;

namespace tl2_tp10_2023_NicoMagro.Controllers
{
    public class TareaController : Controller
    {
        private ITareaRepository repository;
        private readonly ILogger<TareaController> _logger;

        public TareaController(ILogger<TareaController> logger)
        {
            _logger = logger;
            repository = new TareaRepository();
        }

        public IActionResult Index()
        {
            List<Tarea> tareas = new List<Tarea>();

            if (tareas != null)
            {
                return View(tareas);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new Tarea());
        }

        [HttpPost]
        public IActionResult Create(Tarea tarea)
        {
            repository.Create(1, tarea);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult EditarTarea(int id)
        {
            return View(repository.GetById(id));
        }


        [HttpPost]
        public IActionResult EditarTarea(Tarea tarea)
        {
            var tarea2 = repository.GetById(tarea.Id);

            tarea2.Nombre = tarea.Nombre;
            tarea2.Descripcion = tarea.Descripcion;
            tarea2.Color = tarea.Color;
            tarea2.Estado = tarea.Estado;

            repository.Update(tarea.Id, tarea2);

            return RedirectToAction("Index");
        }


        public IActionResult DeleteTarea(int id)
        {
            repository.Remove(id);

            return RedirectToAction("Index");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}