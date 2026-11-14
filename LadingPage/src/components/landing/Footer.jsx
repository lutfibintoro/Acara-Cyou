import Logo from "@/components/landing/Logo";
import { scrollToHash } from "@/components/landing/Navbar";
import { FaInstagram, FaTwitter, FaLinkedin } from 'react-icons/fa';

export const DASHBOARD_URL = import.meta.env.VITE_DASHBOARD_URL;


const NAV = [
  { label: "Masalah", href: "#masalah", id: "footer-link-problem" },
  { label: "Fitur", href: "#fitur", id: "footer-link-features" },
  { label: "Cara Kerja", href: "#cara-kerja", id: "footer-link-how-it-works" },
  { label: "Testimoni", href: "#testimoni", id: "footer-link-testimonials" },
  { label: "FAQ", href: "#faq", id: "footer-link-faq" },
];

export default function Footer() {
  return (
    <footer className="border-t border-slate-200/80 bg-white" data-testid="footer-container">
      <div className="mx-auto max-w-6xl px-6 py-14 md:px-8">
        <div className="grid gap-10 md:grid-cols-[1.4fr_1fr_1fr]">
          <div>
            <Logo />
            <p className="mt-4 max-w-xs text-sm leading-relaxed text-slate-500">
              Pendaftaran, pembayaran &amp; tiket otomatis untuk panitia kecil —
              himpunan, komunitas, dan acara kampus.
            </p>
            <div className="mt-6 flex gap-3">
              {[
                { icon: FaInstagram, label: "Instagram", id: "footer-social-instagram" },
                { icon: FaTwitter, label: "Twitter", id: "footer-social-twitter" },
                { icon: FaLinkedin, label: "LinkedIn", id: "footer-social-linkedin" },
              ].map((s) => (
                <a
                  key={s.label}
                  href="#"
                  aria-label={s.label}
                  data-testid={s.id}
                  className="flex h-9 w-9 items-center justify-center rounded-full border border-slate-200 text-slate-500 transition-colors hover:border-indigo-300 hover:text-indigo-600"
                >
                  <s.icon className="h-4 w-4" />
                </a>
              ))}
            </div>
          </div>

          <div>
            <h4 className="text-sm font-extrabold text-slate-900">Navigasi</h4>
            <ul className="mt-4 space-y-3">
              {NAV.map((l) => (
                <li key={l.href}>
                  <a
                    href={l.href}
                    data-testid={l.id}
                    onClick={(e) => scrollToHash(e, l.href)}
                    className="text-sm font-medium text-slate-500 transition-colors hover:text-indigo-600"
                  >
                    {l.label}
                  </a>
                </li>
              ))}
            </ul>
          </div>

          <div>
            <h4 className="text-sm font-extrabold text-slate-900">Mulai</h4>
            <ul className="mt-4 space-y-3">
              <li>
                <a
                  href={DASHBOARD_URL}
                  target="_blank"
                  rel="noopener noreferrer"
                  data-testid="footer-link-dashboard"
                  className="text-sm font-medium text-slate-500 transition-colors hover:text-indigo-600"
                >
                  Buat Acara Gratis
                </a>
              </li>
              <li>
                <a
                  href={DASHBOARD_URL}
                  target="_blank"
                  rel="noopener noreferrer"
                  data-testid="footer-link-login"
                  className="text-sm font-medium text-slate-500 transition-colors hover:text-indigo-600"
                >
                  Masuk Dashboard
                </a>
              </li>
              <li>
                <a
                  href="#faq"
                  data-testid="footer-link-faq-jump"
                  onClick={(e) => scrollToHash(e, "#faq")}
                  className="text-sm font-medium text-slate-500 transition-colors hover:text-indigo-600"
                >
                  Lihat FAQ
                </a>
              </li>
            </ul>
          </div>
        </div>

        <div className="mt-12 flex flex-col items-start justify-between gap-3 border-t border-slate-100 pt-6 text-xs text-slate-500 md:flex-row md:items-center">
          <p data-testid="footer-copyright">© 2025 Acara.Cyou — Semua hak dilindungi.</p>
          <p>bantuan@acara.cyou</p>
        </div>
      </div>
    </footer>
  );
}
