using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using sistem_wisata.Data;
using sistem_wisata.Models;

namespace SistemWisata.Controllers
{
    public class LokasiController : Controller
    {
        private readonly sistem_wisataContext _context;

        public LokasiController(sistem_wisataContext context)
        {
            _context = context;
        }

        // GET: Lokasi
        public async Task<IActionResult> Index()
        {
            return View(await _context.Lokasi.ToListAsync());
        }

        // GET: Lokasi/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lokasi = await _context.Lokasi
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lokasi == null)
            {
                return NotFound();
            }

            return View(lokasi);
        }

        // GET: Lokasi/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Lokasi/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nama_Lokasi,Provinsi,Kabupaten")] Lokasi lokasi)
        {
            if (ModelState.IsValid)
            {

                // Cek apakah Lokasi sudah ada
                var existingLokasi = await _context.Lokasi
                    .FirstOrDefaultAsync(k => k.Nama_Lokasi == lokasi.Nama_Lokasi);

                if (existingLokasi != null)
                {
                    ModelState.AddModelError("Nama_Lokasi", "Lokasi dengan nama ini sudah ada.");
                    return View(lokasi);
                }

                _context.Add(lokasi);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Data kategori berhasil dibuat!";
                return RedirectToAction(nameof(Index));
            }
            return View(lokasi);
        }

        // GET: Lokasi/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lokasi = await _context.Lokasi.FindAsync(id);
            if (lokasi == null)
            {
                return NotFound();
            }
            return View(lokasi);
        }

        // POST: Lokasi/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nama_Lokasi,Provinsi,Kabupaten")] Lokasi lokasi)
        {
            // Cek apakah Lokasi sudah ada
            var existingLokasi = await _context.Lokasi
                .FirstOrDefaultAsync(k => k.Nama_Lokasi == lokasi.Nama_Lokasi);

            if (existingLokasi != null)
            {
                ModelState.AddModelError("Nama_Lokasi", "Lokasi dengan nama ini sudah ada.");
                return View(lokasi);
            }

            if (id != lokasi.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lokasi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LokasiExists(lokasi.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["SuccessMessage"] = "Data kategori berhasil diubah!";
                return RedirectToAction(nameof(Index));
            }
            return View(lokasi);
        }

        // GET: Lokasi/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lokasi = await _context.Lokasi
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lokasi == null)
            {
                return NotFound();
            }

            return View(lokasi);
        }

        // POST: Lokasi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lokasi = await _context.Lokasi.FindAsync(id);
            if (lokasi != null)
            {
                _context.Lokasi.Remove(lokasi);
            }

            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Data kategori berhasil dihapus!";
            return RedirectToAction(nameof(Index));
        }

        private bool LokasiExists(int id)
        {
            return _context.Lokasi.Any(e => e.Id == id);
        }
    }
}
