import { Link } from "react-router-dom";

export default function Home() {
    return (
        <div className="flex flex-col items-center justify-center p-6">
            <h1 className="text-4xl font-serif font-bold text-center mb-4">
                Velkommen til <span className="text-primary">Det Hemmelige Arkiv</span>
            </h1>
            <p className="max-w-2xl text-center text-base-content/80 mb-10">
                Et skjult hjørne af Candlekeep, hvor bøger, forfattere og genrer samles
                i et katalog. Brug navigationen herunder til at udforske samlingerne.
            </p>

            <div className="grid gap-6 md:grid-cols-3 w-full max-w-4xl">
                <Link to="/books" className="card bg-base-200 shadow-md hover:shadow-lg transition">
                    <div className="card-body items-center text-center">
                        <h2 className="card-title">📚 Bøger</h2>
                        <p>Gennemse arkivets bøger</p>
                    </div>
                </Link>

                <Link to="/authors" className="card bg-base-200 shadow-md hover:shadow-lg transition">
                    <div className="card-body items-center text-center">
                        <h2 className="card-title">✒️ Forfattere</h2>
                        <p>Se hvem der har skrevet værkerne</p>
                    </div>
                </Link>

                <Link to="/genres" className="card bg-base-200 shadow-md hover:shadow-lg transition">
                    <div className="card-body items-center text-center">
                        <h2 className="card-title">🏷️ Genrer</h2>
                        <p>Udforsk klassifikationer og temaer</p>
                    </div>
                </Link>
            </div>
        </div>
    );
}
