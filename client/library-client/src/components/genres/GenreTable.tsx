import { GenreDto } from "../../apiClient";
import GenreRow from "./GenreRow";

type GenreTableProps = {
    genres: GenreDto[];
    onUpdate: (id: string, name: string) => Promise<void> | void;
    onDelete: (id: string) => Promise<void> | void;
};

export default function GenreTable({ genres, onUpdate, onDelete }: GenreTableProps) {
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
                    {genres.map((genre) => (
                        <GenreRow key={genre.id} genre={genre} onUpdate={onUpdate} onDelete={onDelete} />
                    ))}
                </tbody>
            </table>
        </div>
    );
}