import { Outlet, Link } from "react-router-dom";
import ThemeToggle from "./components/ThemeToggle";

export default function App() {
    return (
        <div className="min-h-screen bg-base-100 text-base-content">
            {/* Navbar */}
            <div className="navbar bg-base-200 shadow-md">
                <div className="flex-1">
                    <Link to="/" className="btn btn-ghost text-xl font-serif">
                        Candlekeep Library
                    </Link>
                </div>
                <div className="flex-none gap-2">
                    <Link to="/books" className="btn btn-primary">Books</Link>
                    <Link to="/authors" className="btn btn-secondary">Authors</Link>
                    <Link to="/about" className="btn btn-accent">About</Link>
                    <ThemeToggle />
                </div>
            </div>

            {/* Routed content */}
            <main className="container mx-auto p-6">
                <Outlet />
            </main>
        </div>
    );
}

