using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using tl2_tp10_2023_NicoMagro.Repositories;
using tl2_tp10_2023_NicoMagro.ViewModels;
using tl2_tp10_2023_NicoMagro.Models;

namespace tl2_tp10_2023_NicoMagro.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarioRepository repository;
        private readonly ILogger<LoginController> _logger;

        public LoginController(ILogger<LoginController> logger)
        {
            repository = new UsuarioRepository();
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel loginViewModel)
        {
            var lista = repository.GetAll();
            var usuario = repository.GetAll().FirstOrDefault(u => u.Nombre == loginViewModel.Nombre && u.Password == loginViewModel.Password);
            if (usuario == null)
            {
                return RedirectToAction("Index");
            }
            LoguearUsuario(usuario);
            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }

        public void LoguearUsuario(Usuario usuario)
        {
            HttpContext.Session.SetInt32("id", usuario.Id);
            HttpContext.Session.SetString("usuario", usuario.Nombre);
            HttpContext.Session.SetInt32("rol", (int)usuario.RolUsuario);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}