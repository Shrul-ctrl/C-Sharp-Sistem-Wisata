using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SistemWisata.Models;

namespace SistemWisata.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }
    
    [HttpGet("main")]
    public IActionResult Main()
    {
        return View();
    }

    [HttpGet("about")]
    public IActionResult about()
    {
        return View();
    }

    [HttpGet("destinasi")]
    public IActionResult destinasi()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
