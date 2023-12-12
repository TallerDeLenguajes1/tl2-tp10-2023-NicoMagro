using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_NicoMagro.Models;
using tl2_tp10_2023_NicoMagro.Repositories;
using System.Diagnostics;

namespace tl2_tp10_2023_NicoMagro.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private UsuarioRepository repository;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
            repository = new UsuarioRepository();
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (!Logueado())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }

            List<Usuario> users = repository.GetAll();

            if (users != null)
            {
                return View(users);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public IActionResult CreateUser()
        {
            if (!Logueado())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            return View(new Usuario());
        }

        [HttpPost]
        public IActionResult CreateUser(Usuario user)
        {
            if (!Logueado())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            repository.Create(user);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult UpdateUser(int id)
        {
            if (!Logueado())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            return View(repository.GetById(id));
        }

        [HttpPost]
        public IActionResult UpdateUser(Usuario user)
        {
            if (!Logueado())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            var userFromDb = repository.GetById(user.Id);
            userFromDb.Nombre = user.Nombre;

            repository.Update(user.Id, userFromDb);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult DeleteUser(int id)
        {
            if (!Logueado())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            repository.Remove(id);

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

        [HttpGet]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

}