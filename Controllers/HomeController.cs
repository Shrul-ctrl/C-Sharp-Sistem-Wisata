using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sistem_wisata.Data;
using SistemWisata.Models;

namespace SistemWisata.Controllers;

public class HomeController : Controller
{
    private readonly sistem_wisataContext _context;

    public HomeController(sistem_wisataContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var lokasiCount = _context.Lokasi.Count();
        var kategoriCount = _context.Kategori.Count();
        var wisataCount = _context.Wisata.Count();

        ViewBag.LokasiCount = lokasiCount;
        ViewBag.kategoriCount = kategoriCount;
        ViewBag.WisataCount = wisataCount;

        return View();
    }

    [HttpGet("main")]
    public async Task<IActionResult> main()
    {
        return View(await _context.Wisata.ToListAsync());
    }

    [HttpGet("about")]
    public IActionResult about()
    {
        return View();
    }

    [HttpGet("Destinasi/Show/{id}")]
    public async Task<IActionResult> Detail(int id)
    {
        var wisata = await _context.Wisata
            .Include(w => w.Kategori) // Include kategori jika Anda ingin mengambil data kategori
            .FirstOrDefaultAsync(w => w.Id == id);

        if (wisata == null)
        {
            return NotFound(); // Jika tidak ditemukan
        }

        return View(wisata);
    }

    [HttpGet("destinasi")]
    public async Task<IActionResult> destinasi()
    {
        return View(await _context.Wisata.ToListAsync());
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
