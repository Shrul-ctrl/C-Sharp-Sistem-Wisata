using System.ComponentModel.DataAnnotations;

namespace sistem_wisata.Models;

public class Wisata
{
    public int Id { get; set; }
    public string Nama_Wisata { get; set; }
    public int KategoriId { get; set; }
    public int LokasiId { get; set; }
    public string Deskripsi { get; set; }
    public string? Foto_Wisata { get; set; }

    public Kategori? Kategori { get; set; }
    public Lokasi? Lokasi { get; set; }

}