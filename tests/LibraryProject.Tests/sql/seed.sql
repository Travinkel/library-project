-- Insert some genres
insert into library.genre (id, name, createdat)
values
    (gen_random_uuid()::text, 'Fantasy', now()),
    (gen_random_uuid()::text, 'Science Fiction', now()),
    (gen_random_uuid()::text, 'Philosophy', now());

-- Insert some authors
insert into library.author (id, name, createdat)
values
    (gen_random_uuid()::text, 'Ursula K. Le Guin', now()),
    (gen_random_uuid()::text, 'J.R.R. Tolkien', now()),
    (gen_random_uuid()::text, 'Plato', now());

-- Insert some books
insert into library.book (id, title, pages, createdat, genreid)
values
    (gen_random_uuid()::text, 'A Wizard of Earthsea', 240, now(),
     (select id from library.genre where name = 'Fantasy')),
    (gen_random_uuid()::text, 'The Lord of the Rings', 1178, now(),
     (select id from library.genre where name = 'Fantasy')),
    (gen_random_uuid()::text, 'The Republic', 416, now(),
     (select id from library.genre where name = 'Philosophy'));
