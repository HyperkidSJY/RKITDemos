-- Data Manipulation Language (DML)

-- DML commands are used to manipulate data within tables. INSERT, UPDATE, DELETE

-- Insert data into the table
INSERT INTO Students (StudentID, Name, Age, Grade, Email)
VALUES 
(1, 'Alice', 20, 'A', 'alice@example.com'),
(2, 'Bob', 21, 'B', 'bob@example.com');

-- Update data
UPDATE Students 
SET Grade = 'A+'
WHERE StudentID = 2;

-- Delete data
DELETE FROM Students
WHERE Age > 20;

-- safe update : GUI and command
-- Safe updates in MySQL ensure that update or delete operations cannot run without a WHERE clause or a LIMIT clause, thereby preventing accidental modification or deletion of unintended rows. 
