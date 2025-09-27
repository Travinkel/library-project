import { useEffect, useState } from "react";
import { GenreDto } from "../../apiClient";
import EditActions from "../EditActions";

type GenreRowProps = {
    genre: GenreDto;
    onUpdate: (id: string, name: string) => Promise<void> | void;
    onDelete: (id: string) => Promise<void> | void;
};

export default function GenreRow({ genre, onUpdate, onDelete }: GenreRowProps) {
    const [isEditing, setIsEditing] = useState(false);
    const [editingName, setEditingName] = useState(genre.name ?? "");

    useEffect(() => {
        setEditingName(genre.name ?? "");
    }, [genre.id, genre.name]);

    if (!genre.id) {
        return null;
    }

    const save = async () => {
        const trimmed = editingName.trim();
        if (!trimmed) {
            alert("Navn skal udfyldes.");
            return;
        }

        await onUpdate(genre.id!, trimmed);
        setIsEditing(false);
    };

    const cancel = () => {
        setEditingName(genre.name ?? "");
        setIsEditing(false);
    };

    const remove = async () => {
        await onDelete(genre.id!);
    };

    return (
        <tr>
            <td>
                {isEditing ? (
                    <input
                        className="input input-bordered input-sm w-full"
                        value={editingName}
                        onChange={(e) => setEditingName(e.target.value)}
                    />
                ) : (
                    genre.name
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