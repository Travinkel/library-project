import { useAtom } from "jotai";
import { useEffect } from "react";
import { booksAtom, fetchBooksAtom, deleteBookAtom } from "./state/atoms";

export default function Books() {
    const [books] = useAtom(booksAtom);
    const [, fetchBooks] = useAtom(fetchBooksAtom);
    const [, deleteBook] = useAtom(deleteBookAtom);

    useEffect(() => {
        fetchBooks();
    }, []);

    return (
        <table className="table">
            <thead>
            <tr><th>Titel</th><th></th></tr>
            </thead>
            <tbody>
            {books.map(b => (
                <tr key={b.id}>
                    <td>{b.title}</td>
                    <td>
                        <button onClick={() => deleteBook(b.id!)} className="btn btn-error btn-sm">Slet</button>
                    </td>
                </tr>
            ))}
            </tbody>
        </table>
    );
}
