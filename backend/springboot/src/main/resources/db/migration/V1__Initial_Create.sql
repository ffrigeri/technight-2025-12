-- Create example table
CREATE TABLE example (
    id SERIAL PRIMARY KEY,
    name VARCHAR(200) NOT NULL,
    title VARCHAR(200) NOT NULL,
    entry_date TIMESTAMP NOT NULL,
    description VARCHAR(1000),
    is_active BOOLEAN NOT NULL DEFAULT true
);

-- Create index on entry_date for better query performance
CREATE INDEX ix_example_entry_date ON example(entry_date);

-- Insert seed data
INSERT INTO example (id, name, title, entry_date, description, is_active)
VALUES 
    (1, 'First Example', 'Introduction', '2025-12-02 12:00:00', 'This is the first example entry', true),
    (2, 'Second Example', 'Advanced Topics', '2025-12-01 12:00:00', 'This is the second example entry', true);

-- Update sequence to continue from the seed data
SELECT setval('example_id_seq', (SELECT MAX(id) FROM example));

