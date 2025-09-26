export default function App() {
    return (
        <div className="min-h-screen bg-base-100 text-base-content p-6">
            <h1 className="text-4xl font-bold">Candlekeep Library</h1>
            <nav className="mt-4 space-x-4">
                <a className="btn btn-ghost" href="/books">Books</a>
                <a className="btn btn-ghost" href="/authors">Authors</a>
                <a className="btn btn-ghost" href="/about">About</a>
            </nav>
        </div>
    );
}

