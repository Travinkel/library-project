import { useAtom } from "jotai";
import { useEffect, useRef } from "react";
import {
    createGenreAtom,
    deleteGenreAtom,
    fetchGenresAtom,
    genresAtom,
    updateGenreAtom
} from "./state/atoms";
import GenreForm from "./components/genres/GenreForm";
import GenreTable from "./components/genres/GenreTable";
import EmptyState from "./components/EmptyState";
import { CreateGenreDto } from "./apiClient";

export default function Genres() {
    const [genres] = useAtom(genresAtom);
    const [, fetchGenres] = useAtom(fetchGenresAtom);
    const [, createGenre] = useAtom(createGenreAtom);
    const [, updateGenre] = useAtom(updateGenreAtom);
    const [, deleteGenre] = useAtom(deleteGenreAtom);

    const inputRef = useRef<HTMLInputElement | null>(null);

    useEffect(() => {
        fetchGenres();
    }, [fetchGenres]);

    const handleCreate = async (name: string) => {
        try {
            await createGenre(new CreateGenreDto({ name }));
        } catch (err) {
            console.error(err);
            alert("Kunne ikke oprette genren.");
        }
    };

    const handleUpdate = async (id: string, name: string) => {
        try {
            await updateGenre({ id, dto: new CreateGenreDto({ name }) });
        } catch (err) {
            console.error(err);
            alert("Kunne ikke opdatere genren.");
        }
    };

    const handleDelete = async (id: string) => {
        try {
            await deleteGenre(id);
        } catch (err) {
            console.error(err);
            alert("Kunne ikke slette genren.");
        }
    };

    return (
        <div className="space-y-6">
            <GenreForm onCreate={handleCreate} inputRef={inputRef} />

            {genres.length === 0 ? (
                <EmptyState
                    title="Ingen genrer endnu."
                    description="Tilfoej din foerste genre her:"
                    actionLabel="Opret genre"
                    onAction={() => inputRef.current?.focus()}
                />
            ) : (
                <GenreTable genres={genres} onUpdate={handleUpdate} onDelete={handleDelete} />
            )}
        </div>
    );
}