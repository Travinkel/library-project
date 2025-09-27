import { atom } from "jotai";
import { ApiClient, AuthorDto, BookDto, GenreDto } from "../apiClient";

// Base API client atom (respects env var)
export const apiClientAtom = atom(
    new ApiClient(import.meta.env.VITE_API_URL)
);

// Entity state atoms
export const authorsAtom = atom<AuthorDto[]>([]);
export const booksAtom = atom<BookDto[]>([]);
export const genresAtom = atom<GenreDto[]>([]);

// -----------------------------
// Authors
// -----------------------------
export const fetchAuthorsAtom = atom(
    null,
    async (get, set) => {
        const client = get(apiClientAtom);
        const data = await client.authorAll();
        set(authorsAtom, data);
    }
);

export const deleteAuthorAtom = atom(
    null,
    async (get, set, id: string) => {
        const client = get(apiClientAtom);
        await client.authorDELETE(id);
        set(authorsAtom, get(authorsAtom).filter(a => a.id !== id));
    }
);

// -----------------------------
// Books
// -----------------------------
export const fetchBooksAtom = atom(
    null,
    async (get, set) => {
        const client = get(apiClientAtom);
        const data = await client.bookAll();
        set(booksAtom, data);
    }
);

export const deleteBookAtom = atom(
    null,
    async (get, set, id: string) => {
        const client = get(apiClientAtom);
        await client.bookDELETE(id);
        set(booksAtom, get(booksAtom).filter(b => b.id !== id));
    }
);

// -----------------------------
// Genres
// -----------------------------
export const fetchGenresAtom = atom(
    null,
    async (get, set) => {
        const client = get(apiClientAtom);
        const data = await client.genreAll();
        set(genresAtom, data);
    }
);

export const deleteGenreAtom = atom(
    null,
    async (get, set, id: string) => {
        const client = get(apiClientAtom);
        await client.genreDELETE(id);
        set(genresAtom, get(genresAtom).filter(g => g.id !== id));
    }
);