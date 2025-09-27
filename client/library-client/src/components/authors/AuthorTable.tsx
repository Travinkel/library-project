import { AuthorDto } from "../../apiClient";
import AuthorRow from "./AuthorRow";

type AuthorTableProps = {
    authors: AuthorDto[];
    onUpdate: (id: string, name: string) => Promise<void> | void;
    onDelete: (id: string) => Promise<void> | void;
};

export default function AuthorTable({ authors, onUpdate, onDelete }: AuthorTableProps) {
    return (
        <div className="overflow-x-auto">
            <table className="table">
                <thead>
                    <tr>
                        <th>Navn</th>
                        <th className="text-right">Handlinger</th>
                    </tr>
                </thead>
                <tbody>
                    {authors.map((author) => (
                        <AuthorRow key={author.id} author={author} onUpdate={onUpdate} onDelete={onDelete} />
                    ))}
                </tbody>
            </table>
        </div>
    );
}