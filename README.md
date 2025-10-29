# MyBatis.NET 1.4.0 Demo with DDD Architecture

This project demonstrates MyBatis.NET 1.4.0 with Domain-Driven Design (DDD) architecture, featuring:

- ✅ Basic CRUD operations
- ✅ Custom mapper configurations (Result Maps, Dynamic SQL)
- ✅ DDD architecture with multiple libraries
- ✅ Async operations
- ✅ Transaction management

## Project Structure

```
MyBatis.TestApp/
├── Domain/                    # Domain Layer
│   ├── Entities/
│   │   └── User.cs
│   ├── Repositories/
│   │   └── IUserRepository.cs
│   └── IUnitOfWork.cs
├── Application/               # Application Layer
│   └── Services/
│       ├── IUserService.cs
│       └── UserService.cs
├── Infrastructure/            # Infrastructure Layer
│   ├── Configuration/
│   │   └── DatabaseConfig.cs
│   ├── Mappers/
│   │   ├── IUserMapper.cs
│   │   └── UserMapper.xml
│   ├── Repositories/
│   │   └── UserRepository.cs
│   └── UnitOfWork.cs
├── Controllers/               # Web API Controllers
│   └── UsersController.cs
└── Program.cs                 # ASP.NET Core Startup
```

## API Endpoints

### Users Management

- `GET /api/users` - Get all users
- `GET /api/users/{id}` - Get user by ID
- `GET /api/users/search/{userName}` - Search users by username
- `POST /api/users` - Create new user
- `PUT /api/users/{id}` - Update user
- `DELETE /api/users/{id}` - Delete user
- `POST /api/users/transaction` - Create multiple users in transaction

## Database Setup

Run the SQL script in `DatabaseSetup.sql` to create the database and table:

```sql
-- Create database
CREATE DATABASE MyBatisDemo;
GO

USE MyBatisDemo;
GO

-- Create Users table
CREATE TABLE Users (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    UserName NVARCHAR(50) NOT NULL,
    Email NVARCHAR(100) NOT NULL
);
GO

-- Insert sample data
INSERT INTO Users (UserName, Email) VALUES
('sample_user', 'sample@example.com');
GO
```

## Features Demonstrated

### 1. Basic CRUD Operations

- Create, Read, Update, Delete users
- Async operations with Task.Run wrapper

### 2. Custom Mapper Configurations

- Result Maps for entity mapping
- Dynamic SQL with conditional queries
- Parameter mapping

### 3. DDD Architecture

- **Domain Layer**: Entities, Repository interfaces, Unit of Work
- **Application Layer**: Business logic services
- **Infrastructure Layer**: Data access implementations
- **Presentation Layer**: Web API controllers

### 4. Async Operations

- All repository and service methods are async
- Wrapped synchronous MyBatis calls with Task.Run

### 5. Transaction Management

- Unit of Work pattern implementation
- Transactional operations for multi-user creation
- Commit/Rollback support

## Configuration

Update connection string in `Program.cs`:

```csharp
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? "Server=.;Database=MyBatisDemo;Trusted_Connection=True;";
```

## Running the Application

```bash
dotnet run
```

The API will be available at `https://localhost:5001` with Swagger UI at `https://localhost:5001/swagger`.

## Sample API Usage

### Create User

```bash
POST /api/users
Content-Type: application/json

{
  "userName": "john_doe",
  "email": "john@example.com"
}
```

### Get All Users

```bash
GET /api/users
```

### Search Users

```bash
GET /api/users/search/john
```

### Transactional Operation

```bash
POST /api/users/transaction
Content-Type: application/json

[
  {
    "userName": "alice_tx",
    "email": "alice@example.com"
  },
  {
    "userName": "bob_tx",
    "email": "bob@example.com"
  }
]
```

## Technologies Used

- .NET 10.0
- MyBatis.NET 1.4.0 (resolved to 2.0.0)
- ASP.NET Core Web API
- Microsoft.Data.SqlClient
- Swagger/OpenAPI
- Domain-Driven Design patterns
