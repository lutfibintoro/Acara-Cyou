# Acara.Cyou

Panduan ini menjelaskan langkah-langkah untuk menjalankan proyek **Acara.Cyou** secara lokal menggunakan WSL (Ubuntu) dan Docker.

## Prasyarat

Pastikan aplikasi berikut sudah terinstal di WSL (Ubuntu):

- Git Bash
- WSL dengan Ubuntu
- Docker

## Cara Menjalankan

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
   http://localhost:7116
   ```

## Catatan

- Jalankan semua perintah di atas dari folder root project.
- Jika `docker compose` tidak dikenali, pastikan Docker sudah berjalan dan terinstal dengan benar.
- Jika port `7116` sudah digunakan, hentikan aplikasi atau container lain yang memakai port tersebut terlebih dahulu.