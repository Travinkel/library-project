

export default function Authors() {
    return (
        <div className="min-h-screen bg-base-100 text-base-content">
            <div className="hero min-h-[60vh] bg-base-200">
                <div className="hero-content flex-col lg:flex-row gap-8">
                    <img
                        src="/assets/profile.webp"
                        alt="Archmage of Candlekeep"
                        className="max-w-sm rounded-2xl shadow-2xl border-4 border-primary"
                    />
                    <div>
                        <h1 className="text-4xl font-serif font-bold">The Archmage of Candlekeep</h1>
                        <p className="py-4 text-lg leading-relaxed">
                            Keeper of secrets, master of lore, and guardian of the library’s
                            most forbidden tomes. His glowing eyes speak of wards woven into
                            the very stone of Candlekeep itself. Those who seek knowledge
                            must first earn his trust.
                        </p>
                        <button className="btn btn-primary">Browse Writings</button>
                    </div>
                </div>
            </div>
        </div>
    );
}