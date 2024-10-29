using System.ComponentModel.DataAnnotations;

namespace sistem_wisata.Models;

public class Kategori
{
    public int Id { get; set; }
    public string Nama_Kategori { get; set; }

    public ICollection<Wisata> Wisata { get; set; } = new List<Wisata>();
}