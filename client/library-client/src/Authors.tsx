import { useAtom } from "jotai";
import { useEffect } from "react";
import { authorsAtom, fetchAuthorsAtom, deleteAuthorAtom } from "./state/atoms";

export default function Authors() {
    const [authors] = useAtom(authorsAtom);
    const [, fetchAuthors] = useAtom(fetchAuthorsAtom);
    const [, deleteAuthor] = useAtom(deleteAuthorAtom);

    useEffect(() => {
        fetchAuthors();
    }, []);

    return (
        <table className="table">
            <thead>
            <tr><th>Navn</th><th></th></tr>
            </thead>
            <tbody>
            {authors.map(a => (
                <tr key={a.id}>
                    <td>{a.name}</td>
                    <td>
                        <button onClick={() => deleteAuthor(a.id!)} className="btn btn-error btn-sm">Slet</button>
                    </td>
                </tr>
            ))}
            </tbody>
        </table>
    );
}
