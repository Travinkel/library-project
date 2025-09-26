import { useAtom } from "jotai";
import { apiClientAtom, authorsAtom } from "./state/atoms";
import { useEffect} from "react";

export default function Authors() {
    const [client] = useAtom(apiClientAtom);
    const [authors, setAuthors] = useAtom(authorsAtom);

    useEffect(() => {
        client.authorAll().then(setAuthors);
    }, []);

    return (
        <div className="p-6">
            <h1 className="text-2xl font-bold">Authors</h1>
            <ul className="mt-4 space-y-2">
                {authors.map(a => (
                    <li key={a.id} className="p-2 border-b">
                        {a.name}
                    </li>
                ))}
            </ul>
        </div>
    );
}