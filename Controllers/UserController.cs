using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_NicoMagro.Models;
using tl2_tp10_2023_NicoMagro.Repositories;
using System.Diagnostics;

namespace tl2_tp10_2023_NicoMagro.Controllers
{
    [Route("[controller]")]
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

        [HttpGet("Create")]
        public IActionResult CreateUser()
        {
            return View(new Usuario());
        }

        [HttpPost("Create")]
        public IActionResult CreateUser(Usuario user)
        {
            repository.Create(user);
            return RedirectToAction("Index");
        }

        [HttpGet("Update/{id}")]
        public IActionResult UpdateUser(int id)
        {
            return View(repository.GetById(id));
        }

        [HttpPost("Update")]
        public IActionResult UpdateUser(Usuario user)
        {
            var userFromDb = repository.GetById(user.Id);
            userFromDb.Nombre = user.Nombre;

            repository.Update(user.Id, userFromDb);

            return RedirectToAction("Index");
        }

        [HttpGet("Delete/{id}")]
        public IActionResult DeleteUser(int id)
        {
            repository.Remove(id);

            return RedirectToAction("Index");
        }

        [HttpGet("Error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

}