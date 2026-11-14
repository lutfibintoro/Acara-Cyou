import { ArrowRight, CheckCircle2, XCircle } from "lucide-react";
import Reveal, { Eyebrow } from "@/components/landing/Reveal";

const PROBLEMS = [
  "Rekap ratusan bukti transfer di spreadsheet — satu-satu dicek manual.",
  "Konfirmasi pembayaran lewat WhatsApp satu per satu sampai tengah malam.",
  "Bukti transfer palsu atau dobel lolos tanpa sistem verifikasi.",
  "Hari-H: antrean mengular karena absen lewat printout nama.",
];

const SOLUTIONS = [
  "Pembayaran QRIS & e-wallet terverifikasi otomatis — langsung masuk rekap.",
  "E-tiket ber-QR unik terbit dan terkirim ke peserta seketika.",
  "Dashboard real-time: pendaftar, pendapatan, kehadiran — tanpa Excel.",
  "Hari-H: scan QR di pintu, selesai di bawah 1 detik per peserta.",
];

export default function ProblemSolution() {
  return (
    <section id="masalah" className="scroll-mt-28 py-20 md:py-28">
      <div className="mx-auto max-w-6xl px-6 md:px-8">
        <Reveal className="max-w-2xl">
          <Eyebrow>Kenapa pindah</Eyebrow>
          <h2 className="font-display mt-3 text-3xl font-extrabold tracking-tight text-slate-900 sm:text-4xl">
            Google Forms gratis — tapi mahal waktunya.
          </h2>
        </Reveal>

        <div className="relative mt-12 grid gap-6 lg:grid-cols-2 lg:gap-10">
          <Reveal data-testid="problem-card-container" delay={0.05} className="h-full">
            <div className="h-full rounded-3xl border border-rose-100 bg-white p-8 md:p-10">
              <div className="flex items-center gap-3">
                <span className="flex h-10 w-10 items-center justify-center rounded-xl bg-rose-50 text-rose-600 ring-1 ring-rose-100">
                  <XCircle className="h-5 w-5" />
                </span>
                <h3 className="text-lg font-extrabold text-slate-900 md:text-xl">
                  Pakai Google Forms
                </h3>
              </div>
              <ul className="mt-7 space-y-5">
                {PROBLEMS.map((p, i) => (
                  <li key={i} className="flex gap-3 text-sm leading-relaxed text-slate-600 md:text-base">
                    <XCircle className="mt-0.5 h-5 w-5 shrink-0 text-rose-400" />
                    {p}
                  </li>
                ))}
              </ul>
            </div>
          </Reveal>

          <Reveal data-testid="solution-card-container" delay={0.15} className="h-full">
            <div className="relative h-full rounded-3xl bg-indigo-600 p-8 text-white shadow-glow md:p-10">
              <div className="flex items-center gap-3">
                <span className="flex h-10 w-10 items-center justify-center rounded-xl bg-white/15 text-emerald-300 ring-1 ring-white/20">
                  <CheckCircle2 className="h-5 w-5" />
                </span>
                <h3 className="text-lg font-extrabold md:text-xl">Pakai Acara.Cyou</h3>
              </div>
              <ul className="mt-7 space-y-5">
                {SOLUTIONS.map((s, i) => (
                  <li key={i} className="flex gap-3 text-sm leading-relaxed text-indigo-100 md:text-base">
                    <CheckCircle2 className="mt-0.5 h-5 w-5 shrink-0 text-emerald-300" />
                    {s}
                  </li>
                ))}
              </ul>
            </div>
          </Reveal>

          <div className="absolute left-1/2 top-1/2 z-10 hidden h-14 w-14 -translate-x-1/2 -translate-y-1/2 items-center justify-center rounded-full bg-white shadow-lg ring-1 ring-slate-200 lg:flex">
            <ArrowRight className="h-5 w-5 text-indigo-600" />
          </div>
        </div>
      </div>
    </section>
  );
}
