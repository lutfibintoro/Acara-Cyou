import { motion } from "framer-motion";

export const EASE = [0.16, 1, 0.3, 1];

export function Reveal({ children, delay = 0, y = 28, className = "", ...rest }) {
  return (
    <motion.div
      className={className}
      initial={{ opacity: 0, y }}
      whileInView={{ opacity: 1, y: 0 }}
      viewport={{ once: true, margin: "-60px" }}
      transition={{ duration: 0.7, delay, ease: EASE }}
      {...rest}
    >
      {children}
    </motion.div>
  );
}

export function Eyebrow({ children }) {
  return (
    <p className="text-xs font-extrabold uppercase tracking-[0.22em] text-indigo-600">
      {children}
    </p>
  );
}

export default Reveal;
