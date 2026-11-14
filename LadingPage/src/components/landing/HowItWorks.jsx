import { motion } from "framer-motion";
import { CalendarPlus, QrCode, Share2 } from "lucide-react";
import Reveal, { Eyebrow, EASE } from "@/components/landing/Reveal";

const STEPS = [
  {
    no: "01",
    icon: CalendarPlus,
    title: "Buat acara dalam 3 menit",
    desc: "Isi detail, unggah poster, tentukan harga tiket — gratis atau berbayar. Link pendaftaran langsung jadi.",
  },
  {
    no: "02",
    icon: Share2,
    title: "Sebar link, terima pembayaran",
    desc: "Bagikan ke grup WhatsApp atau bio Instagram. Peserta daftar dan bayar tanpa panitia ikut campur.",
  },
  {
    no: "03",
    icon: QrCode,
    title: "Scan QR saat hari-H",
    desc: "Panitia di pintu tinggal scan e-tiket peserta. Kehadiran tercatat otomatis, bebas antrean.",
  },
];

export default function HowItWorks() {
  return (
    <section id="cara-kerja" className="scroll-mt-28 bg-white py-20 md:py-28">
      <div className="mx-auto max-w-6xl px-6 md:px-8">
        <Reveal className="max-w-2xl">
          <Eyebrow>Cara kerja</Eyebrow>
          <h2 className="font-display mt-3 text-3xl font-extrabold tracking-tight text-slate-900 sm:text-4xl">
            Tiga langkah, dari nol sampai hari-H.
          </h2>
        </Reveal>

        <div className="mt-12 grid gap-6 md:grid-cols-3 lg:gap-8">
          {STEPS.map((s, i) => (
            <Reveal key={s.no} data-testid={`step-${i + 1}`} delay={i * 0.1} className="h-full">
              <div className="relative h-full overflow-hidden rounded-3xl border border-slate-200/80 bg-[#F8F9FB] p-8 transition-all duration-300 hover:border-indigo-200 hover:shadow-xl hover:shadow-indigo-600/10">
                <span className="font-display pointer-events-none absolute -top-3 right-5 text-6xl font-extrabold text-indigo-100">
                  {s.no}
                </span>
                <span className="flex h-11 w-11 items-center justify-center rounded-2xl bg-indigo-600 text-white shadow-lg shadow-indigo-600/25">
                  <s.icon className="h-5 w-5" />
                </span>
                <h3 className="mt-6 text-lg font-extrabold tracking-tight text-slate-900">
                  {s.title}
                </h3>
                <p className="mt-3 text-sm leading-relaxed text-slate-600">{s.desc}</p>
              </div>
            </Reveal>
          ))}
        </div>

        <Reveal delay={0.1} className="mt-14">
          <div className="relative overflow-hidden rounded-[2.5rem] border border-slate-200/80 shadow-soft">
            <motion.img
              src="https://images.pexels.com/photos/36979662/pexels-photo-36979662.jpeg?auto=compress&cs=tinysrgb&w=1600"
              alt="Workshop komunitas kampus yang ramai dan tertata"
              className="h-[300px] w-full object-cover md:h-[420px]"
              initial={{ scale: 1.14 }}
              whileInView={{ scale: 1 }}
              viewport={{ once: true, margin: "-80px" }}
              transition={{ duration: 1.5, ease: EASE }}
            />
            <div className="pointer-events-none absolute inset-x-0 bottom-0 h-32 bg-gradient-to-t from-slate-900/50 to-transparent" />
            <span className="glass absolute bottom-5 left-5 rounded-full px-4 py-2 text-xs font-extrabold text-slate-800 shadow-sm">
              Hari-H: satu scan, langsung tercatat hadir
            </span>
          </div>
        </Reveal>
      </div>
    </section>
  );
}
