using System.ComponentModel.DataAnnotations;

namespace sistem_wisata.Models;

public class Lokasi
{
    public int Id { get; set; }
    public string Nama_Lokasi { get; set; }
    public string Provinsi  { get; set; }
    public string Kabupaten  { get; set; }

    public ICollection<Wisata> Wisata { get; set; } = new List<Wisata>();

}
