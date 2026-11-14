import { ArrowRight } from "lucide-react";
import Reveal, { EASE } from "@/components/landing/Reveal";

export const DASHBOARD_URL = import.meta.env.VITE_DASHBOARD_URL;

export default function FinalCta() {
  return (
    <section className="py-20 md:py-28">
      <div className="mx-auto max-w-6xl px-6 md:px-8">
        <Reveal data-testid="final-cta-banner">
          <div className="relative overflow-hidden rounded-[2.5rem] bg-indigo-600 px-8 py-16 text-center md:py-24">
            <div
              className="absolute -right-24 -top-24 h-80 w-80 rounded-full bg-indigo-500 blur-3xl"
              aria-hidden="true"
            />
            <div
              className="absolute -bottom-28 -left-20 h-80 w-80 rounded-full bg-emerald-500/40 blur-3xl"
              aria-hidden="true"
            />
            <div className="relative">
              <p className="text-xs font-extrabold uppercase tracking-[0.22em] text-indigo-200">
                Mulai sekarang
              </p>
              <h2 className="font-display mx-auto mt-4 max-w-2xl text-3xl font-extrabold tracking-tight text-white sm:text-4xl md:text-5xl">
                Siap tinggalkan Google Forms?
              </h2>
              <p className="mx-auto mt-5 max-w-xl text-base leading-relaxed text-indigo-100 md:text-lg">
                Buat acara pertamamu dalam 3 menit. Gratis, tanpa biaya tersembunyi.
              </p>
              <a
                href={DASHBOARD_URL}
                target="_blank"
                rel="noopener noreferrer"
                data-testid="final-cta-button"
                className="group mt-9 inline-flex items-center gap-2 rounded-full bg-white px-8 py-4 text-sm font-extrabold text-indigo-700 shadow-xl transition-all hover:bg-indigo-50 md:text-base"
              >
                Mulai Buat Acara Gratis
                <ArrowRight className="h-4 w-4 transition-transform group-hover:translate-x-1" />
              </a>
            </div>
          </div>
        </Reveal>
      </div>
    </section>
  );
}
