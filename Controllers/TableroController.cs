using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_NicoMagro.Repositories;
using tl2_tp10_2023_NicoMagro.Models;
using System.Diagnostics;

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
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            List<Tablero> tableros = repository.GetAll();

            if (tableros != null)
            {
                return View(tableros);
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
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            return View(new Tablero());
        }

        [HttpPost]
        public IActionResult CrearTablero(Tablero tablero)
        {
            if (!Logueado())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            repository.Create(tablero);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult EditarTablero(int id)
        {
            if (!Logueado())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            return View(repository.GetById(id));
        }

        [HttpPost]
        public IActionResult EditarTablero(Tablero tablero)
        {
            if (!Logueado())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            var tablero2 = repository.GetById(tablero.Id);
            tablero2.Nombre = tablero.Nombre;
            tablero2.Descripcion = tablero.Descripcion;

            repository.Update(tablero.Id, tablero2);

            return RedirectToAction("Index");
        }

        public IActionResult EliminarTablero(int Id)
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
            return HttpContext.Session.Keys.Any() && ((int)HttpContext.Session.GetInt32("rol") == 1);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}