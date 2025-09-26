import { atom } from "jotai";
import { ApiClient, AuthorDTO, BookDTO, GenreDTO } from "../apiClient";

// Base API client atom (respects env var)
export const apiClientAtom = atom(
    new ApiClient(import.meta.env.VITE_API_URL)
);

// Entities
export const authorsAtom = atom<AuthorDTO[]>([]);
export const booksAtom = atom<BookDTO[]>([]);
export const genresAtom = atom<GenreDTO[]>([]);
