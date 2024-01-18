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
            if (!ModelState.IsValid) return RedirectToAction("EditarTarea");

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
            try
            {
                if (!Logueado())
                {
                    return RedirectToRoute(new { controller = "Login", action = "Index" });
                }
                if (!ModelState.IsValid) return RedirectToAction("EditarTarea");
                return View(new CrearTareaViewModel());
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Error en el endpoint 'CrearTarea'. Detalles: {ex.ToString()}");
                return View("Error");
            }
        }

        [HttpPost]
        public IActionResult CrearTarea(CrearTareaViewModel vm)
        {
            try
            {
                if (!Logueado())
                {
                    return RedirectToRoute(new { controller = "Login", action = "Index" });
                }
                if (!ModelState.IsValid) return RedirectToAction("EditarTarea");
                var task = new Tarea(vm);
                repository.Create(1, task);
                return RedirectToAction("Index");
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Error en el endpoint 'CrearTarea'. Detalles: {ex.ToString()}");
                return View("Error");
            }
        }

        [HttpGet]
        public IActionResult EditarTarea(int id)
        {
            try
            {
                if (!Logueado())
                {
                    return RedirectToRoute(new { controller = "Login", action = "Index" });
                }
                if (!ModelState.IsValid) return RedirectToAction("EditarTarea");
                var vm = new ModificarTareaViewModel(repository.GetById(id));
                return View(vm);
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Error en el endpoint 'EditarTarea'. Detalles: {ex.ToString()}");
                return View("Error");
            }
        }


        [HttpPost]
        public IActionResult EditarTarea(ModificarTareaViewModel vm)
        {
            try
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
                if (!ModelState.IsValid) return RedirectToAction("EditarTarea");

                var tarea2 = new Tarea(vm);

                repository.Update(tarea2.Id, tarea2);

                return RedirectToAction("Index");
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Error en el endpoint 'EditarTarea'. Detalles: {ex.ToString()}");
                return View("Error");
            }
        }


        public IActionResult EliminarTarea(int Id)
        {
            try
            {
                if (!Logueado())
                {
                    return RedirectToRoute(new { controller = "Login", action = "Index" });
                }
                if (!ModelState.IsValid) return RedirectToAction("EditarTarea");

                repository.Remove(Id);

                return RedirectToAction("Index");
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Error en el endpoint 'CrearTarea'. Detalles: {ex.ToString()}");
                return View("Error");
            }
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