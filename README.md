# DESKRIPSI
Asslamulaikum untuk website kelompok 4, Faiz dan Zaki berjudul Sistem Wisata yang menggunakan bahasa programan C-sharp dan databasenya dbeaver. jadi website yang kami buat memberikan informasi seputar deskripsi, Lokasi, Kebudayaan dan event wisata di Jawa tengah. Untuk Role nya memiliki 2 role  guest dan admin, pada role admin, Admin hanya bisa melakukan CRUD (Create, Read, Update, Delete) pada semua tabel sedangkan role guest hanya bisa mengakses tampilan depan website tanpa bisa masuk ke halaman admin. Pada halaman admin terdapat menu navbar yang berisi dropdown yang isinya ada tombol ke halaman depan dan tombol logout sedangkan di sidebar terdapat tombol navigasi ke dashboard dan tabel-tabel seperti kategori, lokasi, dan tempat wisata. pada halamnya depannya ada menu navbar yang berisikan menu beranda, destinasi wisata dan tentang kami. untuk halaman beranda menampilkan beberapa tempat wisata populer dan sedikit penjelasan tentang wisata di jawa tengah, sedangkan di halaman destinasi wisata menampilakan seluruh tempat wisata yang ada di jawa tengah beserta detailnya.

# DATABASE
untuk jumlah tabel pada website ini ada 3 diantaranya:
1. Tabel Kategori yang berisi id dan nama_kategori.
2. Tabel Lokasi yang berisi id, nama_lokasi, provinsi, dan kabupaten.
3. Tabel Tempat Wisata yang berisi nama_wisata, deskripsi, foto_wisata, id_kategori, dan id_lokasi.

Relasi antar tabel:
1. Tabel Lokasi ke Tabel Tempat Wisata: One-to-Many (satu lokasi bisa memiliki banyak tempat wisata).
2. Tabel Kategori ke Tabel Tempat Wisata: One-to-Many (satu kategori bisa memiliki banyak tempat wisata).

# ERD SISTEM INFORMASI WISATA PRODUCT
   
![Untitled](https://github.com/user-attachments/assets/a279d8b6-53b9-4292-a7c9-1a35be236e44)
