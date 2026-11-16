# Acara.Cyou

**ini** adalah source code untuk platform pendaftaran dan manajemen acara yang ditujukan bagi panitia kecil, seperti himpunan, komunitas, dan acara kampus. Platform ini membantu panitia mengelola pendaftaran peserta, pembayaran, e-tiket, hingga proses check-in pada hari acara.

## Deskripsi

Acara.Cyou menyediakan sistem pendaftaran acara dengan pembayaran QRIS otomatis, e-tiket ber-QR unik, dan fitur check-in melalui pemindaian QR code. Tujuannya adalah mempermudah panitia dalam mengelola acara secara lebih rapi, cepat, dan terorganisir.

## Fitur

### Fitur yang sudah selesai

* Login menggunakan OAuth Google
* Manajemen event
* Dashboard event
* Manajemen tiket untuk setiap event
* Form builder
* Custom URL

### Fitur yang masih dalam pengembangan

* Pembayaran
* Form pendaftaran
* Pengiriman e-tiket ke peserta
* Scan e-tiket

## Manfaat

* Memudahkan panitia membuat dan mengelola event
* Membantu peserta melakukan pendaftaran secara online
* Mendukung pembayaran menggunakan QRIS
* Menghasilkan e-tiket dengan QR code unik untuk setiap peserta
* Mempercepat proses check-in peserta pada hari acara
* Memberikan dashboard untuk memantau event dan peserta
* Cocok digunakan oleh komunitas, himpunan, organisasi, dan penyelenggara acara kampus



## Cara Menjalankan

### Persyaratan aplikasi

Pastikan aplikasi berikut sudah terinstal di WSL (Ubuntu):

* Git Bash
* WSL dengan Ubuntu
* Docker

### Langkah-Langkah menjalankan

1. Clone repository ini menggunakan Git Bash:

```bash
   git clone https://github.com/lutfibintoro/Acara-Cyou.git
   ```

2. Buka WSL (Ubuntu), lalu masuk ke folder hasil clone:

```bash
   cd Acara-Cyou
   ```

3. Salin file konfigurasi contoh menjadi file konfigurasi utama:

```bash
   cp ./WebServer/Appsettings.example.json ./WebServer/Appsettings.json
   ```

4. Pastikan file `docker-compose.yml` tersedia di root project:

```bash
   ls
   ```

Pastikan Anda melihat file bernama `docker-compose.yml`.

5. Buat container Docker:

```bash
   sudo docker compose create
   ```

6. Jalankan container Docker:

```bash
   sudo docker compose start
   ```

7. Buka aplikasi melalui browser:

```text
   http://localhost:7116 lading page
   http://localhost:7115 dashboard
   http://localhost:8081/scalar dokumentasi api backend
   ```

## Catatan

* Jalankan semua perintah di atas dari folder root project.
* Jika `docker compose` tidak dikenali, pastikan Docker sudah berjalan dan terinstal dengan benar.
* Jika port `7116, 7115, 8081` sudah digunakan, hentikan aplikasi atau container lain yang memakai port tersebut terlebih dahulu.

