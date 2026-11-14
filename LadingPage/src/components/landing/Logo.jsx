export function LogoMark({ className = "h-9 w-9" }) {
  return (
    <svg viewBox="0 0 36 36" className={className} fill="none" xmlns="http://www.w3.org/2000/svg" aria-hidden="true">
      <rect width="36" height="36" rx="10" fill="#4F46E5" />
      <path d="M10 24L18 10L26 24H21.5L18 17.5L14.5 24H10Z" fill="white" />
      <circle cx="18" cy="21.5" r="2.2" fill="#10B981" />
    </svg>
  );
}

export default function Logo() {
  return (
    <span className="flex items-center gap-2.5">
      <LogoMark />
      <span className="font-display text-xl font-extrabold tracking-tight text-slate-900">
        Acara<span className="text-indigo-600">.Cyou</span>
      </span>
    </span>
  );
}
