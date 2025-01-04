CREATE TABLE sales (
    id INT AUTO_INCREMENT PRIMARY KEY,
    product_name VARCHAR(50),
    quantity INT,
    price DECIMAL(10, 2),
    region VARCHAR(50)
);

INSERT INTO sales (product_name, quantity, price, region) VALUES
('Product A', 10, 15.50, 'North'),
('Product B', 20, 25.00, 'South'),
('Product A', 15, 15.50, 'East'),
('Product C', NULL, 35.00, 'West'),
('Product B', 5, 25.00, 'North'),
('Product A', 10, NULL, 'South'),
('Product C', 12, 35.00, 'East');

-- nested agg

-- Total number of rows
SELECT COUNT(*) AS total_rows FROM sales;

-- Count of non-NULL values in the 'quantity' column
SELECT COUNT(quantity) AS non_null_quantities FROM sales;

-- Total quantity sold
SELECT SUM(quantity) AS total_quantity FROM sales;

-- Total revenue (price multiplied by quantity)
SELECT SUM(price * quantity) AS total_revenue FROM sales;

-- Average price of products
SELECT AVG(price) AS avg_price FROM sales;

-- Average quantity sold
SELECT AVG(quantity) AS avg_quantity FROM sales;

-- Minimum quantity sold
SELECT MIN(quantity) AS min_quantity FROM sales;

-- Minimum price
SELECT MIN(price) AS min_price FROM sales;

-- Maximum quantity sold
SELECT MAX(quantity) AS max_quantity FROM sales;

-- Maximum price
SELECT MAX(price) AS max_price FROM sales;

-- Total quantity sold by product
-- Groups data by a column and applies aggregate functions to each group.
SELECT product_name, SUM(quantity) AS total_quantity
FROM sales
GROUP BY product_name;

-- Filters aggregated data using HAVING.
-- Show regions with total sales revenue greater than 500
SELECT region, SUM(price * quantity) AS total_revenue
FROM sales
GROUP BY region
HAVING total_revenue > 500;

-- grp concat


