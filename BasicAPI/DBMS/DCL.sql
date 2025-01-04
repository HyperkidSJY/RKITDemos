-- Data Control Language (DCL)

-- DCL commands are used to control access to the database by granting and revoking permissions.

-- Create a new user
CREATE USER 'shivam'@'localhost' IDENTIFIED BY 'password123';

-- Grant privileges
GRANT SELECT, INSERT ON School.* TO 'shivam'@'localhost';

-- Revoke privileges
REVOKE INSERT ON School.* FROM 'shivam'@'localhost';

-- Drop the user
DROP USER 'shivam'@'localhost';
