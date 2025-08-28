Trepo - E-Commerce API
Overview
Trepo is a modern, scalable e-commerce backend API built with ASP.NET Core (.NET 8, C# 12). It provides robust endpoints for user management, product catalog, shopping cart, order processing, and role-based access control. The project is designed for maintainability, security, and extensibility, adhering to clean code principles and industry best practices. With the integration of a Services Layer and AutoMapper, it ensures clean separation of concerns and efficient data mapping between layers.
Whether you're building an online store or learning about API development, Trepo is a solid foundation to get you started! 
Tech Stack

Backend: ASP.NET Core (.NET 8, C# 12)
ORM: Entity Framework Core
Database: SQL Server
API: RESTful (JSON)
Authentication: JWT + Identity Framework
Validation: Data Annotations
Mapping: AutoMapper for seamless DTO-to-Entity mapping
Architecture: Layered architecture (API, Services, Core, Infrastructure)

Features

User Management: Registration, authentication, profile management, and role-based access control (Admin, Vendor, Customer).
Product Catalog: Create, update, delete, and search products with categories and filtering.
Shopping Cart: Add, update, and remove items, with checkout functionality.
Order Management: Full order lifecycle with status tracking and cancellation.
Category System: Hierarchical product categorization with cascade operations.
Role-Based Security: Granular permissions for Admin, Vendor, and Customer roles.

Project Architecture
Trepo follows a layered architecture to ensure separation of concerns and maintainability:

API Layer:

Controllers handling HTTP requests and responses.
JWT Authentication and Authorization Policies.
Request/Response Models (DTOs).


Services Layer:

Contains business logic and orchestration of operations.
Acts as an intermediary between the API layer and the Core/Infrastructure layers.
Utilizes AutoMapper to map between DTOs and domain entities, reducing boilerplate code and ensuring clean data transformation.


Core Layer:

Business logic and domain entities.
Service interfaces defining contracts for the Services Layer.
Data Transfer Objects (DTOs) for API communication.


Infrastructure Layer:

Entity Framework Core for database operations.
Repository Pattern for data access.
Database Context configuration.



AutoMapper Usage
AutoMapper is integrated into the Services Layer to streamline data mapping between DTOs and domain entities. This reduces manual mapping code, improves performance, and ensures consistency. For example:

Mapping ProductCreateDto to Product entity when creating a new product.
Mapping User entity to UserDto for secure data exposure in API responses.

AutoMapper configurations are defined in the project’s startup, ensuring all mappings are centralized and reusable.
API Endpoints
Account Management



Endpoint
Method
Description
Authorization
Request Body



/api/Account/register
POST
Register new user
Public
RegisterDTO


/api/Account/login
POST
User authentication
Public
LoginDto


/api/Account/Logout
POST
User logout
Authenticated
-


/api/Account/users
GET
Get all users (paginated)
Admin
Query params


/api/Account/users/{email}
GET
Get user by email
Admin
-


/api/Account/Updateusers/{email}
PUT
Update user details
Admin
UpdateUserDTO


/api/Account/roles/addTOUser
POST
Add role to user
Admin
addRoleDto


Product Management



Endpoint
Method
Description
Authorization
Request Body



/api/Product/AddProduct
POST
Create new product
Admin/Vendor
ProductCreateDto


/api/Product/GetAllProducts
GET
Get all products (paginated)
Public
Query params


/api/Product/GetProductById/{id}
GET
Get product by ID
Public
-


/api/Product/SearchProducts
GET
Search products
Public
Query params


/api/Product/UpdateProduct/{id}
PUT
Update product
Admin/Vendor
ProductUpdateDto


/api/Product/DeleteProduct/{id}
DELETE
Delete product
Admin/Vendor
-


Category Management



Endpoint
Method
Description
Authorization
Request Body



/api/Category/AddCategory
POST
Create new category
Admin
CreateCategoryDto


/api/Category/GetAllCategories
GET
Get all categories
Public
-


/api/Category/GetCategoryById/{id}
GET
Get category by ID
Public
-


/api/Category/UpdateCategory/{id}
PUT
Update category
Admin
UpdateCategoryDto


/api/Category/DeleteCategory/{id}
DELETE
Delete category
Admin
-


Cart & Checkout



Endpoint
Method
Description
Authorization
Request Body



/api/Cart/AddToCart
POST
Add item to cart
Customer
AddtocartDto


/api/Cart/GetCart/{username}
GET
Get user's cart
Customer
-


/api/Cart/UpdateItem
PUT
Update cart item
Customer
UpdateCartitemDto


/api/Cart/RemoveItem
DELETE
Remove cart item
Customer
RemoveCartItemDto


/api/Cart/Checkout
POST
Process checkout
Customer
CheckoutRequestDTO


Order Management



Endpoint
Method
Description
Authorization
Request Body



/api/Order/CreateOrder
POST
Create new order



