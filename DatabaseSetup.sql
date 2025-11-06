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

-- Insert sample Users data
INSERT INTO Users
    (UserName, Email)
VALUES
    ('john_doe', 'john@example.com'),
    ('jane_smith', 'jane@example.com'),
    ('bob_wilson', 'bob@example.com');
GO

-- Create Products table (for Dynamic SQL demo)
CREATE TABLE Products
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(500) NULL,
    Price DECIMAL(18,2) NOT NULL,
    Category NVARCHAR(50) NOT NULL,
    Stock INT NOT NULL DEFAULT 0,
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE()
);
GO

-- Insert sample Products data
INSERT INTO Products
    (Name, Description, Price, Category, Stock, IsActive)
VALUES
    ('Laptop Pro 15', 'High-performance laptop with 15-inch display', 1299.99, 'Electronics', 25, 1),
    ('Wireless Mouse', 'Ergonomic wireless mouse with USB receiver', 29.99, 'Electronics', 150, 1),
    ('Office Chair', 'Comfortable office chair with lumbar support', 249.99, 'Furniture', 40, 1),
    ('Standing Desk', 'Adjustable height standing desk', 399.99, 'Furniture', 15, 1),
    ('USB-C Cable', '2-meter USB-C to USB-C cable', 12.99, 'Accessories', 200, 1),
    ('Laptop Bag', 'Padded laptop bag for 15-inch laptops', 45.00, 'Accessories', 80, 1),
    ('4K Monitor', '27-inch 4K UHD monitor', 449.99, 'Electronics', 30, 1),
    ('Mechanical Keyboard', 'RGB mechanical gaming keyboard', 89.99, 'Electronics', 60, 1),
    ('Desk Lamp', 'LED desk lamp with adjustable brightness', 34.99, 'Furniture', 100, 1),
    ('Webcam HD', '1080p HD webcam with microphone', 69.99, 'Electronics', 45, 1),
    ('Notebook Set', 'Set of 3 lined notebooks', 15.99, 'Stationery', 120, 1),
    ('Pen Pack', 'Pack of 10 ballpoint pens', 8.99, 'Stationery', 250, 1),
    ('Wireless Headset', 'Bluetooth headset with noise cancellation', 129.99, 'Electronics', 35, 1),
    ('Bookshelf', '5-tier wooden bookshelf', 179.99, 'Furniture', 20, 1),
    ('Cable Organizer', 'Desktop cable management system', 19.99, 'Accessories', 150, 1);
GO