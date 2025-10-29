-- Create database
CREATE DATABASE MyBatisDemo;
GO

USE MyBatisDemo;
GO

-- Create Users table
CREATE TABLE Users
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    UserName NVARCHAR(50) NOT NULL,
    Email NVARCHAR(100) NOT NULL
);
GO

-- Insert sample data
INSERT INTO Users
    (UserName, Email)
VALUES
    ('sample_user', 'sample@example.com');
GO