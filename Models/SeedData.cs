using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using sistem_wisata.Models;
using System.Linq;

namespace sistem_wisata.Data
{
    public static class sistem_wisataContextSeed
    {
        public static void Seed(sistem_wisataContext context)
        {
            // Cek apakah sudah ada akun admin di database
            if (!context.Logins.Any())  // Pastikan untuk memeriksa tabel yang benar (Users)
            {
                var passwordHasher = new PasswordHasher<string>();
                var hashedPassword = passwordHasher.HashPassword(null, "Admin123"); // password yang di-hash

                // Menambahkan akun admin ke database
                context.Logins.Add(new Login
                {
                    Email = "admin@sistemwisata123.com",
                    Password = hashedPassword
                });

                // Simpan perubahan ke database
                context.SaveChanges();
            }
        }
    }
}
