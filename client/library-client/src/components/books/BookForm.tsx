import { useState, type FormEvent, type RefObject } from "react";
import { GenreDto } from "../../apiClient";

type BookFormProps = {
    genres: GenreDto[];
    onCreate: (dto: { title: string; pages: number; genreId?: string }) => Promise<void> | void;
    titleInputRef: RefObject<HTMLInputElement | null>;
};

export default function BookForm({ genres, onCreate, titleInputRef }: BookFormProps) {
    const [title, setTitle] = useState("");
    const [pages, setPages] = useState("200");
    const [genreId, setGenreId] = useState("");

    const submitNewBook = async (evt: FormEvent) => {
        evt.preventDefault();
        const trimmedTitle = title.trim();
        const parsedPages = Number(pages);

        if (!trimmedTitle) {
            alert("Titel skal udfyldes.");
            titleInputRef.current?.focus();
            return;
        }

        if (!Number.isFinite(parsedPages) || parsedPages <= 0) {
            alert("Antal sider skal vaere et positivt tal.");
            return;
        }

        await onCreate({
            title: trimmedTitle,
            pages: parsedPages,
            genreId: genreId || undefined
        });

        setTitle("");
        setPages("200");
        setGenreId("");
    };

    return (
        <form className="card bg-base-200 shadow" onSubmit={submitNewBook}>
            <div className="card-body space-y-4">
                <h2 className="card-title">Tilfoej bog</h2>
                <div className="grid gap-3 md:grid-cols-3">
                    <input
                        ref={titleInputRef}
                        className="input input-bordered"
                        placeholder="Titel"
                        value={title}
                        onChange={(e) => setTitle(e.target.value)}
                    />
                    <input
                        className="input input-bordered"
                        type="number"
                        min="1"
                        value={pages}
                        onChange={(e) => setPages(e.target.value)}
                    />
                    <select
                        className="select select-bordered"
                        value={genreId}
                        onChange={(e) => setGenreId(e.target.value)}
                    >
                        <option value="">Ingen genre</option>
                        {genres.map((g) => (
                            <option key={g.id} value={g.id ?? ""}>
                                {g.name}
                            </option>
                        ))}
                    </select>
                </div>
                <div>
                    <button className="btn btn-primary" type="submit">
                        Tilfoej
                    </button>
                </div>
            </div>
        </form>
    );
}