using Microsoft.AspNetCore.Mvc;
using sistem_wisata.Models;

namespace sistem_wisata.Controllers
{
    public class AccountController : Controller
    {
        // Menampilkan halaman login
        public IActionResult Login()
        {
            return View();
        }   

        [HttpPost]
        public IActionResult Login(Login model)
    {
    if (ModelState.IsValid)
    {
        string validEmail = "admin@sistemwisata.com";
        string validPassword = "Admin123";

        if (model.Email == validEmail && model.Password == validPassword)
        {
            return RedirectToAction("Index", "Home");
        }

        ModelState.AddModelError(string.Empty, "Username atau password salah.");
    }

    return View(model);
}

        public IActionResult Dashboard()
        {
            return View();
        }

        
    }
}
