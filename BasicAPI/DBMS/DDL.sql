-- Data Definition Language (DDL)

-- DDL commands are used to define and modify the structure of database objects like tables, schemas, and indexes. CREATE, ALTER, DROP, TRUNCATE

-- Create a database
CREATE DATABASE School;

-- Use the database
USE School;

-- Create a table
CREATE TABLE Students (
    StudentID INT PRIMARY KEY,
    Name VARCHAR(100),
    Age INT,
    Grade VARCHAR(5)
);

-- Alter the table
ALTER TABLE Students ADD Email VARCHAR(100);

-- Drop the table
-- DROP TABLE Students;

-- Truncate the table
-- TRUNCATE TABLE students;

-- DROP
-- Removes the entire table and its structure from the database, including associated objects like indexes. This is a permanent action that cannot be recovered without a backup. 
-- DELETE
-- Removes specific rows from a table while keeping the table structure intact. You can use the WHERE clause to specify a condition for deleting rows. DELETE is slower than TRUNCATE for large tables because it logs each row deletion. 
-- TRUNCATE
-- Removes all rows from a table, but preserves its structure. TRUNCATE is generally faster than DELETE because it doesn't log individual row deletions. It cannot be used to remove specific rows. 

