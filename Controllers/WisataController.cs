using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using sistem_wisata.Data;
using sistem_wisata.Models;
using Microsoft.AspNetCore.Authorization;

namespace SistemWisata.Controllers
{
    public class WisataController : Controller
    {
        private readonly sistem_wisataContext _context;

        public WisataController(sistem_wisataContext context)
        {
            _context = context;
        }

        // GET: Wisata
         [Authorize]
        [HttpGet("Admin/Wisata")]
        public async Task<IActionResult> Index(string searchString)
        {
            if (_context.Wisata == null)
            {
                return Problem("Entity set 'SistemWisataContext.Wisata' is null.");
            }

            var wisata = _context.Wisata.Include(w => w.Kategori).Include(w => w.Lokasi).AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                wisata = wisata.Where(w => w.Nama_Wisata!.ToUpper().Contains(searchString.ToUpper()));
            }

            return View(await wisata.OrderByDescending(k => k.CreatedAt).ToListAsync());
        }


        // GET: Wisata/Details/5
         [Authorize]
        [HttpGet("Admin/Wisata/Detail/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wisata = await _context.Wisata
                .Include(w => w.Kategori)
                .Include(w => w.Lokasi)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (wisata == null)
            {
                return NotFound();
            }

            return View(wisata);
        }

        // GET: Wisata/Create
         [Authorize]
        [HttpGet("Admin/Wisata/Create")]
        public IActionResult Create()
        {
            ViewData["KategoriId"] = new SelectList(_context.Kategori, "Id", "Nama_Kategori");
            ViewData["LokasiId"] = new SelectList(_context.Lokasi, "Id", "Nama_Lokasi");
            return View();
        }

        // POST: Wisata/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Admin/Wisata/Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nama_Wisata,KategoriId,LokasiId,Deskripsi,Foto_Wisata")] Wisata wisata, IFormFile Foto_Wisata)
        {
            if (ModelState.IsValid)
            {
                // Cek apakah Wisata sudah ada
                var existingWisata = await _context.Wisata
                    .FirstOrDefaultAsync(k => k.Nama_Wisata == wisata.Nama_Wisata);

                if (existingWisata != null)
                {
                    ModelState.AddModelError("Nama_Wisata", "Wisata dengan nama ini sudah ada.");
                    return View(wisata);
                }

                if (Foto_Wisata != null && Foto_Wisata.Length > 0)
                {
                    var fileName = Path.GetFileName(Foto_Wisata.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await Foto_Wisata.CopyToAsync(stream);
                    }

                    wisata.Foto_Wisata = fileName;
                }

                TimeZoneInfo indonesiaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
                DateTime waktuIndonesia = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, indonesiaTimeZone);

                wisata.CreatedAt = waktuIndonesia;
                wisata.UpdatedAt = waktuIndonesia;


                _context.Add(wisata);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Data tempat wisata berhasil dibuat!";
                return RedirectToAction(nameof(Index));
            }

            ViewData["KategoriId"] = new SelectList(_context.Kategori, "Id", "Nama_Kategori", wisata.KategoriId);
            ViewData["LokasiId"] = new SelectList(_context.Lokasi, "Id", "Nama_Lokasi", wisata.LokasiId);
            return View(wisata);
        }


        // GET: Wisata/Edit/5
         [Authorize]
        [HttpGet("Admin/Wisata/Edit/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wisata = await _context.Wisata.FindAsync(id);
            if (wisata == null)
            {
                return NotFound();
            }
            ViewData["KategoriId"] = new SelectList(_context.Kategori, "Id", "Nama_Kategori", wisata.KategoriId);
            ViewData["LokasiId"] = new SelectList(_context.Lokasi, "Id", "Nama_Lokasi", wisata.LokasiId);
            return View(wisata);
        }

        // POST: Wisata/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Admin/Wisata/Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nama_Wisata,KategoriId,LokasiId,Deskripsi,Foto_Wisata")] Wisata wisata, IFormFile Foto_Wisata)
        {
            if (id != wisata.Id)
            {
                return NotFound();
            }

            var wisataUpdateAt = await _context.Wisata.FindAsync(id);
            if (wisataUpdateAt == null)
            {
                return NotFound();
            }

            TimeZoneInfo indonesiaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
            DateTime waktuIndonesia = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, indonesiaTimeZone);

            wisataUpdateAt.Nama_Wisata = wisata.Nama_Wisata;
            wisataUpdateAt.KategoriId = wisata.KategoriId;
            wisataUpdateAt.LokasiId = wisata.LokasiId;
            wisataUpdateAt.Deskripsi = wisata.Deskripsi;
            wisataUpdateAt.UpdatedAt = waktuIndonesia;

            // Menyimpan nama foto lama
            var oldFotoWisata = _context.Wisata.AsNoTracking().FirstOrDefault(w => w.Id == id)?.Foto_Wisata;

            if (Foto_Wisata != null && Foto_Wisata.Length > 0)
            {
                var fileName = Path.GetFileName(Foto_Wisata.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await Foto_Wisata.CopyToAsync(stream);
                }

                wisataUpdateAt.Foto_Wisata = fileName;

                // Hapus foto lama
                if (!string.IsNullOrEmpty(oldFotoWisata))
                {
                    var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", oldFotoWisata);
                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(wisataUpdateAt);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Data tempat wisata berhasil diubah!";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WisataExists(wisataUpdateAt.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            ViewData["KategoriId"] = new SelectList(_context.Kategori, "Id", "Nama_Kategori", wisata.KategoriId);
            ViewData["LokasiId"] = new SelectList(_context.Lokasi, "Id", "Nama_Lokasi", wisata.LokasiId);
            return View(wisata);
        }

        // GET: Wisata/Delete/5
         [Authorize]
        [HttpGet("Admin/Wisata/Delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wisata = await _context.Wisata
                .Include(w => w.Kategori)
                .Include(w => w.Lokasi)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (wisata == null)
            {
                return NotFound();
            }

            return View(wisata);
        }

        // POST: Wisata/Delete/5
        [HttpPost("Admin/Wisata/Delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var wisata = await _context.Wisata.FindAsync(id);
            if (wisata != null)
            {
                _context.Wisata.Remove(wisata);
            }

            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Data tempat wisata berhasil dihapus!";
            return RedirectToAction(nameof(Index));
        }

        private bool WisataExists(int id)
        {
            return _context.Wisata.Any(e => e.Id == id);
        }
    }
}
