import ReactDOM from "react-dom/client";
import { BrowserRouter, Routes, Route } from "react-router-dom";

import App from "./App";
import Books from "./Books";
import Authors from "./Authors";
import "./index.css";

import { DevTools } from "jotai-devtools";
import "jotai-devtools/styles.css";

ReactDOM.createRoot(document.getElementById("root")!).render(
    <BrowserRouter>
        <Routes>
            <Route path="/" element={<App />} />
            <Route path="/books" element={<Books />} />
            <Route path="/authors" element={<Authors />} />
        </Routes>
        <DevTools />
    </BrowserRouter>
);
