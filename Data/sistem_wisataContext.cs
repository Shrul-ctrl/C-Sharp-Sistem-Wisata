using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using sistem_wisata.Models;

namespace sistem_wisata.Data
{
    public class sistem_wisataContext : DbContext
    {
        public sistem_wisataContext (DbContextOptions<sistem_wisataContext> options)
            : base(options)
        {
        }

        public DbSet<sistem_wisata.Models.Kategori> Kategori { get; set; } = default!;
        public DbSet<sistem_wisata.Models.Lokasi> Lokasi { get; set; } = default!;
        public DbSet<sistem_wisata.Models.Wisata> Wisata { get; set; } = default!;
    }
}
