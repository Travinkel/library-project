import ReactDOM from "react-dom/client";
import { BrowserRouter, Routes, Route } from "react-router-dom";

import App from "./App";
import Books from "./Books";
import Authors from "./Authors";
import About from "./About";
import "./index.css";

import { DevTools } from "jotai-devtools";
import "jotai-devtools/styles.css";

const LIGHT_THEME = "candlekeep";
const DARK_THEME = "candlekeep-night";

if (typeof window !== "undefined") {
    const prefersDark = window.matchMedia("(prefers-color-scheme: dark)");
    const applyTheme = (isDark: boolean) => {
        document.documentElement.dataset.theme = isDark ? DARK_THEME : LIGHT_THEME;
    };

    applyTheme(prefersDark.matches);

    const listener = (event: MediaQueryListEvent) => {
        applyTheme(event.matches);
    };

    prefersDark.addEventListener("change", listener);

    if (import.meta.hot) {
        import.meta.hot.dispose(() => {
            prefersDark.removeEventListener("change", listener);
        });
    }
}

ReactDOM.createRoot(document.getElementById("root")!).render(
    <BrowserRouter>
        <Routes>
            <Route path="/" element={<App />} />
            <Route path="/books" element={<Books />} />
            <Route path="/authors" element={<Authors />} />

            <Route path="/about" element={<About />} />
        </Routes>
        <DevTools />
    </BrowserRouter>
);
