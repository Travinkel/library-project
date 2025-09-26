import { useEffect } from "react";
import { ApiClient, CreateGenreDTO } from "./apiClient";

function App() {
    useEffect(() => {
        const client = new ApiClient("http://localhost:5132");

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
        </div>
    );
}

export default App;
