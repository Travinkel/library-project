import { useState, useEffect } from "react";

const LIGHT_THEME = "candlekeep";
const DARK_THEME = "candlekeep-night";

function applyTheme(theme: string) {
    document.documentElement.setAttribute("data-theme", theme);
    localStorage.setItem("theme", theme);
}

export default function ThemeToggle() {
    const [theme, setTheme] = useState(LIGHT_THEME);

    // Load saved theme from localStorage
    useEffect(() => {
        const saved = localStorage.getItem("theme") as string | null;
        const initial = saved || LIGHT_THEME;
        setTheme(initial);
        applyTheme(initial);
    }, []);

    // Toggle between light/dark
    const toggle = () => {
        const newTheme = theme === LIGHT_THEME ? DARK_THEME : LIGHT_THEME;
        setTheme(newTheme);
        applyTheme(newTheme);
    };

    return (
        <button className="btn btn-ghost" onClick={toggle}>
            {theme === LIGHT_THEME ? "🌙 Night" : "☀️ Day"}
        </button>
    );
}
