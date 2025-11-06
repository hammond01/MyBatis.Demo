# MyBatis.NET 2.0.0 Demo with DDD Architecture

This project demonstrates **MyBatis.NET 2.0.0** with Domain-Driven Design (DDD) architecture, featuring:

-   ✅ **Dynamic SQL** - `<if>`, `<where>`, `<set>`, `<choose>`, `<foreach>`, `<trim>` tags
-   ✅ **Code Generator Tool** - Auto-generate C# interfaces from XML mappers
-   ✅ **Mandatory returnSingle** - Type-safe single vs collection queries
-   ✅ **Basic CRUD operations** - Users and Products examples
-   ✅ **Custom mapper configurations** - Result Maps, Parameter mapping
-   ✅ **DDD architecture** - Multi-layer design with repositories
-   ✅ **Async operations** - Full async/await support
-   ✅ **Transaction management** - Built-in transaction handling

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

### Users Management (Basic CRUD)

-   `GET /api/users` - Get all users
-   `GET /api/users/{id}` - Get user by ID
-   `GET /api/users/search/{userName}` - Search users by username
-   `POST /api/users` - Create new user
-   `PUT /api/users/{id}` - Update user
-   `DELETE /api/users/{id}` - Delete user
-   `POST /api/users/transaction` - Create multiple users in transaction

### Products Management (Dynamic SQL Demo)

-   `GET /api/products` - Get all products
-   `GET /api/products/{id}` - Get product by ID
-   `GET /api/products/search` - Search products with multiple optional filters
    -   Query params: `name`, `category`, `minPrice`, `maxPrice`, `isActive`, `minStock`
-   `GET /api/products/categories` - Find products by multiple categories
    -   Query params: `categories` (array)
-   `GET /api/products/search-by-type` - Search by type (name/category/description)
    -   Query params: `searchType`, `searchValue`
-   `GET /api/products/complex-search` - Complex search with nested conditions
    -   Query params: `name`, `categories`, `priceRange`, `inStock`, `isActive`, `orderBy`
-   `POST /api/products` - Create new product
-   `PUT /api/products/{id}` - Update product (dynamic SET)
-   `DELETE /api/products/{id}` - Delete product
-   `GET /api/products/count` - Count products with filters

## Database Setup

Run the SQL script in `DatabaseSetup.sql` to create the database with two tables:

### Tables

1. **Users** - Basic CRUD example

    - Id, UserName, Email

2. **Products** - Dynamic SQL demonstration
    - Id, Name, Description, Price, Category, Stock, IsActive, CreatedDate
    - 15 sample products across multiple categories (Electronics, Furniture, Accessories, Stationery)

```bash
# Execute the SQL script
sqlcmd -S . -i DatabaseSetup.sql
```

Or open `DatabaseSetup.sql` in SQL Server Management Studio and execute.

## Features Demonstrated

### 1. ⚠️ Breaking Change: returnSingle Attribute (v2.0.0)

All `<select>` statements **must** include `returnSingle` attribute:

```xml
<!-- Returns List<User> -->
<select id="GetAll" resultMap="User" returnSingle="false">
  SELECT * FROM Users
</select>

<!-- Returns User? (nullable single object) -->
<select id="GetById" resultMap="User" returnSingle="true">
  SELECT * FROM Users WHERE Id = @id
</select>
```

### 2. 🤖 Code Generator Tool

Auto-generate C# interfaces from XML mappers to keep them in sync!

**Install the tool:**

```bash
dotnet tool install -g MyBatis.NET.SqlMapper.Tool
```

**Generate interface:**

```bash
# Navigate to Infrastructure/Mappers directory
cd Infrastructure/Mappers

# Generate interface from XML
mybatis-gen generate ProductMapper.xml

# Or generate all mappers in folder
mybatis-gen generate-all .
```

The tool will create `IProductMapper.cs` with correct return types based on `returnSingle` attribute!

### 3. 🔄 Dynamic SQL (ProductMapper.xml)

#### `<if>` - Conditional WHERE clauses

```xml
<select id="SearchProducts" returnSingle="false">
  SELECT * FROM Products
  <where>
    <if test="name != null">
      Name LIKE '%' + @name + '%'
    </if>
    <if test="minPrice != null">
      AND Price >= @minPrice
    </if>
  </where>
</select>
```

#### `<foreach>` - IN clause with collections

```xml
<select id="FindByCategories" returnSingle="false">
  SELECT * FROM Products
  WHERE Category IN
  <foreach collection="categories" item="category" open="(" separator="," close=")">
    @category
  </foreach>
</select>
```

#### `<choose>/<when>/<otherwise>` - Switch-case logic

```xml
<select id="SearchByType" returnSingle="false">
  SELECT * FROM Products
  <where>
    <choose>
      <when test="searchType == 'name'">
        Name LIKE '%' + @searchValue + '%'
      </when>
      <when test="searchType == 'category'">
        Category = @searchValue
      </when>
      <otherwise>
        Name LIKE '%' + @searchValue + '%' OR Description LIKE '%' + @searchValue + '%'
      </otherwise>
    </choose>
  </where>
</select>
```

#### `<set>` - Dynamic UPDATE with optional fields

```xml
<update id="UpdateProduct">
  UPDATE Products
  <set>
    <if test="Name != null">Name = @Name,</if>
    <if test="Price != null">Price = @Price,</if>
    <if test="Stock != null">Stock = @Stock,</if>
  </set>
  WHERE Id = @Id
</update>
```

#### Complex Nested Dynamic SQL

See `ComplexSearch` in `ProductMapper.xml` for example with:

-   Multiple `<if>` conditions
-   Nested `<foreach>` for categories
-   Nested `<choose>` for price ranges
-   Dynamic ORDER BY clause

### 4. 📦 Basic CRUD Operations (UserMapper.xml)

-   Create, Read, Update, Delete operations
-   Result Maps for entity mapping
-   Parameter mapping

### 5. 🏗️ DDD Architecture

-   **Domain Layer**: Entities (User, Product), Repository interfaces
-   **Application Layer**: Business logic services
-   **Infrastructure Layer**: Data access implementations, XML mappers
-   **Presentation Layer**: Web API controllers

### 6. ⚡ Async Operations

-   All repository and service methods are async
-   Full async/await support

### 7. 💾 Transaction Management

-   Unit of Work pattern implementation
-   Transactional operations for multi-entity creation
-   Commit/Rollback support

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

## Sample API Usage

### Products - Dynamic SQL Examples

#### Search with multiple filters

```bash
GET /api/products/search?name=laptop&category=Electronics&minPrice=100&maxPrice=2000&isActive=true
```

#### Find by multiple categories (foreach)

```bash
GET /api/products/categories?categories=Electronics&categories=Furniture&categories=Accessories
```

#### Complex search with nested conditions

```bash
GET /api/products/complex-search?name=laptop&categories=Electronics&priceRange=high&inStock=true&orderBy=price
```

#### Update product with dynamic SET

```bash
PUT /api/products/1
Content-Type: application/json

{
  "id": 1,
  "price": 1199.99,
  "stock": 30
  // Only these fields will be updated, others remain unchanged
}
```

## 🚀 Running the Application

```bash
# Restore packages
dotnet restore

# Build project
dotnet build

# Run application
dotnet run --project Presentation
```

The API will be available at `https://localhost:5001` with Swagger UI at `https://localhost:5001/swagger`.

## 🔧 Development Workflow

### Modifying Mappers

1. Edit XML mapper (e.g., `ProductMapper.xml`)
2. Regenerate interface:
    ```bash
    mybatis-gen generate ProductMapper.xml
    ```
3. Review generated interface and adjust if needed
4. Rebuild and test

### Adding New Mapper

1. Create new XML mapper in `Infrastructure/Mappers/`
2. Generate interface:
    ```bash
    mybatis-gen generate NewMapper.xml
    ```
3. Implement repository using the generated interface
4. Register in DI container

## Technologies Used

-   **.NET 8.0**
-   **MyBatis.NET 2.0.0** (with Dynamic SQL support)
-   **MyBatis.NET.SqlMapper.Tool 2.0.1** (Code Generator)
-   **ASP.NET Core Web API**
-   **Microsoft.Data.SqlClient 6.1.2**
-   Swagger/OpenAPI
-   Domain-Driven Design patterns
