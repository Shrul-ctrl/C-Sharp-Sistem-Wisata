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
    public class KategoriController : Controller
    {
        private readonly sistem_wisataContext _context;

        public KategoriController(sistem_wisataContext context)
        {
            _context = context;
        }

        // GET: Kategori
        public async Task<IActionResult> Index()
        {
            return View(await _context.Kategori.ToListAsync());
        }

        // GET: Kategori/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kategori = await _context.Kategori
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kategori == null)
            {
                return NotFound();
            }

            return View(kategori);
        }

        // GET: Kategori/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Kategori/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create([Bind("Id,Nama_Kategori")] Kategori kategori)
        {
            if (ModelState.IsValid)
            {
                var existingKategori = await _context.Kategori
                    .FirstOrDefaultAsync(k => k.Nama_Kategori == kategori.Nama_Kategori);

                if (existingKategori != null)
                {
                    ModelState.AddModelError("Nama_Kategori", "Kategori dengan nama ini sudah ada.");
                    return View(kategori);
                }

                kategori.CreatedAt = DateTime.UtcNow;
                kategori.UpdatedAt = DateTime.UtcNow;

                _context.Add(kategori);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Data kategori berhasil dibuat!";
                return RedirectToAction(nameof(Index));
            }
            return View(kategori);
        }

        // GET: Kategori/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kategori = await _context.Kategori.FindAsync(id);
            if (kategori == null)
            {
                return NotFound();
            }
            return View(kategori);
        }

        // POST: Kategori/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nama_Kategori")] Kategori kategori)
        {
            if (id != kategori.Id)
            {
                return NotFound();
            }

            var existingKategori = await _context.Kategori
                    .FirstOrDefaultAsync(k => k.Nama_Kategori == kategori.Nama_Kategori);

            if (existingKategori != null)
            {
                ModelState.AddModelError("Nama_Kategori", "Kategori dengan nama ini sudah ada.");
                return View(kategori);
            }

            var kategoriUpdateAt = await _context.Kategori.FindAsync(id);
            if (kategoriUpdateAt == null)
            {
                return NotFound();
            }

            kategoriUpdateAt.Nama_Kategori = kategori.Nama_Kategori;
            kategoriUpdateAt.UpdatedAt = DateTime.UtcNow;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kategoriUpdateAt);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KategoriExists(kategoriUpdateAt.Id))
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
            return View(kategori);
        }

        // GET: Kategori/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kategori = await _context.Kategori
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kategori == null)
            {
                return NotFound();
            }

            return View(kategori);
        }

        // POST: Kategori/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kategori = await _context.Kategori.FindAsync(id);
            if (kategori != null)
            {
                _context.Kategori.Remove(kategori);
            }

            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Data kategori berhasil dihapus!";
            return RedirectToAction(nameof(Index));
        }

        private bool KategoriExists(int id)
        {
            return _context.Kategori.Any(e => e.Id == id);
        }
    }
}
