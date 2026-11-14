import { motion, useMotionValue, useSpring } from "framer-motion";
import { ArrowRight, QrCode, ShieldCheck, Sparkles, Timer } from "lucide-react";
import TicketMockup from "@/components/landing/TicketMockup";
import { scrollToHash } from "@/components/landing/Navbar";
import { EASE } from "@/components/landing/Reveal";

export const DASHBOARD_URL = import.meta.env.VITE_DASHBOARD_URL;

export default function Hero() {
  const rx = useMotionValue(0);
  const ry = useMotionValue(0);
  const srx = useSpring(rx, { stiffness: 100, damping: 18 });
  const sry = useSpring(ry, { stiffness: 100, damping: 18 });

  const onMove = (e) => {
    const r = e.currentTarget.getBoundingClientRect();
    const x = (e.clientX - r.left) / r.width - 0.5;
    const y = (e.clientY - r.top) / r.height - 0.5;
    ry.set(x * 10);
    rx.set(-y * 8);
  };
  const onLeave = () => {
    rx.set(0);
    ry.set(0);
  };
      
  return (
    <section className="relative overflow-hidden pb-20 pt-32 md:pb-28 md:pt-44">
      <div className="hero-grid absolute inset-0" aria-hidden="true" />
      <div
        className="absolute -top-32 left-1/2 h-[480px] w-[820px] max-w-full -translate-x-1/2 rounded-full bg-indigo-200/50 blur-3xl"
        aria-hidden="true"
      />
      <div
        className="absolute right-[-140px] top-1/3 h-[380px] w-[380px] rounded-full bg-emerald-200/40 blur-3xl"
        aria-hidden="true"
      />

      <div className="relative mx-auto grid max-w-6xl items-center gap-16 px-6 md:px-8 lg:grid-cols-[1.05fr_0.95fr]">
        <div>
          <motion.div
            initial={{ opacity: 0, y: 16 }}
            animate={{ opacity: 1, y: 0 }}
            transition={{ duration: 0.6, delay: 0.05, ease: EASE }}
            data-testid="hero-badge"
            className="inline-flex items-center gap-2 rounded-full border border-indigo-100 bg-white px-4 py-1.5 text-xs font-bold text-indigo-700 shadow-sm"
          >
            <Sparkles className="h-3.5 w-3.5 text-indigo-500" />
            Dibuat khusus untuk acara dan event
          </motion.div>

          <h1
            data-testid="hero-headline"
            className="font-display mt-6 text-4xl font-extrabold leading-[1.08] tracking-tight text-slate-900 sm:text-5xl lg:text-[56px] lg:leading-[1.06]"
          >
            <span className="block overflow-hidden pb-1">
              <motion.span
                className="block"
                initial={{ y: "112%" }}
                animate={{ y: 0 }}
                transition={{ duration: 0.9, delay: 0.15, ease: EASE }}
              >
                Stop pakai
              </motion.span>
            </span>
            <span className="block overflow-hidden pb-1">
              <motion.span
                className="block"
                initial={{ y: "112%" }}
                animate={{ y: 0 }}
                transition={{ duration: 0.9, delay: 0.26, ease: EASE }}
              >
                Google Forms
              </motion.span>
            </span>
            <span className="block overflow-hidden pb-2">
              <motion.span
                className="block"
                initial={{ y: "112%" }}
                animate={{ y: 0 }}
                transition={{ duration: 0.9, delay: 0.37, ease: EASE }}
              >
                untuk acara{" "}
                <span className="relative inline-block text-indigo-600">
                  berbayar.
                  <motion.svg
                    className="absolute -bottom-1.5 left-0 h-2.5 w-full"
                    viewBox="0 0 300 12"
                    preserveAspectRatio="none"
                    fill="none"
                    aria-hidden="true"
                  >
                    <motion.path
                      d="M4 9C70 3 230 3 296 8"
                      stroke="#059669"
                      strokeWidth="5"
                      strokeLinecap="round"
                      initial={{ pathLength: 0 }}
                      animate={{ pathLength: 1 }}
                      transition={{ duration: 0.7, delay: 1.05, ease: "easeOut" }}
                    />
                  </motion.svg>
                </span>
              </motion.span>
            </span>
          </h1>

          <motion.p
            data-testid="hero-subtitle"
            initial={{ opacity: 0, y: 20 }}
            animate={{ opacity: 1, y: 0 }}
            transition={{ duration: 0.7, delay: 0.55, ease: EASE }}
            className="mt-6 max-w-xl text-base leading-relaxed text-slate-600 md:text-lg"
          >
            Acara.Cyou mengurus pendaftaran, pembayaran, dan e-tiket secara otomatis —
            plus scan QR super cepat di hari-H. Tanpa rekap transfer, tanpa Excel, tanpa drama.
          </motion.p>

          <motion.div
            initial={{ opacity: 0, y: 20 }}
            animate={{ opacity: 1, y: 0 }}
            transition={{ duration: 0.7, delay: 0.7, ease: EASE }}
            className="mt-8 flex flex-wrap items-center gap-4"
          >
            <a
              href={DASHBOARD_URL}
              target="_blank"
              rel="noopener noreferrer"
              data-testid="hero-cta-primary"
              className="group inline-flex items-center gap-2 rounded-full bg-indigo-600 px-7 py-3.5 text-sm font-bold text-white shadow-xl shadow-indigo-600/25 transition-all hover:bg-indigo-700 hover:shadow-indigo-600/35 md:text-base"
            >
              Mulai Gratis Sekarang
              <ArrowRight className="h-4 w-4 transition-transform group-hover:translate-x-1" />
            </a>
            <a
              href="#cara-kerja"
              data-testid="hero-cta-secondary"
              onClick={(e) => scrollToHash(e, "#cara-kerja")}
              className="inline-flex items-center gap-2 rounded-full border border-slate-200 bg-white px-7 py-3.5 text-sm font-bold text-slate-700 transition-all hover:border-indigo-300 hover:text-indigo-700 md:text-base"
            >
              Lihat Cara Kerja
            </a>
          </motion.div>

          <motion.div
            initial={{ opacity: 0 }}
            animate={{ opacity: 1 }}
            transition={{ duration: 0.8, delay: 0.9 }}
            className="mt-8 flex flex-wrap gap-x-6 gap-y-2 text-xs font-semibold text-slate-500 md:text-sm"
          >
            <span className="flex items-center gap-1.5">
              <ShieldCheck className="h-4 w-4 text-emerald-600" /> Gratis, tanpa biaya tersembunyi
            </span>
            <span className="flex items-center gap-1.5">
              <Timer className="h-4 w-4 text-indigo-500" /> Setup 3 menit
            </span>
            <span className="flex items-center gap-1.5">
              <QrCode className="h-4 w-4 text-indigo-500" /> Check-in di bawah 1 detik
            </span>
          </motion.div>
        </div>

        <motion.div
          initial={{ opacity: 0, y: 40 }}
          animate={{ opacity: 1, y: 0 }}
          transition={{ duration: 1, delay: 0.5, ease: EASE }}
          className="relative flex justify-center px-4 lg:justify-end lg:px-8"
          onMouseMove={onMove}
          onMouseLeave={onLeave}
        >
          <motion.div style={{ rotateX: srx, rotateY: sry, transformPerspective: 1000 }} className="relative">
            <TicketMockup />
          </motion.div>
        </motion.div>
      </div>
    </section>
  );
}
