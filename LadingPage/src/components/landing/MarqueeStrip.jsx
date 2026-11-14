import Component from "react-fast-marquee";
import { Asterisk } from "lucide-react";

// Fallback jika import modul terbungkus objek `.default`
const Marquee = Component.default || Component;

const ITEMS = [
  "Konser Kampus",
  "Seminar Himpunan",
  "Workshop Kreatif",
  "Lomba Mahasiswa",
  "Gathering Alumni",
  "Webinar Nasional",
  "Turnamen Esports",
  "Komunitas Meetup",
];

export default function MarqueeStrip() {
  return (
    <div data-testid="event-marquee-strip" className="overflow-hidden border-y border-slate-200/80 bg-white py-5">
      <Marquee speed={30} gradient gradientColor="rgb(248, 249, 251)" gradientWidth={90} pauseOnHover>
        {ITEMS.map((it, i) => (
          <span key={i} className="flex items-center gap-8 px-4">
            <span className="text-sm font-extrabold uppercase tracking-[0.25em] text-slate-400">
              {it}
            </span>
            {Asterisk ? (
              <Asterisk className="h-4 w-4 text-indigo-500" />
            ) : (
              <span className="text-indigo-500 font-bold">*</span>
            )}
          </span>
        ))}
      </Marquee>
    </div>
  );
}