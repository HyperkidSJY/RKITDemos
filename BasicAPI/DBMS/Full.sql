-- Create Database
CREATE DATABASE IF NOT EXISTS company_db;
USE company_db;

-- Create Employees Table (yme01)
CREATE TABLE yme01 (
    e01f01 INT AUTO_INCREMENT PRIMARY KEY, -- Employee ID
    e01f02 VARCHAR(50), -- First Name
    e01f03 VARCHAR(50), -- Last Name
    e01f04 INT, -- Department ID
    e01f05 DECIMAL(10, 2), -- Salary
    e01f06 DATE -- Joining Date
);

-- Create Departments Table (ymd01)
CREATE TABLE ymd01 (
    d01f01 INT AUTO_INCREMENT PRIMARY KEY, -- Department ID
    d01f02 VARCHAR(50) -- Department Name
);

-- Create Projects Table (ymp01)
CREATE TABLE ymp01 (
    p01f01 INT AUTO_INCREMENT PRIMARY KEY, -- Project ID
    p01f02 VARCHAR(100), -- Project Name
    p01f03 DATE, -- Start Date
    p01f04 DATE, -- End Date
    p01f05 DECIMAL(15, 2) -- Budget
);

-- Insert Data into Departments (ymd01)
INSERT INTO ymd01 (d01f02) VALUES 
('HR'), ('IT'), ('Finance');

-- Insert Data into Employees (yme01)
INSERT INTO yme01 (e01f02, e01f03, e01f04, e01f05, e01f06) VALUES
('John', 'Doe', 1, 50000, '2020-01-15'),
('Jane', 'Smith', 2, 75000, '2019-03-10'),
('Robert', 'Brown', 3, 62000, '2021-07-22'),
('Emily', 'Davis', 2, 80000, '2018-11-01');

-- Insert Data into Projects (ymp01)
INSERT INTO ymp01 (p01f02, p01f03, p01f04, p01f05) VALUES
('Project Alpha', '2023-01-01', '2023-06-30', 100000.00),
('Project Beta', '2023-07-01', '2023-12-31', 200000.00);

-- Data Sorting
SELECT * FROM yme01 ORDER BY e01f05 DESC;

-- Null Value & Keyword
ALTER TABLE yme01 ADD e01f07 INT; -- Add Manager ID Column
SELECT * FROM yme01 WHERE e01f07 IS NULL;

-- Auto Increment
SELECT * FROM yme01;

-- DDL Example
ALTER TABLE yme01 ADD e01f08 VARCHAR(100); -- Add Email Column

-- DML Example
UPDATE yme01 SET e01f05 = e01f05 * 1.1 WHERE e01f04 = 2;

-- DCL Example
-- GRANT SELECT, INSERT ON company_db.* TO 'user'@'localhost' IDENTIFIED BY 'password';

-- TCL Example
START TRANSACTION;
UPDATE yme01 SET e01f05 = e01f05 - 1000 WHERE e01f01 = 1;
ROLLBACK;

-- DQL Example
SELECT * FROM yme01 WHERE e01f05 > 60000;

-- Limit
SELECT * FROM yme01 ORDER BY e01f05 DESC LIMIT 2;

-- Aggregate Functions
SELECT e01f04, AVG(e01f05) AS avg_salary FROM yme01 GROUP BY e01f04;

-- Subqueries
SELECT * FROM yme01 
WHERE e01f05 > (SELECT AVG(e01f05) FROM yme01);

-- Joins
SELECT e.e01f02, e.e01f03, d.d01f02 
FROM yme01 e 
JOIN ymd01 d ON e.e01f04 = d.d01f01;

-- Unions
SELECT e01f02 AS name FROM yme01
UNION
SELECT p01f02 AS name FROM ymp01;

-- Index
CREATE INDEX idx_salary ON yme01 (e01f05);
SHOW INDEX FROM yme01;

-- View
CREATE VIEW high_earners AS 
SELECT e01f02, e01f03, e01f05 
FROM yme01 
WHERE e01f05 > 70000;
SELECT * FROM high_earners;

-- Explain Keyword
EXPLAIN SELECT * FROM yme01 WHERE e01f05 > 60000;