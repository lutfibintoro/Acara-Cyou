import { useState } from "react";
import { AnimatePresence, motion } from "framer-motion";
import { ArrowRight, ChevronDown } from "lucide-react";
import Reveal, { Eyebrow, EASE } from "@/components/landing/Reveal";

export const DASHBOARD_URL = import.meta.env.VITE_DASHBOARD_URL;

const FAQS = [
  {
    q: "Apakah Acara.Cyou benar-benar 100% gratis digunakan?",
    a: "Ya! Acara.Cyou sepenuhnya gratis untuk panitia himpunan, komunitas kampus, dan organisasi mahasiswa — tanpa biaya langganan bulanan.",
  },
  {
    q: "Metode pembayaran apa saja yang didukung untuk peserta?",
    a: "Peserta dapat membayar tiket menggunakan QRIS (GoPay, OVO, Dana, ShopeePay, LinkAja) serta transfer bank dari seluruh bank di Indonesia.",
  },
  {
    q: "Bagaimana cara panitia melakukan scan QR e-tiket di venue saat hari-H?",
    a: "Panitia cukup membuka dashboard Acara.Cyou di browser HP dan mengaktifkan mode scanner kamera. Tanpa perlu mengunduh aplikasi tambahan!",
  },
  {
    q: "Bagaimana jika acara saya gratis (free event)?",
    a: "Sangat bisa! Setel harga Rp 0 dan peserta tetap mendapatkan e-tiket resmi dengan QR code untuk check-in di lokasi acara.",
  },
  {
    q: "Apakah data pendaftar bisa diunduh ke Excel/CSV?",
    a: "Bisa banget! Semua data peserta, status pembayaran, dan waktu kehadiran dapat diunduh kapan saja dalam format .xlsx/.csv dengan satu klik.",
  },
];

export default function Faq() {
  const [open, setOpen] = useState(0);

  return (
    <section id="faq" className="scroll-mt-28 bg-white py-20 md:py-28">
      <div className="mx-auto grid max-w-6xl gap-12 px-6 md:px-8 lg:grid-cols-[0.85fr_1.15fr]">
        <Reveal>
          <Eyebrow>FAQ</Eyebrow>
          <h2 className="font-display mt-3 text-3xl font-extrabold tracking-tight text-slate-900 sm:text-4xl">
            Pertanyaan yang sering muncul.
          </h2>
          <p className="mt-4 text-base leading-relaxed text-slate-600">
            Masih penasaran? Coba langsung — gratis.
          </p>
          <a
            href={DASHBOARD_URL}
            target="_blank"
            rel="noopener noreferrer"
            data-testid="faq-cta-dashboard"
            className="group mt-5 inline-flex items-center gap-2 text-sm font-extrabold text-indigo-600 transition-colors hover:text-indigo-800"
          >
            Buka dashboard sekarang
            <ArrowRight className="h-4 w-4 transition-transform group-hover:translate-x-1" />
          </a>
        </Reveal>

        <div className="space-y-4">
          {FAQS.map((f, i) => {
            const isOpen = open === i;
            return (
              <Reveal key={i} data-testid={`faq-item-${i + 1}`} delay={i * 0.06}>
                <div
                  className={`overflow-hidden rounded-2xl border transition-colors ${
                    isOpen ? "border-indigo-200 bg-[#F8F9FB]" : "border-slate-200/80 bg-white"
                  }`}
                >
                  <button
                    data-testid={`faq-trigger-${i + 1}`}
                    onClick={() => setOpen(isOpen ? -1 : i)}
                    aria-expanded={isOpen}
                    className="flex w-full items-center justify-between gap-4 p-6 text-left"
                  >
                    <span className="text-sm font-extrabold text-slate-900 md:text-base">{f.q}</span>
                    <ChevronDown
                      className={`h-5 w-5 shrink-0 text-indigo-500 transition-transform duration-300 ${
                        isOpen ? "rotate-180" : ""
                      }`}
                    />
                  </button>
                  <AnimatePresence initial={false}>
                    {isOpen && (
                      <motion.div
                        initial={{ height: 0, opacity: 0 }}
                        animate={{ height: "auto", opacity: 1 }}
                        exit={{ height: 0, opacity: 0 }}
                        transition={{ duration: 0.35, ease: EASE }}
                        className="overflow-hidden"
                      >
                        <p className="px-6 pb-6 text-sm leading-relaxed text-slate-600 md:text-base">
                          {f.a}
                        </p>
                      </motion.div>
                    )}
                  </AnimatePresence>
                </div>
              </Reveal>
            );
          })}
        </div>
      </div>
    </section>
  );
}
