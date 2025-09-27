import { useState, type FormEvent, type RefObject } from "react";

export type GenreFormProps = {
    onCreate: (name: string) => Promise<void> | void;
    inputRef: RefObject<HTMLInputElement | null>;
};

export default function GenreForm({ onCreate, inputRef }: GenreFormProps) {
    const [name, setName] = useState("");

    const submitNewGenre = async (evt: FormEvent) => {
        evt.preventDefault();
        const trimmed = name.trim();
        if (!trimmed) {
            alert("Navn skal udfyldes.");
            inputRef.current?.focus();
            return;
        }

        await onCreate(trimmed);
        setName("");
    };

    return (
        <form className="card bg-base-200 shadow" onSubmit={submitNewGenre}>
            <div className="card-body">
                <h2 className="card-title">Tilfoej genre</h2>
                <div className="flex gap-3">
                    <input
                        ref={inputRef}
                        className="input input-bordered flex-1"
                        placeholder="Navn"
                        value={name}
                        onChange={(e) => setName(e.target.value)}
                    />
                    <button className="btn btn-primary" type="submit">
                        Tilfoej
                    </button>
                </div>
            </div>
        </form>
    );
}