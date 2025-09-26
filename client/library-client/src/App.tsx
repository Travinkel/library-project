import { useEffect } from "react";
import { ApiClient, CreateGenreDTO } from "./apiClient";

function App() {
    useEffect(() => {
        const client = new ApiClient(import.meta.env.VITE_API_URL);

        const run = async () => {
            try {
                const dto = new CreateGenreDTO();
                dto.name = "Fantasy";
                // Create a genre (POST)
                const created = await client.genrePOST(dto);
                console.log("Created:", created);

                // Get all genres (GET)
                const genres = await client.genreAll();
                console.log("Genres:", genres);
            } catch (err) {
                console.error("API error:", err);
            }
        };

        run();
    }, []);

    return (
        <div>
            <h1>Library Client</h1>
            <a className="btn btn-ghost" href="/books">Books</a>
            <a className="btn btn-ghost" href="/authors">Authors</a>
        </div>
    );
}

export default App;
