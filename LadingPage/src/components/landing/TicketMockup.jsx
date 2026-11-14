import { motion } from "framer-motion";
import { BadgeCheck } from "lucide-react";
import { EASE } from "@/components/landing/Reveal";

const MODULES = 21;
const CELL = 4;
const SIZE = MODULES * CELL;
const inFinder = (r, c) => (r < 7 && c < 7) || (r < 7 && c >= 14) || (r >= 14 && c < 7);

const CELLS = [];
for (let r = 0; r < MODULES; r++) {
  for (let c = 0; c < MODULES; c++) {
    if (!inFinder(r, c) && (r * 7 + c * 11 + ((r * c) % 9) * 3) % 4 < 2) CELLS.push([r, c]);
  }
}
const FINDERS = [[0, 0], [0, 14], [14, 0]];

function QrSvg({ className = "h-40 w-40" }) {
  return (
    <svg viewBox={`0 0 ${SIZE} ${SIZE}`} className={className} role="img" aria-label="QR code e-tiket">
      <rect width={SIZE} height={SIZE} fill="white" />
      {CELLS.map(([r, c], i) => (
        <rect key={i} x={c * CELL} y={r * CELL} width={CELL} height={CELL} fill="#0F172A" />
      ))}
      {FINDERS.map(([r, c], i) => (
        <g key={`f${i}`}>
          <rect x={c * CELL} y={r * CELL} width={7 * CELL} height={7 * CELL} fill="#0F172A" rx={6} />
          <rect x={(c + 1) * CELL} y={(r + 1) * CELL} width={5 * CELL} height={5 * CELL} fill="white" />
          <rect x={(c + 2) * CELL} y={(r + 2) * CELL} width={3 * CELL} height={3 * CELL} fill="#0F172A" />
        </g>
      ))}
    </svg>
  );
}

export { QrSvg };
export default TicketMockup;

export function TicketMockup() {
  return (
    <div className="relative" data-testid="hero-mockup-ticket">
      <div className="animate-floaty relative w-[280px] rounded-[2.6rem] border border-slate-200/80 bg-white p-2.5 shadow-glow sm:w-[310px]">
        <div className="overflow-hidden rounded-[2.1rem] bg-slate-50">
          <div className="bg-indigo-600 px-6 pb-9 pt-7 text-white">
            <div className="flex items-center justify-between text-[10px] font-extrabold uppercase tracking-[0.22em] text-indigo-200">
              <span>E-Tiket</span>
              <span>No. 0214</span>
            </div>
            <p className="font-display mt-3 text-lg font-extrabold leading-snug">
              Seminar Nasional Teknologi 2025
            </p>
            <p className="mt-1 text-xs font-medium text-indigo-200">
              Sabtu, 14 Juni • Aula Kampus
            </p>
          </div>
          <div className="-mt-5 rounded-t-[1.6rem] bg-white px-6 pb-7 pt-6">
            <div className="flex items-center justify-between gap-3">
              <div>
                <p className="text-sm font-extrabold text-slate-900">Budi Pratama</p>
                <p className="text-xs text-slate-500">HIMA Teknik Informatika</p>
              </div>
              <span className="inline-flex shrink-0 items-center gap-1 rounded-full bg-mint-light px-2.5 py-1 text-[10px] font-extrabold uppercase tracking-wide text-emerald-700 ring-1 ring-emerald-200">
                <BadgeCheck className="h-3 w-3" /> Lunas
              </span>
            </div>
            <div className="relative mx-auto mt-5 w-fit rounded-2xl border border-slate-200 bg-white p-3">
              <QrSvg className="h-36 w-36" />
              <div className="scanline absolute left-2 right-2 h-0.5 rounded-full bg-indigo-500/70" />
            </div>
            <p className="mt-3 text-center text-[10px] font-bold uppercase tracking-[0.2em] text-slate-400">
              Tunjukkan QR di pintu masuk
            </p>
          </div>
        </div>
      </div>

      <div className="animate-floaty-slow absolute -top-5 -right-2 flex items-center gap-3 rounded-2xl border border-slate-200/80 bg-white/95 px-4 py-3 shadow-soft backdrop-blur sm:-right-8">
        <span className="flex h-9 w-9 items-center justify-center rounded-full bg-mint-light ring-1 ring-emerald-200">
          <BadgeCheck className="h-4 w-4 text-emerald-600" />
        </span>
        <span>
          <span className="block text-xs font-extrabold text-slate-900">QRIS diterima</span>
          <span className="block text-[11px] text-slate-500">Rp 75.000 • otomatis</span>
        </span>
      </div>

      <div className="animate-floaty absolute -left-2 bottom-2 w-48 rounded-2xl border border-slate-200/80 bg-white/95 p-4 shadow-soft backdrop-blur sm:-left-14">
        <div className="flex items-center justify-between text-[11px] font-extrabold text-slate-700">
          <span>Check-in hari ini</span>
          <span className="text-emerald-600">93%</span>
        </div>
        <div className="mt-2 h-1.5 overflow-hidden rounded-full bg-slate-100">
          <motion.div
            className="h-full rounded-full bg-emerald-500"
            initial={{ width: 0 }}
            animate={{ width: "93%" }}
            transition={{ duration: 1.2, delay: 1.4, ease: EASE }}
          />
        </div>
        <p className="mt-2 text-[10px] font-medium text-slate-500">214 dari 230 peserta hadir</p>
      </div>
    </div>
  );
}
