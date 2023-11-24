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
            List<Tablero> tableros = new List<Tablero>();

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
        public IActionResult Create()
        {
            return View(new Tablero());
        }

        [HttpPost]
        public IActionResult Create(Tablero tablero)
        {
            repository.Create(tablero);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            return View(repository.GetById(id));
        }

        [HttpPost]
        public IActionResult Update(Tablero tablero)
        {
            var tablero2 = repository.GetById(tablero.Id);
            tablero2.Nombre = tablero.Nombre;
            tablero2.Descripcion = tablero.Descripcion;

            repository.Update(tablero.Id, tablero2);

            return RedirectToAction("Index");
        }

        public IActionResult DeleteTablero(int idTablero)
        {
            repository.Remove(idTablero);

            return RedirectToAction("Index");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}