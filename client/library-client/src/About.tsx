export default function About() {
    return (
        <div className="min-h-screen bg-base-100 text-base-content flex flex-col items-center p-8">
            <img
                src="/src/assets/profile.webp"
                alt="Portræt"
                className="max-w-xs rounded-2xl shadow-2xl border-4 border-primary"
            />

            <h1 className="text-3xl font-serif font-bold mt-6">Det Hemmelige Arkiv</h1>

            <p className="max-w-2xl text-lg leading-relaxed text-center mb-4">
                Velkommen til mit lille skjulte hjørne af Candlekeep. Her finder du bøger
                fyldt med både viden og hemmeligheder, gemt for de få, som tør søge dem.
            </p>

            <p className="max-w-2xl text-lg leading-relaxed text-center mb-4">
                Som besøgende kan du udforske samlingerne, søge i kataloget eller
                kontakte personalet for vejledning. Alle henvendelser håndteres af
                ærkemagerens skriftkloge og vogtere, som hjælper dig videre i
                bibliotekets verden af viden.
            </p>

            <div className="mt-6 p-4 bg-base-200 rounded-xl shadow-md max-w-md text-center">
                <h2 className="text-xl font-semibold">Praktisk information</h2>
                <ul className="mt-2 space-y-2 text-sm">
                    <li>📖 Adgang: Åben for alle med en bog som adgangsbetaling</li>
                    <li>🕰 Åbningstider: Solopgang – solnedgang</li>
                    <li>📍 Placering: De vestlige klipper ved Sværdkysten</li>
                    <li>📬 Kontakt: skriv til Arcananet@candlekeep.faerûn</li>
                </ul>
            </div>
        </div>
    );
}
