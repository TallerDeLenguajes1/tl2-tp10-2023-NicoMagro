using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_NicoMagro.Repositories;
using tl2_tp10_2023_NicoMagro.Models;
using System.Diagnostics;
using tl2_tp10_2023_NicoMagro.ViewModels.Tableros;

namespace tl2_tp10_2023_NicoMagro.Controllers
{
    public class TableroController : Controller
    {
        private ITableroRepository repository;
        private readonly ILogger<TableroController> _logger;

        public TableroController(ILogger<TableroController> logger)
        {
            _logger = logger;
            repository = new TableroRepository();
        }

        public IActionResult Index()
        {
            if (!Logueado())
            {
                TempData["ErrorMessage"] = "No tienes permisos para editar un usuario";
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            List<Tablero> tableros = repository.GetAll();
            ListarTablerosViewModel vm = new ListarTablerosViewModel(tableros);

            if (tableros != null)
            {
                return View(vm);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public IActionResult CrearTablero()
        {
            if (!Logueado())
            {
                TempData["ErrorMessage"] = "No tienes permisos para editar un usuario";
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            if (!esAdmin())
            {
                TempData["ErrorMessage"] = "No tienes permisos para crear un tablero";
                return RedirectToAction("Index");
            }
            return View(new CrearTableroViewModel());
        }

        [HttpPost]
        public IActionResult CrearTablero(CrearTableroViewModel vm)
        {
            if (!Logueado())
            {
                TempData["ErrorMessage"] = "No tienes permisos para editar un usuario";
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            if (!esAdmin())
            {
                TempData["ErrorMessage"] = "No tienes permisos para crear un tablero";
                return RedirectToAction("Index");
            }


            var tablero = new Tablero(vm);
            repository.Create(tablero);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult EditarTablero(int id)
        {
            if (!Logueado())
            {
                TempData["ErrorMessage"] = "No tienes permisos para editar un usuario";
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            if (!esAdmin())
            {
                TempData["ErrorMessage"] = "No tienes permisos para crear un tablero";
                return RedirectToAction("Index");
            }
            ModificarTableroViewModel vm = new ModificarTableroViewModel(repository.GetById(id));
            return View(vm);
        }

        [HttpPost]
        public IActionResult EditarTablero(ModificarTableroViewModel vm)
        {
            if (!Logueado())
            {
                TempData["ErrorMessage"] = "No tienes permisos para editar un usuario";
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            if (!esAdmin())
            {
                TempData["ErrorMessage"] = "No tienes permisos para crear un tablero";
                return RedirectToAction("Index");
            }
            var userFromDb = new Tablero(vm);
            repository.Update(userFromDb.Id, userFromDb);


            return RedirectToAction("Index");
        }

        public IActionResult EliminarTablero(int Id)
        {
            if (!Logueado())
            {
                TempData["ErrorMessage"] = "No tienes permisos para editar un usuario";
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            if (!esAdmin())
            {
                TempData["ErrorMessage"] = "No tienes permisos para crear un tablero";
                return RedirectToAction("Index");
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