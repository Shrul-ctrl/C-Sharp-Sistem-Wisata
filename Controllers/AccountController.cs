using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using sistem_wisata.Models;

public class AccountController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;

    public AccountController(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

  [HttpPost]
public async Task<IActionResult> Register(Register model)
{
    if (ModelState.IsValid)
    {
        var user = new IdentityUser 
        { 
            UserName = model.Nama, 
            Email = model.Email 
        };

        var result = await _userManager.CreateAsync(user, model.Password);
        if (result.Succeeded)
        {
            // Mengarahkan ke tampilan kategori
            return RedirectToAction("Hoem", "Kategori");
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }
    }
    return View(model);
}


}
