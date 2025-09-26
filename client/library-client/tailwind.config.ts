// @ts-ignore
import type { Config } from "tailwindcss";

import daisyui from "daisyui";

// --- Candlekeep (light) ---
const candlekeep = {
    "primary": "#166534",
    "primary-content": "#FFFFFF",
    "secondary": "#B45309",
    "secondary-content": "#FFFFFF",
    "accent": "#155E75",
    "accent-content": "#FFFFFF",
    "neutral": "#2F2A25",
    "neutral-content": "#EEE9DC",
    "base-100": "#FAF7F0",
    "base-200": "#EFE9DD",
    "base-300": "#E3DBC8",
    "base-content": "#1F2937",
    "info": "#1D4ED8",
    "info-content": "#FFFFFF",
    "success": "#15803D",
    "success-content": "#FFFFFF",
    "warning": "#A16207",
    "warning-content": "#FFFFFF",
    "error": "#B91C1C",
    "error-content": "#FFFFFF",
};

// --- Candlekeep Night (dark) ---
const candlekeepNight = {
    "primary": "#34D399",
    "primary-content": "#0B1020",
    "secondary": "#F59E0B",
    "secondary-content": "#0B1020",
    "accent": "#22D3EE",
    "accent-content": "#0B1020",
    "neutral": "#1F2937",
    "neutral-content": "#E5E7EB",
    "base-100": "#0B1020",
    "base-200": "#0A0C19",
    "base-300": "#070B14",
    "base-content": "#EDEDF4",
    "info": "#60A5FA",
    "info-content": "#0B1020",
    "success": "#34D399",
    "success-content": "#0B1020",
    "warning": "#F59E0B",
    "warning-content": "#0B1020",
    "error": "#F87171",
    "error-content": "#0B1020",
};

export default {
    content: [
        "./index.html",
        "./src/**/*.{ts,tsx,js,jsx}",
    ],
    theme: {
        extend: {},
    },
    plugins: [daisyui],
    daisyui: {
        themes: [
            { candlekeep },
            { "candlekeep-night": candlekeepNight },
        ],
    },
} satisfies Config;
