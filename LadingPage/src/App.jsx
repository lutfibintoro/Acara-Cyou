import React, { useEffect } from "react";
import Lenis from "lenis";
import Navbar from "@/components/landing/Navbar";
import Hero from "@/components/landing/Hero";
import MarqueeStrip from "@/components/landing/MarqueeStrip";
import ProblemSolution from "@/components/landing/ProblemSolution";
import FeaturesBento from "@/components/landing/FeaturesBento";
import HowItWorks from "@/components/landing/HowItWorks";
import Testimonials from "@/components/landing/Testimonials";
import Faq from "@/components/landing/Faq";
import FinalCta from "@/components/landing/FinalCta";
import Footer from "@/components/landing/Footer";

class ErrorBoundary extends React.Component {
  constructor(props) {
    super(props);
    this.state = { hasError: false };
  }
  static getDerivedStateFromError() {
    return { hasError: true };
  }
  render() {
    if (this.state.hasError) {
      return (
        <div className="flex min-h-screen items-center justify-center bg-[#F8F9FB] text-slate-600">
          Terjadi kesalahan - muat ulang halaman.
        </div>
      );
    }
    return this.props.children;
  }
}

function App() {
  useEffect(() => {
    const lenis = new Lenis({ duration: 1.15, smoothWheel: true });
    window.__lenis = lenis;
    let raf;
    const loop = (t) => {
      lenis.raf(t);
      raf = requestAnimationFrame(loop);
    };
    raf = requestAnimationFrame(loop);
    return () => {
      cancelAnimationFrame(raf);
      lenis.destroy();
      window.__lenis = null;
    };
  }, []);

  return (
    <ErrorBoundary>
      <div className="relative min-h-screen overflow-x-clip bg-[#F8F9FB] font-sans text-slate-900 antialiased">
        <div className="grain" aria-hidden="true" />
        <Navbar />
        <main>
          <Hero />
          <MarqueeStrip />
          <ProblemSolution />
          <FeaturesBento />
          <HowItWorks />
          <Testimonials />
          <Faq />
          <FinalCta />
        </main>
        <Footer />
      </div>
    </ErrorBoundary>
  );
}

export default App;