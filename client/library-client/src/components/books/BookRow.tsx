import { useEffect, useState } from "react";
import { BookDto, GenreDto } from "../../apiClient";
import EditActions from "../EditActions";

type BookRowProps = {
    book: BookDto;
    genres: GenreDto[];
    onUpdate: (id: string, dto: { title: string; pages: number; genreId?: string }) => Promise<void> | void;
    onDelete: (id: string) => Promise<void> | void;
};

export default function BookRow({ book, genres, onUpdate, onDelete }: BookRowProps) {
    const [isEditing, setIsEditing] = useState(false);
    const [title, setTitle] = useState(book.title ?? "");
    const [pages, setPages] = useState(String(book.pages ?? 1));
    const [genreId, setGenreId] = useState(book.genreId ?? "");

    useEffect(() => {
        setTitle(book.title ?? "");
        setPages(String(book.pages ?? 1));
        setGenreId(book.genreId ?? "");
    }, [book.id, book.title, book.pages, book.genreId]);

    if (!book.id) {
        return null;
    }

    const save = async () => {
        const trimmedTitle = title.trim();
        const parsedPages = Number(pages);

        if (!trimmedTitle) {
            alert("Titel skal udfyldes.");
            return;
        }

        if (!Number.isFinite(parsedPages) || parsedPages <= 0) {
            alert("Antal sider skal vaere et positivt tal.");
            return;
        }

        await onUpdate(book.id!, {
            title: trimmedTitle,
            pages: parsedPages,
            genreId: genreId || undefined
        });
        setIsEditing(false);
    };

    const cancel = () => {
        setTitle(book.title ?? "");
        setPages(String(book.pages ?? 1));
        setGenreId(book.genreId ?? "");
        setIsEditing(false);
    };

    const remove = async () => {
        await onDelete(book.id!);
    };

    const genreName = genres.find((g) => g.id === (book.genreId ?? undefined))?.name ?? "-";

    return (
        <tr>
            <td>
                {isEditing ? (
                    <input
                        className="input input-bordered input-sm w-full"
                        value={title}
                        onChange={(e) => setTitle(e.target.value)}
                    />
                ) : (
                    book.title
                )}
            </td>
            <td className="w-32">
                {isEditing ? (
                    <input
                        className="input input-bordered input-sm w-full"
                        type="number"
                        min="1"
                        value={pages}
                        onChange={(e) => setPages(e.target.value)}
                    />
                ) : (
                    book.pages
                )}
            </td>
            <td className="w-48">
                {isEditing ? (
                    <select
                        className="select select-bordered select-sm w-full"
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
                ) : (
                    genreName
                )}
            </td>
            <td className="text-right">
                <EditActions
                    isEditing={isEditing}
                    onEdit={() => setIsEditing(true)}
                    onSave={save}
                    onCancel={cancel}
                    onDelete={remove}
                />
            </td>
        </tr>
    );
}