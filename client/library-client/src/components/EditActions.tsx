type EditActionsProps = {
    isEditing: boolean;
    onEdit: () => void;
    onSave: () => void;
    onCancel: () => void;
    onDelete: () => void;
};

export default function EditActions({ isEditing, onEdit, onSave, onCancel, onDelete }: EditActionsProps) {
    return isEditing ? (
        <div className="flex justify-end gap-2">
            <button type="button" className="btn btn-success btn-sm" onClick={onSave}>
                Gem
            </button>
            <button type="button" className="btn btn-ghost btn-sm" onClick={onCancel}>
                Annuller
            </button>
        </div>
    ) : (
        <div className="flex justify-end gap-2">
            <button type="button" className="btn btn-outline btn-sm" onClick={onEdit}>
                Rediger
            </button>
            <button type="button" className="btn btn-error btn-sm" onClick={onDelete}>
                Slet
            </button>
        </div>
    );
}