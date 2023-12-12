using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_NicoMagro.Models;
using tl2_tp10_2023_NicoMagro.Repositories;
using System.Diagnostics;
using tl2_tp10_2023_NicoMagro.ViewModels.Tareas;

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
            if (!Logueado())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }

            List<Tarea> tareas = repository.GetAll();
            ListarTareasViewModel vm = new ListarTareasViewModel(tareas);

            if (tareas != null)
            {
                return View(vm);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public IActionResult CrearTarea()
        {
            if (!Logueado())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            return View(new CrearTareaViewModel());
        }

        [HttpPost]
        public IActionResult CrearTarea(CrearTareaViewModel vm)
        {
            if (!Logueado())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            var task = new Tarea(vm);
            repository.Create(1, task);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult EditarTarea(int id)
        {
            if (!Logueado())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            var vm = new ModificarTareaViewModel(repository.GetById(id));
            return View(vm);
        }


        [HttpPost]
        public IActionResult EditarTarea(ModificarTareaViewModel vm)
        {
            if (!Logueado())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            if (!esAdmin())
            {
                TempData["ErrorMessage"] = "No tienes permisos para editar un usuario";
                return RedirectToAction("Index");
            }

            var tarea2 = new Tarea(vm);

            repository.Update(tarea2.Id, tarea2);

            return RedirectToAction("Index");
        }


        public IActionResult EliminarTarea(int Id)
        {
            if (!Logueado())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }

            repository.Remove(Id);

            return RedirectToAction("Index");
        }

        public bool Logueado()
        {
            return HttpContext.Session.Keys.Any();
        }

        private bool esAdmin()
        {
            return HttpContext.Session.Keys.Any() && ((int)HttpContext.Session.GetInt32("rol") == 0);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}