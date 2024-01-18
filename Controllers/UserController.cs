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
            if (!ModelState.IsValid) return RedirectToAction("EditarTarea");

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
            try
            {
                if (!Logueado())
                {
                    return RedirectToRoute(new { controller = "Login", action = "Index" });
                }
                if (!ModelState.IsValid) return RedirectToAction("EditarTarea");
                return View(new CrearUsuarioViewModel());
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Error en el endpoint 'CreateUser'. Detalles: {ex.ToString()}");
                return View("Error");
            }
        }

        [HttpPost]
        public IActionResult CreateUser(CrearUsuarioViewModel vm)
        {
            try
            {
                if (!Logueado())
                {
                    return RedirectToRoute(new { controller = "Login", action = "Index" });
                }
                if (!ModelState.IsValid) return RedirectToAction("EditarTarea");
                repository.Create(new Usuario(vm));
                return RedirectToAction("Index");
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Error en el endpoint 'CreateUser'. Detalles: {ex.ToString()}");
                return View("Error");
            }
        }

        [HttpGet]
        public IActionResult UpdateUser(int id)
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
                ModificarUsuarioViewModel modificarUsuarioVM = new ModificarUsuarioViewModel(repository.GetById(id));
                return View(modificarUsuarioVM);
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Error en el endpoint 'UpdateUser'. Detalles: {ex.ToString()}");
                return View("Error");
            }
        }

        [HttpPost]
        public IActionResult UpdateUser(ModificarUsuarioViewModel vm)
        {
            try
            {
                if (!Logueado())
                {
                    TempData["ErrorMessage"] = "No tienes permisos para editar un usuario";
                    return RedirectToRoute(new { controller = "Login", action = "Index" });
                }
                if (!esAdmin())
                {
                    TempData["ErrorMessage"] = "No tienes permisos para editar un usuario";
                    return RedirectToAction("Index");
                }
                if (!ModelState.IsValid) return RedirectToAction("EditarTarea");
                var userFromDb = new Usuario(vm);
                repository.Update(userFromDb.Id, userFromDb);

                return RedirectToAction("Index");
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Error en el endpoint 'UpdateUser'. Detalles: {ex.ToString()}");
                return View("Error");
            }
        }

        [HttpGet]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                if (!Logueado())
                {
                    TempData["ErrorMessage"] = "No tienes permisos para editar un usuario";
                    return RedirectToRoute(new { controller = "Login", action = "Index" });
                }
                if (!ModelState.IsValid) return RedirectToAction("EditarTarea");
                repository.Remove(id);

                return RedirectToAction("Index");
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Error en el endpoint 'DeleteUser'. Detalles: {ex.ToString()}");
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

        [HttpGet]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

}