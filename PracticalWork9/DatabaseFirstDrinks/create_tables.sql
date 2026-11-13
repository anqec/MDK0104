-- Практическая №9. Сначала таблица, потом классы C# через scaffold.
CREATE TABLE IF NOT EXISTS drinks (
    id SERIAL PRIMARY KEY,
    name VARCHAR(50) NOT NULL,
    price INTEGER NOT NULL
);
