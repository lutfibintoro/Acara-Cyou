import { useEffect, useState } from "react";
import { AnimatePresence, motion } from "framer-motion";
import { ArrowRight, Menu, X } from "lucide-react";
import Logo from "@/components/landing/Logo";

export const DASHBOARD_URL = import.meta.env.VITE_DASHBOARD_URL;

const LINKS = [
  { label: "Masalah", href: "#masalah", id: "nav-link-problem" },
  { label: "Fitur", href: "#fitur", id: "nav-link-features" },
  { label: "Cara Kerja", href: "#cara-kerja", id: "nav-link-how-it-works" },
  { label: "Testimoni", href: "#testimoni", id: "nav-link-testimonials" },
  { label: "FAQ", href: "#faq", id: "nav-link-faq" },
];

export function scrollToHash(e, hash) {
  e.preventDefault();
  const el = document.querySelector(hash);
  if (!el) return;
  if (window.__lenis) window.__lenis.scrollTo(el, { offset: -90 });
  else el.scrollIntoView({ behavior: "smooth" });
}

export default function Navbar() {
  const [scrolled, setScrolled] = useState(false);
  const [open, setOpen] = useState(false);

  useEffect(() => {
    const onScroll = () => setScrolled(window.scrollY > 12);
    onScroll();
    window.addEventListener("scroll", onScroll, { passive: true });
    return () => window.removeEventListener("scroll", onScroll);
  }, []);

  return (
    <header className="fixed inset-x-0 top-0 z-50 px-4 pt-3" data-testid="navbar-container">
      <div
        className={`mx-auto flex max-w-6xl items-center justify-between rounded-2xl px-4 py-3 transition-all duration-300 md:px-6 ${
          scrolled || open
            ? "glass border border-slate-200/80 shadow-lg shadow-slate-900/5"
            : "border border-transparent"
        }`}
      >
        <a
          href="#"
          data-testid="nav-logo"
          onClick={(e) => {
            e.preventDefault();
            if (window.__lenis) window.__lenis.scrollTo(0);
            else window.scrollTo({ top: 0, behavior: "smooth" });
            setOpen(false);
          }}
        >
          <Logo />
        </a>

        <nav className="hidden items-center gap-1 lg:flex">
          {LINKS.map((l) => (
            <a
              key={l.href}
              href={l.href}
              data-testid={l.id}
              onClick={(e) => scrollToHash(e, l.href)}
              className="rounded-full px-4 py-2 text-sm font-semibold text-slate-600 transition-colors hover:bg-brand-soft hover:text-indigo-700"
            >
              {l.label}
            </a>
          ))}
        </nav>

        <div className="hidden lg:block">
          <a
            href={DASHBOARD_URL}
            target="_blank"
            rel="noopener noreferrer"
            data-testid="nav-cta-dashboard"
            className="group inline-flex items-center gap-2 rounded-full bg-indigo-600 px-5 py-2.5 text-sm font-bold text-white shadow-lg shadow-indigo-600/25 transition-all hover:bg-indigo-700"
          >
            Mulai Gratis
            <ArrowRight className="h-4 w-4 transition-transform group-hover:translate-x-0.5" />
          </a>
        </div>

        <button
          className="flex h-10 w-10 items-center justify-center rounded-xl text-slate-700 transition-colors hover:bg-slate-100 lg:hidden"
          onClick={() => setOpen(!open)}
          data-testid="nav-mobile-toggle"
          aria-label={open ? "Tutup menu" : "Buka menu"}
        >
          {open ? <X className="h-5 w-5" /> : <Menu className="h-5 w-5" />}
        </button>
      </div>

      <AnimatePresence>
        {open && (
          <motion.div
            initial={{ opacity: 0, y: -8 }}
            animate={{ opacity: 1, y: 0 }}
            exit={{ opacity: 0, y: -8 }}
            transition={{ duration: 0.25 }}
            className="glass mx-auto mt-2 max-w-6xl rounded-2xl border border-slate-200/80 p-4 shadow-xl lg:hidden"
          >
            {LINKS.map((l) => (
              <a
                key={l.href}
                href={l.href}
                data-testid={`${l.id}-mobile`}
                onClick={(e) => {
                  scrollToHash(e, l.href);
                  setOpen(false);
                }}
                className="block rounded-xl px-4 py-3 font-semibold text-slate-700 transition-colors hover:bg-brand-soft hover:text-indigo-700"
              >
                {l.label}
              </a>
            ))}
            <a
              href={DASHBOARD_URL}
              target="_blank"
              rel="noopener noreferrer"
              data-testid="nav-cta-dashboard-mobile"
              className="mt-2 flex items-center justify-center gap-2 rounded-xl bg-indigo-600 px-5 py-3 font-bold text-white transition-colors hover:bg-indigo-700"
            >
              Mulai Gratis <ArrowRight className="h-4 w-4" />
            </a>
          </motion.div>
        )}
      </AnimatePresence>
    </header>
  );
}
