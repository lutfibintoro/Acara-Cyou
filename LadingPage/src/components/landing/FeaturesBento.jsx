import { motion } from "framer-motion";
import {
  BadgeCheck,
  BarChart3,
  BellRing,
  Globe,
  QrCode,
  ScanLine,
  Wallet,
} from "lucide-react";
import Reveal, { Eyebrow, EASE } from "@/components/landing/Reveal";
import { QrSvg } from "@/components/landing/TicketMockup";

const card =
  "group flex h-full flex-col rounded-3xl border border-slate-200/80 bg-white p-8 transition-all duration-300 hover:-translate-y-1 hover:border-indigo-200 hover:shadow-xl hover:shadow-indigo-600/10";
const badge =
  "inline-flex w-fit shrink-0 items-center rounded-full bg-mint-light px-3 py-1 text-[11px] font-extrabold text-emerald-700 ring-1 ring-emerald-100";
const iconChip =
  "flex h-11 w-11 shrink-0 items-center justify-center rounded-2xl bg-brand-soft text-indigo-600 ring-1 ring-indigo-100";

const PAYMENTS = ["QRIS", "GoPay", "OVO", "ShopeePay", "Dana", "Bank"];
const BARS = [45, 70, 55, 85, 65, 95];

export default function FeaturesBento() {
  return (
    <section id="fitur" className="scroll-mt-28 py-20 md:py-28">
      <div className="mx-auto max-w-6xl px-6 md:px-8">
        <Reveal className="max-w-2xl">
          <Eyebrow>Fitur unggulan</Eyebrow>
          <h2 className="font-display mt-3 text-3xl font-extrabold tracking-tight text-slate-900 sm:text-4xl">
            Semua kebutuhan panitia, dalam satu tempat.
          </h2>
          <p className="mt-4 text-base leading-relaxed text-slate-600 md:text-lg">
            Dari pembayaran sampai pintu masuk, urusan panitia dikerjakan otomatis.
          </p>
        </Reveal>

        <div className="mt-12 grid grid-cols-1 gap-6 md:grid-cols-3 lg:gap-8">
          <Reveal data-testid="bento-feature-payment" delay={0} className="md:col-span-2">
            <div className={card}>
              <div className="flex items-start justify-between gap-4">
                <span className={iconChip}><Wallet className="h-5 w-5" /></span>
                <span className={badge}>Terverifikasi otomatis</span>
              </div>
              <h3 className="mt-5 text-xl font-extrabold tracking-tight text-slate-900 md:text-2xl">
                Pembayaran otomatis &amp; aman
              </h3>
              <p className="mt-3 text-sm leading-relaxed text-slate-600 md:text-base">
                Peserta bayar via QRIS, e-wallet, atau transfer bank. Verifikasi jalan
                sendiri — panitia tidak lagi mengecek mutasi satu-satu.
              </p>
              <div className="mt-auto pt-6">
                <div className="flex flex-wrap gap-2">
                  {PAYMENTS.map((p) => (
                    <span key={p} className="rounded-lg bg-slate-100 px-3 py-1.5 text-[11px] font-bold text-slate-600">
                      {p}
                    </span>
                  ))}
                </div>
                <div className="mt-3 flex items-center gap-2 rounded-xl bg-mint-light px-4 py-3 text-xs font-bold text-emerald-700 ring-1 ring-emerald-100">
                  <BadgeCheck className="h-4 w-4 shrink-0" />
                  Pembayaran terverifikasi otomatis — tanpa cek mutasi manual
                </div>
              </div>
            </div>
          </Reveal>

          <Reveal data-testid="bento-feature-eticket" delay={0.08}>
            <div className={card}>
              <div className="flex items-start justify-between gap-4">
                <span className={iconChip}><QrCode className="h-5 w-5" /></span>
                <span className={badge}>Anti-palsu</span>
              </div>
              <h3 className="mt-5 text-xl font-extrabold tracking-tight text-slate-900">
                E-tiket dengan QR unik
              </h3>
              <p className="mt-3 text-sm leading-relaxed text-slate-600">
                Tiap peserta dapat tiket digital dengan QR unik yang tidak bisa
                dipalsukan atau dipakai dobel.
              </p>
              <div className="mt-auto pt-6">
                <div className="flex items-center gap-4 rounded-2xl border border-dashed border-slate-300 bg-slate-50 p-4">
                  <QrSvg className="h-14 w-14 shrink-0" />
                  <div className="space-y-2">
                    <div className="h-2 w-24 rounded bg-slate-200" />
                    <div className="h-2 w-16 rounded bg-slate-200" />
                    <div className="h-2 w-20 rounded bg-indigo-100" />
                  </div>
                </div>
              </div>
            </div>
          </Reveal>

          <Reveal data-testid="bento-feature-scan" delay={0}>
            <div className={card}>
              <div className="flex items-start justify-between gap-4">
                <span className={iconChip}><ScanLine className="h-5 w-5" /></span>
                <span className={badge}>&lt; 1 detik</span>
              </div>
              <h3 className="mt-5 text-xl font-extrabold tracking-tight text-slate-900">
                Scan QR di hari-H
              </h3>
              <p className="mt-3 text-sm leading-relaxed text-slate-600">
                Panitia cukup pakai kamera HP. Check-in selesai di bawah 1 detik
                per peserta.
              </p>
              <div className="relative mx-auto mt-auto h-24 w-24 pt-6">
                <span className="absolute left-0 top-0 h-6 w-6 rounded-tl-lg border-l-[3px] border-t-[3px] border-indigo-500" />
                <span className="absolute right-0 top-0 h-6 w-6 rounded-tr-lg border-r-[3px] border-t-[3px] border-indigo-500" />
                <span className="absolute bottom-0 left-0 h-6 w-6 rounded-bl-lg border-b-[3px] border-l-[3px] border-indigo-500" />
                <span className="absolute bottom-0 right-0 h-6 w-6 rounded-br-lg border-b-[3px] border-r-[3px] border-indigo-500" />
                <span className="absolute inset-0 flex items-center justify-center">
                  <QrCode className="h-10 w-10 text-slate-300" />
                </span>
                <span className="scanline absolute left-1 right-1 h-0.5 rounded-full bg-emerald-500/80" />
              </div>
            </div>
          </Reveal>

          <Reveal data-testid="bento-feature-dashboard" delay={0.08} className="md:col-span-2">
            <div className={card}>
              <div className="flex items-start justify-between gap-4">
                <span className={iconChip}><BarChart3 className="h-5 w-5" /></span>
                <span className={badge}>Live</span>
              </div>
              <h3 className="mt-5 text-xl font-extrabold tracking-tight text-slate-900 md:text-2xl">
                Dashboard real-time
              </h3>
              <p className="mt-3 text-sm leading-relaxed text-slate-600 md:text-base">
                Pantau pendaftar, pendapatan, dan tingkat kehadiran secara live.
                Ekspor data ke CSV/Excel kapan saja dengan satu klik.
              </p>
              <div className="mt-auto flex items-end gap-2.5 pt-6">
                {BARS.map((h, i) => (
                  <motion.div
                    key={i}
                    className={`flex-1 rounded-t-lg ${i === BARS.length - 1 ? "bg-indigo-600" : "bg-indigo-100"}`}
                    initial={{ height: 0 }}
                    whileInView={{ height: h }}
                    viewport={{ once: true, margin: "-60px" }}
                    transition={{ duration: 0.9, delay: 0.15 + i * 0.07, ease: EASE }}
                  />
                ))}
              </div>
            </div>
          </Reveal>

          <Reveal data-testid="bento-feature-landingpage" delay={0} className="md:col-span-2">
            <div className={card}>
              <div className="flex items-start justify-between gap-4">
                <span className={iconChip}><Globe className="h-5 w-5" /></span>
                <span className="inline-flex w-fit shrink-0 items-center rounded-full bg-brand-soft px-3 py-1 font-mono text-[11px] font-bold text-indigo-700 ring-1 ring-indigo-100">
                  acara.cyou/e/nama-acaramu
                </span>
              </div>
              <h3 className="mt-5 text-xl font-extrabold tracking-tight text-slate-900 md:text-2xl">
                Halaman acara instan
              </h3>
              <p className="mt-3 text-sm leading-relaxed text-slate-600 md:text-base">
                Link acara sendiri berisi poster, deskripsi, dan rundown — siap
                disebar di status WhatsApp dan bio Instagram.
              </p>
              <div className="mt-auto overflow-hidden rounded-2xl border border-slate-200 bg-slate-50 pt-0">
                <div className="flex items-center gap-2 border-b border-slate-200 bg-white px-4 py-2.5">
                  <span className="h-2.5 w-2.5 rounded-full bg-rose-300" />
                  <span className="h-2.5 w-2.5 rounded-full bg-amber-300" />
                  <span className="h-2.5 w-2.5 rounded-full bg-emerald-300" />
                  <span className="ml-2 rounded-md bg-slate-100 px-2 py-0.5 font-mono text-[10px] text-slate-500">
                    acara.cyou/e/himtek-fest
                  </span>
                </div>
                <div className="space-y-2 p-4">
                  <div className="h-12 w-full rounded-lg bg-indigo-100" />
                  <div className="h-2 w-3/4 rounded bg-slate-200" />
                  <div className="h-2 w-1/2 rounded bg-slate-200" />
                </div>
              </div>
            </div>
          </Reveal>

          <Reveal data-testid="bento-feature-notification" delay={0.08}>
            <div className={card}>
              <div className="flex items-start justify-between gap-4">
                <span className={iconChip}><BellRing className="h-5 w-5" /></span>
                <span className={badge}>Auto-reminder</span>
              </div>
              <h3 className="mt-5 text-xl font-extrabold tracking-tight text-slate-900">
                Pengingat Email
              </h3>
              <p className="mt-3 text-sm leading-relaxed text-slate-600">
                Notifikasi otomatis ke peserta sebelum acara dimulai, biar angka
                kehadiran maksimal.
              </p>
              <div className="mt-auto pt-6">
                <div className="max-w-[240px] rounded-2xl rounded-tl-sm bg-mint-light px-4 py-3 text-xs font-medium leading-relaxed text-slate-700 ring-1 ring-emerald-100">
                  Besok 09.00 acara dimulai - jangan lupa tunjukkan e-tiketmu!
                </div>
                <p className="mt-1 text-right text-[10px] font-medium text-slate-400">09.00</p>
              </div>
            </div>
          </Reveal>
        </div>
      </div>
    </section>
  );
}
