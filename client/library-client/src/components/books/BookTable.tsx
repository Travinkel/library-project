import { BookDto, GenreDto } from "../../apiClient";
import BookRow from "./BookRow";

type BookTableProps = {
    books: BookDto[];
    genres: GenreDto[];
    onUpdate: (id: string, dto: { title: string; pages: number; genreId?: string }) => Promise<void> | void;
    onDelete: (id: string) => Promise<void> | void;
};

export default function BookTable({ books, genres, onUpdate, onDelete }: BookTableProps) {
    return (
        <div className="overflow-x-auto">
            <table className="table">
                <thead>
                    <tr>
                        <th>Titel</th>
                        <th>Sider</th>
                        <th>Genre</th>
                        <th className="text-right">Handlinger</th>
                    </tr>
                </thead>
                <tbody>
                    {books.map((book) => (
                        <BookRow key={book.id} book={book} genres={genres} onUpdate={onUpdate} onDelete={onDelete} />
                    ))}
                </tbody>
            </table>
        </div>
    );
}