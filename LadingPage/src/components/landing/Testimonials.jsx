import { Star } from "lucide-react";
import Reveal, { Eyebrow } from "@/components/landing/Reveal";

const TESTIMONIALS = [
  {
    quote:
      "Dulu bikin konser himpunan buat 500 peserta, panitia pusing rekap bukti transfer dari Google Forms. Sekarang sistem yang verifikasi, dan pas hari-H scan QR-nya ngebut — gak ada antrean.",
    author: "Rian Ardiansyah",
    role: "Ketua HIMA Teknik Informatika",
    event: "Konser musik kampus",
    initials: "RA",
  },
  {
    quote:
      "Workshop kami cuma 50 seat dan selalu penuh. Tiket otomatis terkunci pas kuota habis, jadi gak ada drama overbooking lagi.",
    author: "Nabila Putri",
    role: "Lead Organizer Komunitas Desain",
    event: "Workshop UI/UX",
    initials: "NP",
  },
  {
    quote:
      "Peserta dapat e-tiket langsung lewat WhatsApp, dan kami punya rekap keuangan yang rapi buat laporan ke kampus. Jauh lebih profesional.",
    author: "Fikri Ramadhan",
    role: "Ketua Panitia Seminar Nasional",
    event: "Seminar nasional",
    initials: "FR",
  },
];

const TONES = [
  "bg-indigo-100 text-indigo-700",
  "bg-emerald-100 text-emerald-700",
  "bg-amber-100 text-amber-700",
];

function Stars() {
  return (
    <div className="flex gap-0.5">
      {Array.from({ length: 5 }).map((_, i) => (
        <Star key={i} className="h-4 w-4 fill-amber-400 text-amber-400" />
      ))}
    </div>
  );
}

export default function Testimonials() {
  return (
    <section id="testimoni" className="scroll-mt-28 py-20 md:py-28">
      <div className="mx-auto max-w-6xl px-6 md:px-8">
        <Reveal className="max-w-2xl">
          <Eyebrow>Kata mereka</Eyebrow>
          <h2 className="font-display mt-3 text-3xl font-extrabold tracking-tight text-slate-900 sm:text-4xl">
            Cerita panitia yang sudah pindah.
          </h2>
        </Reveal>

        <div className="mt-12 grid gap-6 md:grid-cols-3 lg:gap-8">
          {TESTIMONIALS.map((t, i) => (
            <Reveal key={t.author} data-testid={`testimonial-card-${i + 1}`} delay={i * 0.1} className="h-full">
              <div className="flex h-full flex-col rounded-3xl border border-slate-200/80 bg-white p-8 transition-all duration-300 hover:-translate-y-1 hover:border-indigo-200 hover:shadow-xl hover:shadow-indigo-600/10">
                <div className="flex items-center justify-between gap-3">
                  <Stars />
                  <span className="rounded-full bg-brand-soft px-3 py-1 text-[11px] font-extrabold text-indigo-700 ring-1 ring-indigo-100">
                    {t.event}
                  </span>
                </div>
                <p className="mt-5 text-sm leading-relaxed text-slate-700 md:text-[15px]">
                  &ldquo;{t.quote}&rdquo;
                </p>
                <div className="mt-auto flex items-center gap-3 border-t border-slate-100 pt-6 text-left">
                  <span
                    className={`flex h-11 w-11 shrink-0 items-center justify-center rounded-full text-sm font-extrabold ring-2 ring-white ${TONES[i]}`}
                  >
                    {t.initials}
                  </span>
                  <span>
                    <span className="block text-sm font-extrabold text-slate-900">{t.author}</span>
                    <span className="block text-xs text-slate-500">{t.role}</span>
                  </span>
                </div>
              </div>
            </Reveal>
          ))}
        </div>
      </div>
    </section>
  );
}
