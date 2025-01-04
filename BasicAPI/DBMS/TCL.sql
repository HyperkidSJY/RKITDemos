--  Transaction Control Language (TCL)

-- TCL commands manage transactions, ensuring data integrity.

-- Start a transaction
START TRANSACTION;

-- Insert data
INSERT INTO Students (StudentID, Name, Age, Grade, Email)
VALUES (3, 'Charlie', 22, 'B', 'charlie@example.com');

-- Update data
UPDATE Students SET Age = 23 WHERE StudentID = 3;

-- Commit the transaction
COMMIT;

-- Start another transaction
START TRANSACTION;

-- Insert data
INSERT INTO Students (StudentID, Name, Age, Grade, Email)
VALUES (4, 'David', 24, 'C', 'david@example.com');

-- Rollback the transaction
ROLLBACK;


-- COMMIT
-- Permanently saves changes made by the current transaction.
-- Cannot undo changes after execution.

-- ROLLBACK
-- Undoes changes made by the current transaction.
-- Reverts the database to its previous state before the transaction.

