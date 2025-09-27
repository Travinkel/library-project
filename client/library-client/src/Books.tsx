import { useAtom } from "jotai";
import { useEffect, useRef } from "react";
import {
    booksAtom,
    createBookAtom,
    deleteBookAtom,
    fetchBooksAtom,
    fetchGenresAtom,
    genresAtom,
    updateBookAtom
} from "./state/atoms";
import BookForm from "./components/books/BookForm";
import BookTable from "./components/books/BookTable";
import EmptyState from "./components/EmptyState";
import { CreateBookDto } from "./apiClient";

export default function Books() {
    const [books] = useAtom(booksAtom);
    const [genres] = useAtom(genresAtom);
    const [, fetchBooks] = useAtom(fetchBooksAtom);
    const [, fetchGenres] = useAtom(fetchGenresAtom);
    const [, createBook] = useAtom(createBookAtom);
    const [, updateBook] = useAtom(updateBookAtom);
    const [, deleteBook] = useAtom(deleteBookAtom);

    const titleInputRef = useRef<HTMLInputElement | null>(null);

    useEffect(() => {
        fetchBooks();
        fetchGenres();
    }, [fetchBooks, fetchGenres]);

    const handleCreate = async ({ title, pages, genreId }: { title: string; pages: number; genreId?: string }) => {
        try {
            await createBook(new CreateBookDto({ title, pages, genreId }));
        } catch (err) {
            console.error(err);
            alert("Kunne ikke oprette bogen.");
        }
    };

    const handleUpdate = async (id: string, dto: { title: string; pages: number; genreId?: string }) => {
        try {
            await updateBook({ id, dto: new CreateBookDto(dto) });
        } catch (err) {
            console.error(err);
            alert("Kunne ikke opdatere bogen.");
        }
    };

    const handleDelete = async (id: string) => {
        try {
            await deleteBook(id);
        } catch (err) {
            console.error(err);
            alert("Kunne ikke slette bogen.");
        }
    };

    return (
        <div className="space-y-6">
            <BookForm genres={genres} onCreate={handleCreate} titleInputRef={titleInputRef} />

            {books.length === 0 ? (
                <EmptyState
                    title="Ingen boeger endnu."
                    description="Tilfoej din foerste bog her:"
                    actionLabel="Opret bog"
                    onAction={() => titleInputRef.current?.focus()}
                />
            ) : (
                <BookTable books={books} genres={genres} onUpdate={handleUpdate} onDelete={handleDelete} />
            )}
        </div>
    );
}