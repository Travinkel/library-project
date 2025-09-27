import { Outlet, Link } from "react-router-dom";
import ThemeToggle from "./components/ThemeToggle";

export default function App() {
    return (
        <div className="min-h-screen bg-base-100 text-base-content">
            <nav className="navbar bg-base-200 shadow">
                <div className="flex-1">
                    <Link to="/" className="btn btn-ghost text-xl font-serif">
                        Det Hemmelige Arkiv
                    </Link>
                    
                </div>
                <div className="flex-none space-x-2 pr-4">
                    <Link to="/books" className="btn btn-sm btn-primary">Books</Link>
                    <Link to="/authors" className="btn btn-sm btn-secondary">Authors</Link>
                    <Link to="/genres" className="btn btn-sm btn-accent">Genres</Link>
                    <Link to="/about" className="btn btn-sm">About</Link>
                    <ThemeToggle />
                </div>
            </nav>

            {/* Routed content */}
            <main className="container mx-auto p-6">
                <Outlet />
            </main>
        </div>
    );
}
