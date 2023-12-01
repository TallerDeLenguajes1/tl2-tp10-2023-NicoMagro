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
            return View(new Tablero());
        }

        [HttpPost]
        public IActionResult CrearTablero(Tablero tablero)
        {
            repository.Create(tablero);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult EditarTablero(int id)
        {
            return View(repository.GetById(id));
        }

        [HttpPost]
        public IActionResult EditarTablero(Tablero tablero)
        {
            var tablero2 = repository.GetById(tablero.Id);
            tablero2.Nombre = tablero.Nombre;
            tablero2.Descripcion = tablero.Descripcion;

            repository.Update(tablero.Id, tablero2);

            return RedirectToAction("Index");
        }

        public IActionResult EliminarTablero(int Id)
        {
            repository.Remove(Id);

            return RedirectToAction("Index");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}