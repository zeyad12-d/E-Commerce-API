# Trepo - E-Commerce API

## Overview
**Trepo** is a modern, scalable e-commerce backend API built with **ASP.NET Core (.NET 8, C# 12)**. It offers robust APIs for user management, product catalog, shopping cart, order processing, and role-based access control. Designed for maintainability, security, and extensibility, it follows clean code principles and industry best practices. With a dedicated **Services Layer** and **AutoMapper** integration, it ensures clean business logic separation and efficient data mapping.

---

## Tech Stack
| Icon | Technology | Description |
|------|------------|-------------|
| 🖥️ | **Backend** | ASP.NET Core (.NET 8, C# 12) |
| 🗄️ | **ORM** | Entity Framework Core |
| 🗃️ | **Database** | SQL Server |
| 🔄 | **API** | RESTful (JSON) |
| 🔒 | **Authentication** | JWT + Identity Framework |
| ✅ | **Validation** | Data Annotations |
| 🔗 | **Mapping** | AutoMapper for DTO-to-Entity mapping |

---

## Features
- **🧑‍🤝‍🧑 User Management**: Registration, authentication, profile management, and role-based access control (Admin, Vendor, Customer).
- **📦 Product Catalog**: Comprehensive product management with categories, search, and filtering.
- **🛒 Shopping Cart**: Dynamic cart management with checkout and order processing.
- **📜 Order Management**: Full order lifecycle with status tracking and cancellation.
- **🏷️ Category System**: Hierarchical product categorization with cascade operations.
- **🛡️ Role-Based Security**: Granular permissions for Admin, Vendor, and Customer roles.

---

## API Endpoints

### Account Management
| Endpoint | Method | Description | Authorization | Request Body |
|----------|--------|-------------|---------------|--------------|
| `/api/Account/register` | `POST` | Register new user | Public | `RegisterDTO` |
| `/api/Account/login` | `POST` | User authentication | Public | `LoginDto` |
| `/api/Account/Logout` | `POST` | User logout | Authenticated | - |
| `/api/Account/users` | `GET` | Get all users (paginated) | Admin | Query params |
| `/api/Account/users/{email}` | `GET` | Get user by email | Admin | - |
| `/api/Account/Updateusers/{email}` | `PUT` | Update user details | Admin | `UpdateUserDTO` |
| `/api/Account/roles/addTOUser` | `POST` | Add role to user | Admin | `addRoleDto` |

### Product Management
| Endpoint | Method | Description | Authorization | Request Body |
|----------|--------|-------------|---------------|--------------|
| `/api/Product/AddProduct` | `POST` | Create new product | Admin/Vendor | `ProductCreateDto` |
| `/api/Product/GetAllProducts` | `GET` | Get all products (paginated) | Public | Query params |
| `/api/Product/GetProductById/{id}` | `GET` | Get product by ID | Public | - |
| `/api/Product/SearchProducts` | `GET` | Search products | Public | Query params |
| `/api/Product/UpdateProduct/{id}` | `PUT` | Update product | Admin/Vendor | `ProductUpdateDto` |
| `/api/Product/DeleteProduct/{id}` | `DELETE` | Delete product | Admin/Vendor | - |

### Category Management
| Endpoint | Method | Description | Authorization | Request Body |
|----------|--------|-------------|---------------|--------------|
| `/api/Category/AddCategory` | `POST` | Create new category | Admin | `CreateCategoryDto` |
| `/api/Category/GetAllCategories` | `GET` | Get all categories | Public | - |
| `/api/Category/GetCategoryById/{id}` | `GET` | Get category by ID | Public | - |
| `/api/Category/UpdateCategory/{id}` | `PUT` | Update category | Admin | `UpdateCategoryDto` |
| `/api/Category/DeleteCategory/{id}` | `DELETE` | Delete category | Admin | - |

### Cart & Checkout
| Endpoint | Method | Description | Authorization | Request Body |
|----------|--------|-------------|---------------|--------------|
| `/api/Cart/AddToCart` | `POST` | Add item to cart | Customer | `AddtocartDto` |
| `/api/Cart/GetCart/{username}` | `GET` | Get user's cart | Customer | - |
| `/api/Cart/UpdateItem` | `PUT` | Update cart item | Customer | `UpdateCartitemDto` |
| `/api/Cart/RemoveItem` | `DELETE` | Remove cart item | Customer | `RemoveCartItemDto` |
| `/api/Cart/Checkout` | `POST` | Process checkout | Customer | `CheckoutRequestDTO` |

### Order Management
| Endpoint | Method | Description | Authorization | Request Body |
|----------|--------|-------------|---------------|--------------|
| `/api/Order/CreateOrder` | `POST` | Create new order | Customer | `CreateOrderDto` |
| `/api/Order/GetOrderById/{orderId}` | `GET` | Get order by ID | Admin/Vendor | - |
| `/api/Order/GetAllOrders` | `GET` | Get all orders (paginated) | Admin/Vendor | Query params |
| `/api/Order/GetOrdersByUserName/{userName}` | `GET` | Get user's orders | Customer | - |
| `/api/Order/UpdateOrderStatus/{orderId}` | `PUT` | Update order status | Admin/Vendor | `OrderStatus` |
| `/api/Order/CancelOrder/{orderId}` | `POST` | Cancel order | Customer | - |

---

## Data Transfer Objects (DTOs)
- **RegisterDTO**:
  - UserName
  - Email
  - PhoneNumber
  - Password
- **LoginDto**:
  - UserName
  - Password
- **ProductCreateDto**:
  - Name
  - Description
  - Price
  - CategoryId
  - Stock
- **AddtocartDto**:
  - ProductId
  - Quantity
  - Username
- **CreateOrderDto**:
  - CustomerUsername
  - ShippingAddress
  - PaymentMethod
  - OrderItems
- **CreateCategoryDto**:
  - Name
  - Description
  - ParentCategoryId

---

## Standardized API Response
```json
{
  "statusCode": 200,
  "success": true,
  "data": {},
  "message": "Operation completed successfully",
  "errors": []
}
```

---

## Project Architecture
*Trepo* follows a layered architecture for clean separation of concerns:

- **API Layer**:
  - Controllers for handling HTTP requests/responses.
  - JWT Authentication and Authorization Policies.
  - Request/Response Models (DTOs).
- **Services Layer**:
  - Contains business logic and orchestration of operations.
  - Uses **AutoMapper** to map DTOs to domain entities, reducing boilerplate code.
  - Acts as an intermediary between API and Core/Infrastructure layers.
- **Core Layer**:
  - Business logic and domain entities.
  - Service interfaces defining contracts.
  - DTOs for API communication.
- **Infrastructure Layer**:
  - Entity Framework Core for database operations.
  - Repository Pattern for data access.
  - Database Context configuration.

### AutoMapper Integration
**AutoMapper** is used in the **Services Layer** to streamline data mapping between DTOs and domain entities. For example:
- Mapping `ProductCreateDto` to `Product` entity for creating products.
- Mapping `User` entity to `UserDto` for secure API responses.
AutoMapper configurations are centralized in the project’s startup for reusability and maintainability.

---

## Setup & Installation
1. **Clone the Repository**:
   ```bash
   git clone https://github.com/zeyad12-d/E-Commerce-API.git
   ```
2. **Update Connection String**:
   Edit `appsettings.json` with your SQL Server details:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=YOUR_SERVER;Database=TrepoDB;Trusted_Connection=true;TrustServerCertificate=true;"
     }
   }
   ```
3. **Restore Dependencies**:
   ```bash
   dotnet restore
   ```
4. **Apply Migrations**:
   ```bash
   dotnet ef database update
   ```
5. **Run the Application**:
   ```bash
   dotnet run
   ```

---

## Security Features
- **🔑 JWT Token Authentication**: Secure user authentication.
- **🛡️ Role-Based Authorization**: Granular permissions for Admin, Vendor, and Customer.
- **🔐 Secure Password Hashing**: Protects user credentials.
- **✅ Input Validation**: Ensures safe data handling.
- **🗄️ SQL Injection Protection**: Via Entity Framework Core.
- **🔒 Data Encryption**: Secures sensitive data.

---

## Contributing
Contributions are welcome! Follow these steps:
1. Fork the repository.
2. Create a feature branch (`git checkout -b feature/your-feature`).
3. Commit your changes (`git commit -m 'Add your feature'`).
4. Push to the branch (`git push origin feature/your-feature`).
5. Submit a Pull Request.

---

## Footer
Built with passion using ASP.NET Core, AutoMapper, and modern development practices.  
🚀 Scalable Architecture • 🔒 Enterprise Security • 📈 Performance Optimized • 🛡️ Production Ready