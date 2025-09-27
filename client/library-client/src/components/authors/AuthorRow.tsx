import { useEffect, useState } from "react";
import { AuthorDto } from "../../apiClient";
import EditActions from "../EditActions";

type AuthorRowProps = {
    author: AuthorDto;
    onUpdate: (id: string, name: string) => Promise<void> | void;
    onDelete: (id: string) => Promise<void> | void;
};

export default function AuthorRow({ author, onUpdate, onDelete }: AuthorRowProps) {
    const [isEditing, setIsEditing] = useState(false);
    const [editingName, setEditingName] = useState(author.name ?? "");

    useEffect(() => {
        setEditingName(author.name ?? "");
    }, [author.id, author.name]);

    if (!author.id) {
        return null;
    }

    const handleSave = async () => {
        const trimmed = editingName.trim();
        if (!trimmed) {
            alert("Navn skal udfyldes.");
            return;
        }

        await onUpdate(author.id!, trimmed);
        setIsEditing(false);
    };

    const handleCancel = () => {
        setEditingName(author.name ?? "");
        setIsEditing(false);
    };

    const handleDelete = async () => {
        await onDelete(author.id!);
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
                    author.name
                )}
            </td>
            <td className="text-right">
                <EditActions
                    isEditing={isEditing}
                    onEdit={() => setIsEditing(true)}
                    onSave={handleSave}
                    onCancel={handleCancel}
                    onDelete={handleDelete}
                />
            </td>
        </tr>
    );
}