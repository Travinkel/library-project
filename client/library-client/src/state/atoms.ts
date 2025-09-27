import { atom } from "jotai";
import {
    ApiClient,
    AuthorDto,
    BookDto,
    CreateAuthorDto,
    CreateBookDto,
    CreateGenreDto,
    GenreDto
} from "../apiClient";

const locale = "da";
const sortAuthors = (items: AuthorDto[]) => [...items].sort((a, b) => (a.name ?? "").localeCompare(b.name ?? "", locale, { sensitivity: "base" }));
const sortBooks = (items: BookDto[]) => [...items].sort((a, b) => (a.title ?? "").localeCompare(b.title ?? "", locale, { sensitivity: "base" }));
const sortGenres = (items: GenreDto[]) => [...items].sort((a, b) => (a.name ?? "").localeCompare(b.name ?? "", locale, { sensitivity: "base" }));

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
        set(authorsAtom, sortAuthors(data));
    }
);

export const createAuthorAtom = atom(
    null,
    async (get, set, dto: CreateAuthorDto) => {
        const client = get(apiClientAtom);
        const created = await client.authorPOST(dto);
        set(authorsAtom, sortAuthors([...get(authorsAtom), created]));
        return created;
    }
);

export const updateAuthorAtom = atom(
    null,
    async (get, set, payload: { id: string; dto: CreateAuthorDto }) => {
        const client = get(apiClientAtom);
        const updated = await client.authorPUT(payload.id, payload.dto);
        set(authorsAtom, sortAuthors(get(authorsAtom).map(a => a.id === updated.id ? updated : a)));
        return updated;
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
        set(booksAtom, sortBooks(data));
    }
);

export const createBookAtom = atom(
    null,
    async (get, set, dto: CreateBookDto) => {
        const client = get(apiClientAtom);
        const created = await client.bookPOST(dto);
        set(booksAtom, sortBooks([...get(booksAtom), created]));
        return created;
    }
);

export const updateBookAtom = atom(
    null,
    async (get, set, payload: { id: string; dto: CreateBookDto }) => {
        const client = get(apiClientAtom);
        const updated = await client.bookPUT(payload.id, payload.dto);
        set(booksAtom, sortBooks(get(booksAtom).map(b => b.id === updated.id ? updated : b)));
        return updated;
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
        set(genresAtom, sortGenres(data));
    }
);

export const createGenreAtom = atom(
    null,
    async (get, set, dto: CreateGenreDto) => {
        const client = get(apiClientAtom);
        const created = await client.genrePOST(dto);
        set(genresAtom, sortGenres([...get(genresAtom), created]));
        return created;
    }
);

export const updateGenreAtom = atom(
    null,
    async (get, set, payload: { id: string; dto: CreateGenreDto }) => {
        const client = get(apiClientAtom);
        const updated = await client.genrePUT(payload.id, payload.dto);
        set(genresAtom, sortGenres(get(genresAtom).map(g => g.id === updated.id ? updated : g)));
        return updated;
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
