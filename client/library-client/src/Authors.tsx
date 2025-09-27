import { useAtom } from "jotai";
import { useEffect, useRef } from "react";
import {
    authorsAtom,
    createAuthorAtom,
    deleteAuthorAtom,
    fetchAuthorsAtom,
    updateAuthorAtom
} from "./state/atoms";
import AuthorForm from "./components/authors/AuthorForm";
import AuthorTable from "./components/authors/AuthorTable";
import EmptyState from "./components/EmptyState";
import { CreateAuthorDto } from "./apiClient";

export default function Authors() {
    const [authors] = useAtom(authorsAtom);
    const [, fetchAuthors] = useAtom(fetchAuthorsAtom);
    const [, createAuthor] = useAtom(createAuthorAtom);
    const [, updateAuthor] = useAtom(updateAuthorAtom);
    const [, deleteAuthor] = useAtom(deleteAuthorAtom);

    const inputRef = useRef<HTMLInputElement | null>(null);

    useEffect(() => {
        fetchAuthors();
    }, [fetchAuthors]);

    const handleCreate = async (name: string) => {
        try {
            await createAuthor(new CreateAuthorDto({ name }));
        } catch (err) {
            console.error(err);
            alert("Kunne ikke oprette forfatter. Proev igen.");
        }
    };

    const handleUpdate = async (id: string, name: string) => {
        try {
            await updateAuthor({ id, dto: new CreateAuthorDto({ name }) });
        } catch (err) {
            console.error(err);
            alert("Kunne ikke opdatere forfatteren.");
        }
    };

    const handleDelete = async (id: string) => {
        try {
            await deleteAuthor(id);
        } catch (err) {
            console.error(err);
            alert("Kunne ikke slette forfatteren.");
        }
    };

    return (
        <div className="space-y-6">
            <AuthorForm onCreate={handleCreate} inputRef={inputRef} />

            {authors.length === 0 ? (
                <EmptyState
                    title="Ingen forfattere endnu."
                    description="Tilfoej din foerste forfatter her:"
                    actionLabel="Opret forfatter"
                    onAction={() => inputRef.current?.focus()}
                />
            ) : (
                <AuthorTable authors={authors} onUpdate={handleUpdate} onDelete={handleDelete} />
            )}
        </div>
    );
}