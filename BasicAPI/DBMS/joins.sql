CREATE DATABASE IF NOT EXISTS DemoDB;
USE DemoDB;

-- Create the departments table
CREATE TABLE departments (
    department_id INT AUTO_INCREMENT PRIMARY KEY,
    department_name VARCHAR(100) NOT NULL
);

-- Insert sample data into departments
INSERT INTO departments (department_name)
VALUES ('HR'), ('IT'), ('Sales'), ('Finance');

-- Create the employees table
CREATE TABLE employees (
    employee_id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(100),
    salary DECIMAL(10, 2),
    department_id INT,
    FOREIGN KEY (department_id) REFERENCES departments(department_id)
);

-- Insert sample data into employees
INSERT INTO employees (name, salary, department_id)
VALUES 
('Alice', 60000, 1),
('Bob', 75000, 2),
('Charlie', 50000, 1),
('David', 80000, 3),
('Eve', 90000, 4);


-- A sub-query is a query nested within another query. Below is an example of finding employees whose salary is above the average salary.
-- Sub-query to find employees earning above the average salary
SELECT name, salary
FROM employees
WHERE salary > (SELECT AVG(salary) FROM employees);


-- Joins combine rows from multiple tables. Here’s an example using an INNER JOIN to fetch employee details along with their department names.
-- Join employees with departments
SELECT e.name AS employee_name, e.salary, d.department_name
FROM employees e
INNER JOIN departments d
ON e.department_id = d.department_id;

-- Choose the right join based on our requirements:

--     Use INNER JOIN when you need only matching data.
--     Use LEFT JOIN or RIGHT JOIN for data completeness.
--     Use FULL JOIN to ensure no data is left out.
--     Use CROSS JOIN cautiously to avoid large datasets.


-- Unions combine results from multiple SELECT statements. Below, we’ll fetch names of employees and department names in a single query.
-- Union of employees and department names
SELECT name AS entity_name, 'Employee' AS entity_type
FROM employees
UNION
SELECT department_name AS entity_name, 'Department' AS entity_type
FROM departments;

-- An index improves query performance. We’ll create an index on the salary column of the employees table and demonstrate its usage.
-- Create an index on the salary column
CREATE INDEX idx_salary ON employees(salary);
-- Query to utilize the index
SELECT name, salary
FROM employees
WHERE salary > 70000;

EXPLAIN SELECT name, salary FROM employees WHERE salary > 70000;


-- A view is a stored query that can be treated like a table. simplify querying high-earning employees.
-- Create a view for high earners
CREATE VIEW high_earners AS
SELECT name, salary, department_id
FROM employees
WHERE salary > 70000;

-- Query the view
SELECT * FROM high_earners;


-- The EXPLAIN keyword is used to analyze and optimize SQL queries by showing the execution plan.
-- Understand how MySQL processes a query.
-- Identify performance bottlenecks, like missing indexes.
-- Optimize joins, subqueries, and filtering conditions.

EXPLAIN SELECT e.name, e.salary, d.department_name
FROM employees e
INNER JOIN departments d
ON e.department_id = d.department_id;


-- Optimise queries using explain
CREATE TABLE products (
    product_id INT AUTO_INCREMENT PRIMARY KEY,
    product_name VARCHAR(100),
    category_id INT,
    price DECIMAL(10, 2)
);

INSERT INTO products (product_name, category_id, price)
VALUES 
('Laptop', 1, 50000),
('Phone', 1, 30000),
('Tablet', 1, 20000),
('Shirt', 2, 1500),
('Shoes', 2, 2500);

 CREATE TABLE categories (
    category_id INT AUTO_INCREMENT PRIMARY KEY,
    category_name VARCHAR(100)
);

INSERT INTO categories (category_name)
VALUES 
('Electronics'),
('Clothing');

EXPLAIN
SELECT p.product_name, p.price, c.category_name
FROM products p
JOIN categories c
ON p.category_id = c.category_id
WHERE p.price > 20000 AND c.category_name = 'Electronics';

-- categories: Full table scan (type = ALL).
-- products: Full table scan (type = ALL).

CREATE INDEX idx_category_name ON categories(category_name);
CREATE INDEX idx_price ON products(price);

EXPLAIN
SELECT p.product_name, p.price, c.category_name
FROM products p
JOIN categories c
ON p.category_id = c.category_id
WHERE p.price > 20000 AND c.category_name = 'Electronics';



