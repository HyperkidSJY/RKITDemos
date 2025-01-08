use Shivam_422;

-- The UNION ALL operation also combines results from two or more SELECT statements. However, unlike UNION, it does not remove duplicate rows.

SELECT column_name FROM table1
UNION ALL
SELECT column_name FROM table2;

-- There is no functional difference between INNER JOIN and JOIN. The term INNER JOIN is more explicit, while JOIN is used as a shorthand that SQL understands as an inner join by default.

-- order by asc and desc


SELECT 
    e.emp_name, 
    d.dept_name, 
    e.dept_id
FROM employees e
LEFT JOIN departments d
    ON e.dept_id = d.dept_id
RIGHT JOIN departments d2
    ON e.dept_id = d2.dept_id
INNER JOIN departments d3
    ON e.dept_id = d3.dept_id
ORDER BY e.emp_name;

-- Explanation of the Joins:
--     LEFT JOIN: Includes all rows from employees and the matching rows from departments.
--     RIGHT JOIN: Includes all rows from departments and the matching rows from employees.
--     INNER JOIN: Ensures only employees that have a valid department are included in the result.



-- First part of the query: INNER JOIN between employees and departments
SELECT 
    e.emp_name, 
    d.dept_name,
    e.salary,
    -- Aggregate function: Average salary for each department
    AVG(e.salary) AS avg_dept_salary,
    -- Subquery: Count of employees in the same department
    (SELECT COUNT(*) 
     FROM employees 
     WHERE dept_id = e.dept_id) AS dept_emp_count
FROM 
    employees e
INNER JOIN 
    departments d ON e.dept_id = d.dept_id  -- INNER JOIN to combine data based on dept_id
WHERE 
    e.salary > (SELECT AVG(salary) FROM employees)  -- Subquery to filter employees with salary higher than the average
GROUP BY 
    e.dept_id, e.emp_name, d.dept_name, e.salary  -- GROUP BY to calculate AVG salary per department

UNION  -- Union operator to combine the two parts

-- Second part of the query: RIGHT JOIN to include departments without employees
SELECT 
    e.emp_name, 
    d.dept_name,
    e.salary,
    NULL AS avg_dept_salary,  -- NULL for aggregate columns as no employees in the department
    NULL AS dept_emp_count   -- NULL for departments without employees
FROM 
    employees e
RIGHT JOIN 
    departments d ON e.dept_id = d.dept_id  -- RIGHT JOIN to include all departments
WHERE 
    e.emp_id IS NULL  -- Ensure we only get departments without employees

ORDER BY 
    salary DESC  -- Apply ORDER BY after the UNION to sort the final result
LIMIT 5;  -- Limit the number of rows returned for the entire query


-- First Part (INNER JOIN):

--     This part returns employees whose salary is above the average salary of all employees.
--     The result includes the employee’s name, department name, their salary, the average salary for their department, and the count of employees in their department.

-- Second Part (RIGHT JOIN):

--     This part returns departments that have no employees.
--     The result includes the department’s name and NULL for employee-related columns such as emp_name, salary, avg_dept_salary, and dept_emp_count.

-- UNION:

--     Combines both sets of results—employees and departments without employees.
--     The union will remove any duplicate rows, but since the two parts are logically distinct, there should not be any duplicates.

-- Sorting and Limiting:

--     The final result is sorted by salary in descending order.
--     Only the top 5 rows from the combined result set are returned.







