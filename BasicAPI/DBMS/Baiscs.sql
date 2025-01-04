CREATE DATABASE Employee;
USE Employee;

CREATE TABLE employees (
    employee_id INT PRIMARY KEY,
    first_name VARCHAR(50),
    last_name VARCHAR(50),
    department VARCHAR(50),
    salary DECIMAL(10, 2),
    hire_date DATE,
    bonus DECIMAL(10, 2)
);

INSERT INTO employees VALUES
(1, 'Alice', 'Johnson', 'HR', 55000, '2021-05-15', NULL),
(2, 'Bob', 'Smith', 'IT', 75000, '2020-03-10', 5000),
(3, 'Carol', 'Taylor', 'Finance', 65000, '2018-11-22', NULL),
(4, 'David', 'Brown', 'IT', 80000, '2019-06-30', 7000),
(5, 'Eve', 'Davis', NULL, 60000, '2022-02-18', NULL);

SELECT * 
FROM employees
ORDER BY salary DESC;

-- NULL -> Represents the absence of a value (unknown or missing).	
-- Empty -> Represents a value that exists but is blank (e.g., an empty string or zero).
SELECT * 
FROM employees
WHERE bonus IS NULL;

-- MySQL reserved keywords can be used as column names, but they must be enclosed in backticks.
-- ALTER TABLE employees ADD `rank` INT;

-- UPDATE employees 
-- SET `rank` = ROW_NUMBER() OVER (ORDER BY salary DESC);

-- SELECT employee_id, first_name, salary, `rank`
-- FROM employees
-- ORDER BY `rank`;

-- ALTER TABLE employees AUTO_INCREMENT = 100;

-- Fetches the first 5 rows from the employees table.
SELECT * 
FROM employees
LIMIT 5;

-- Skips the first 10 rows and fetches the next 5 rows. 
SELECT * 
FROM employees
LIMIT 5 OFFSET 10;



