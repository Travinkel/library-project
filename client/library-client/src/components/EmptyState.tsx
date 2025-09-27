import type { ReactNode } from "react";

type EmptyStateProps = {
    title: string;
    description: string;
    actionLabel: string;
    onAction: () => void;
    icon?: ReactNode;
};

export default function EmptyState({ title, description, actionLabel, onAction, icon }: EmptyStateProps) {
    return (
        <div className="card bg-base-200 shadow">
            <div className="card-body items-center text-center space-y-2">
                {icon}
                <h2 className="card-title">{title}</h2>
                <p>{description}</p>
                <button type="button" className="btn btn-primary" onClick={onAction}>
                    {actionLabel}
                </button>
            </div>
        </div>
    );
}