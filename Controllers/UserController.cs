using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_NicoMagro.Models;
using tl2_tp10_2023_NicoMagro.Repositories;
using tl2_tp10_2023_NicoMagro.ViewModels.Usuarios;
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
                TempData["ErrorMessage"] = "Debes iniciar sesión para acceder a esta sección.";
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }

            List<Usuario> users = repository.GetAll();
            ListarUsuariosViewModel vm = new ListarUsuariosViewModel(users);

            if (users != null)
            {
                return View(vm);
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
            return View(new CrearUsuarioViewModel());
        }

        [HttpPost]
        public IActionResult CreateUser(CrearUsuarioViewModel vm)
        {
            if (!Logueado())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            repository.Create(new Usuario(vm));
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult UpdateUser(int id)
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
            ModificarUsuarioViewModel modificarUsuarioVM = new ModificarUsuarioViewModel(repository.GetById(id));
            return View(modificarUsuarioVM);
        }

        [HttpPost]
        public IActionResult UpdateUser(ModificarUsuarioViewModel vm)
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
            var userFromDb = new Usuario(vm);
            repository.Update(userFromDb.Id, userFromDb);

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
            return HttpContext.Session.Keys.Any() && ((int)HttpContext.Session.GetInt32("rol") == 0);
        }

        [HttpGet]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

}