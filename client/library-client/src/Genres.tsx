import { useAtom } from "jotai";
import { useEffect } from "react";
import { genresAtom, fetchGenresAtom, deleteGenreAtom } from "./state/atoms";

export default function Genres() {
    const [genres] = useAtom(genresAtom);
    const [, fetchGenres] = useAtom(fetchGenresAtom);
    const [, deleteGenre] = useAtom(deleteGenreAtom);

    useEffect(() => {
        fetchGenres();
    }, []);

    return (
        <div>
            <h1 className="text-3xl font-bold mb-4">Genrer</h1>
            <table className="table w-full bg-base-200 rounded-xl shadow-md">
                <thead>
                <tr>
                    <th>Navn</th>
                    <th></th>
                </tr>
                </thead>
                <tbody>
                {genres.map((g) => (
                    <tr key={g.id}>
                        <td>{g.name}</td>
                        <td>
                            <button
                                onClick={() => deleteGenre(g.id!)}
                                className="btn btn-error btn-sm"
                            >
                                Slet
                            </button>
                        </td>
                    </tr>
                ))}
                </tbody>
            </table>
        </div>
    );
}
