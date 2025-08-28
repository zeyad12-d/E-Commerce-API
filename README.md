<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Trepo - E-Commerce Application README</title>
    <link href="https://cdn.jsdelivr.net/npm/tailwindcss@2.2.19/dist/tailwind.min.css" rel="stylesheet">
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/@fortawesome/fontawesome-free@6.4.0/css/all.min.css">
    <style>
        .code-block {
            background-color: #1a202c;
            border-radius: 8px;
            padding: 1rem;
            overflow-x: auto;
        }
        .endpoint-table {
            border-collapse: collapse;
            width: 100%;
        }
        .endpoint-table th,
        .endpoint-table td {
            border: 1px solid #e2e8f0;
            padding: 12px;
            text-align: left;
        }
        .endpoint-table th {
            background-color: #f7fafc;
            font-weight: 600;
        }
        .method-post { background-color: #fed7d7; color: #c53030; }
        .method-get { background-color: #c6f6d5; color: #2d7d32; }
        .method-put { background-color: #feebc8; color: #dd6b20; }
        .method-delete { background-color: #fed7e2; color: #b83280; }
        
        .feature-card {
            transition: transform 0.2s;
        }
        .feature-card:hover {
            transform: translateY(-2px);
        }
    </style>
</head>
<body class="bg-gray-50 min-h-screen">
    <div class="max-w-7xl mx-auto px-4 py-8">
        <!-- Header -->
        <div class="text-center mb-12">
            <h1 class="text-5xl font-bold text-gray-900 mb-4">
                <i class="fas fa-shopping-cart text-blue-600 mr-4"></i>Trepo
            </h1>
            <p class="text-xl text-gray-600 max-w-4xl mx-auto leading-relaxed">
                Trepo is a modern, scalable e-commerce backend built with ASP.NET Core (.NET 8, C# 12). It provides robust APIs for user management, product catalog, shopping cart, order processing, and comprehensive role-based access control.
                The architecture is designed for maintainability, security, and extensibility, following clean coding principles and industry best practices.
            </p>
        </div>

        <!-- Tech Stack -->
        <div class="bg-white rounded-lg shadow-lg p-8 mb-8">
            <h2 class="text-3xl font-bold text-gray-900 mb-6 flex items-center">
                <i class="fas fa-layer-group text-purple-600 mr-3"></i>Tech Stack
            </h2>
            <div class="grid grid-cols-1 md:grid-cols-2 gap-6">
                <div>
                    <div class="flex items-center mb-3">
                        <i class="fas fa-server text-blue-500 mr-2"></i>
                        <span class="font-semibold">Backend:</span> ASP.NET Core (.NET 8, C# 12)
                    </div>
                    <div class="flex items-center mb-3">
                        <i class="fas fa-database text-green-500 mr-2"></i>
                        <span class="font-semibold">ORM:</span> Entity Framework Core
                    </div>
                    <div class="flex items-center mb-3">
                        <i class="fas fa-table text-red-500 mr-2"></i>
                        <span class="font-semibold">Database:</span> SQL Server
                    </div>
                </div>
                <div>
                    <div class="flex items-center mb-3">
                        <i class="fas fa-exchange-alt text-yellow-500 mr-2"></i>
                        <span class="font-semibold">API:</span> RESTful (JSON)
                    </div>
                    <div class="flex items-center mb-3">
                        <i class="fas fa-shield-alt text-indigo-500 mr-2"></i>
                        <span class="font-semibold">Authentication:</span> JWT + Identity Framework
                    </div>
                    <div class="flex items-center mb-3">
                        <i class="fas fa-check-circle text-pink-500 mr-2"></i>
                        <span class="font-semibold">Validation:</span> Data Annotations
                    </div>
                </div>
            </div>
        </div>

        <!-- Features -->
        <div class="bg-white rounded-lg shadow-lg p-8 mb-8">
            <h2 class="text-3xl font-bold text-gray-900 mb-6 flex items-center">
                <i class="fas fa-star text-yellow-500 mr-3"></i>Features
            </h2>
            <div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
                <div class="feature-card bg-gradient-to-br from-blue-50 to-blue-100 p-6 rounded-lg border border-blue-200">
                    <i class="fas fa-users text-blue-600 text-2xl mb-3"></i>
                    <h3 class="font-semibold text-gray-900 mb-2">User Management</h3>
                    <p class="text-gray-600 text-sm">Registration, authentication, profile management with role-based access control</p>
                </div>
                <div class="feature-card bg-gradient-to-br from-green-50 to-green-100 p-6 rounded-lg border border-green-200">
                    <i class="fas fa-boxes text-green-600 text-2xl mb-3"></i>
                    <h3 class="font-semibold text-gray-900 mb-2">Product Catalog</h3>
                    <p class="text-gray-600 text-sm">Comprehensive product management with categories, search, and filtering</p>
                </div>
                <div class="feature-card bg-gradient-to-br from-purple-50 to-purple-100 p-6 rounded-lg border border-purple-200">
                    <i class="fas fa-shopping-cart text-purple-600 text-2xl mb-3"></i>
                    <h3 class="font-semibrand text-gray-900 mb-2">Shopping Cart</h3>
                    <p class="text-gray-600 text-sm">Dynamic cart management with checkout and order processing</p>
                </div>
                <div class="feature-card bg-gradient-to-br from-red-50 to-red-100 p-6 rounded-lg border border-red-200">
                    <i class="fas fa-receipt text-red-600 text-2xl mb-3"></i>
                    <h3 class="font-semibold text-gray-900 mb-2">Order Management</h3>
                    <p class="text-gray-600 text-sm">Complete order lifecycle with status tracking and cancellation</p>
                </div>
                <div class="feature-card bg-gradient-to-br from-yellow-50 to-yellow-100 p-6 rounded-lg border border-yellow-200">
                    <i class="fas fa-tags text-yellow-600 text-2xl mb-3"></i>
                    <h3 class="font-semibold text-gray-900 mb-2">Category System</h3>
                    <p class="text-gray-600 text-sm">Hierarchical product categorization with cascade operations</p>
                </div>
                <div class="feature-card bg-gradient-to-br from-indigo-50 to-indigo-100 p-6 rounded-lg border border-indigo-200">
                    <i class="fas fa-user-shield text-indigo-600 text-2xl mb-3"></i>
                    <h3 class="font-semibold text-gray-900 mb-2">Role-Based Security</h3>
                    <p class="text-gray-600 text-sm">Admin, Vendor, and Customer roles with appropriate permissions</p>
                </div>
            </div>
        </div>

        <!-- API Endpoints -->
        <div class="bg-white rounded-lg shadow-lg p-8 mb-8">
            <h2 class="text-3xl font-bold text-gray-900 mb-6 flex items-center">
                <i class="fas fa-code text-blue-600 mr-3"></i>API Endpoints
            </h2>

            <!-- Account Management -->
            <div class="mb-8">
                <h3 class="text-2xl font-semibold text-gray-800 mb-4 flex items-center">
                    <i class="fas fa-user-cog text-blue-500 mr-2"></i>Account Management
                </h3>
                <div class="overflow-x-auto">
                    <table class="endpoint-table w-full">
                        <thead>
                            <tr>
                                <th>Endpoint</th>
                                <th>Method</th>
                                <th>Description</th>
                                <th>Authorization</th>
                                <th>Request Body</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td class="font-mono text-sm">/api/Account/register</td>
                                <td><span class="px-2 py-1 rounded text-xs font-semibold method-post">POST</span></td>
                                <td>Register new user</td>
                                <td class="text-green-600">Public</td>
                                <td class="font-mono text-xs">RegisterDTO</td>
                            </tr>
                            <tr>
                                <td class="font-mono text-sm">/api/Account/login</td>
                                <td><span class="px-2 py-1 rounded text-xs font-semibold method-post">POST</span></td>
                                <td>User authentication</td>
                                <td class="text-green-600">Public</td>
                                <td class="font-mono text-xs">LoginDto</td>
                            </tr>
                            <tr>
                                <td class="font-mono text-sm">/api/Account/Logout</td>
                                <td><span class="px-2 py-1 rounded text-xs font-semibold method-post">POST</span></td>
                                <td>User logout</td>
                                <td class="text-yellow-600">Authenticated</td>
                                <td>-</td>
                            </tr>
                            <tr>
                                <td class="font-mono text-sm">/api/Account/users</td>
                                <td><span class="px-2 py-1 rounded text-xs font-semibold method-get">GET</span></td>
                                <td>Get all users (paginated)</td>
                                <td class="text-red-600">Admin</td>
                                <td>Query params</td>
                            </tr>
                            <tr>
                                <td class="font-mono text-sm">/api/Account/users/{email}</td>
                                <td><span class="px-2 py-1 rounded text-xs font-semibold method-get">GET</span></td>
                                <td>Get user by email</td>
                                <td class="text-red-600">Admin</td>
                                <td>-</td>
                            </tr>
                            <tr>
                                <td class="font-mono text-sm">/api/Account/Updateusers/{email}</td>
                                <td><span class="px-2 py-1 rounded text-xs font-semibold method-put">PUT</span></td>
                                <td>Update user details</td>
                                <td class="text-red-600">Admin</td>
                                <td class="font-mono text-xs">UpdateUserDTO</td>
                            </tr>
                            <tr>
                                <td class="font-mono text-sm">/api/Account/roles/addTOUser</td>
                                <td><span class="px-2 py-1 rounded text-xs font-semibold method-post">POST</span></td>
                                <td>Add role to user</td>
                                <td class="text-red-600">Admin</td>
                                <td class="font-mono text-xs">addRoleDto</td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>

            <!-- Product Management -->
            <div class="mb-8">
                <h3 class="text-2xl font-semibold text-gray-800 mb-4 flex items-center">
                    <i class="fas fa-box text-green-500 mr-2"></i>Product Management
                </h3>
                <div class="overflow-x-auto">
                    <table class="endpoint-table w-full">
                        <thead>
                            <tr>
                                <th>Endpoint</th>
                                <th>Method</th>
                                <th>Description</th>
                                <th>Authorization</th>
                                <th>Request Body</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td class="font-mono text-sm">/api/Product/AddProduct</td>
                                <td><span class="px-2 py-1 rounded text-xs font-semibold method-post">POST</span></td>
                                <td>Create new product</td>
                                <td class="text-purple-600">Admin/Vendor</td>
                                <td class="font-mono text-xs">ProductCreateDto</td>
                            </tr>
                            <tr>
                                <td class="font-mono text-sm">/api/Product/GetAllProducts</td>
                                <td><span class="px-2 py-1 rounded text-xs font-semibold method-get">GET</span></td>
                                <td>Get all products (paginated)</td>
                                <td class="text-green-600">Public</td>
                                <td>Query params</td>
                            </tr>
                            <tr>
                                <td class="font-mono text-sm">/api/Product/GetProductById/{id}</td>
                                <td><span class="px-2 py-1 rounded text-xs font-semibold method-get">GET</span></td>
                                <td>Get product by ID</td>
                                <td class="text-green-600">Public</td>
                                <td>-</td>
                            </tr>
                            <tr>
                                <td class="font-mono text-sm">/api/Product/SearchProducts</td>
                                <td><span class="px-2 py-1 rounded text-xs font-semibold method-get">GET</span></td>
                                <td>Search products</td>
                                <td class="text-green-600">Public</td>
                                <td>Query params</td>
                            </tr>
                            <tr>
                                <td class="font-mono text-sm">/api/Product/UpdateProduct/{id}</td>
                                <td><span class="px-2 py-1 rounded text-xs font-semibold method-put">PUT</span></td>
                                <td>Update product</td>
                                <td class="text-purple-600">Admin/Vendor</td>
                                <td class="font-mono text-xs">ProductUpdateDto</td>
                            </tr>
                            <tr>
                                <td class="font-mono text-sm">/api/Product/DeleteProduct/{id}</td>
                                <td><span class="px-2 py-1 rounded text-xs font-semibold method-delete">DELETE</span></td>
                                <td>Delete product</td>
                                <td class="text-purple-600">Admin/Vendor</td>
                                <td>-</td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>

            <!-- Category Management -->
            <div class="mb-8">
                <h3 class="text-2xl font-semibold text-gray-800 mb-4 flex items-center">
                    <i class="fas fa-tags text-yellow-500 mr-2"></i>Category Management
                </h3>
                <div class="overflow-x-auto">
                    <table class="endpoint-table w-full">
                        <thead>
                            <tr>
                                <th>Endpoint</th>
                                <th>Method</th>
                                <th>Description</th>
                                <th>Authorization</th>
                                <th>Request Body</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td class="font-mono text-sm">/api/Category/AddCategory</td>
                                <td><span class="px-2 py-1 rounded text-xs font-semibold method-post">POST</span></td>
                                <td>Create new category</td>
                                <td class="text-red-600">Admin</td>
                                <td class="font-mono text-xs">CreateCategoryDto</td>
                            </tr>
                            <tr>
                                <td class="font-mono text-sm">/api/Category/GetAllCategories</td>
                                <td><span class="px-2 py-1 rounded text-xs font-semibold method-get">GET</span></td>
                                <td>Get all categories</td>
                                <td class="text-green-600">Public</td>
                                <td>-</td>
                            </tr>
                            <tr>
                                <td class="font-mono text-sm">/api/Category/GetCategoryById/{id}</td>
                                <td><span class="px-2 py-1 rounded text-xs font-semibold method-get">GET</span></td>
                                <td>Get category by ID</td>
                                <td class="text-green-600">Public</td>
                                <td>-</td>
                            </tr>
                            <tr>
                                <td class="font-mono text-sm">/api/Category/UpdateCategory/{id}</td>
                                <td><span class="px-2 py-1 rounded text-xs font-semibold method-put">PUT</span></td>
                                <td>Update category</td>
                                <td class="text-red-600">Admin</td>
                                <td class="font-mono text-xs">UpdateCategoryDto</td>
                            </tr>
                            <tr>
                                <td class="font-mono text-sm">/api/Category/DeleteCategory/{id}</td>
                                <td><span class="px-2 py-1 rounded text-xs font-semibold method-delete">DELETE</span></td>
                                <td>Delete category</td>
                                <td class="text-red-600">Admin</td>
                                <td>-</td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>

            <!-- Cart & Checkout -->
            <div class="mb-8">
                <h3 class="text-2xl font-semibold text-gray-800 mb-4 flex items-center">
                    <i class="fas fa-shopping-cart text-purple-500 mr-2"></i>Cart & Checkout
                </h3>
                <div class="overflow-x-auto">
                    <table class="endpoint-table w-full">
                        <thead>
                            <tr>
                                <th>Endpoint</th>
                                <th>Method</th>
                                <th>Description</th>
                                <th>Authorization</th>
                                <th>Request Body</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td class="font-mono text-sm">/api/Cart/AddToCart</td>
                                <td><span class="px-2 py-1 rounded text-xs font-semibold method-post">POST</span></td>
                                <td>Add item to cart</td>
                                <td class="text-blue-600">Customer</td>
                                <td class="font-mono text-xs">AddtocartDto</td>
                            </tr>
                            <tr>
                                <td class="font-mono text-sm">/api/Cart/GetCart/{username}</td>
                                <td><span class="px-2 py-1 rounded text-xs font-semibold method-get">GET</span></td>
                                <td>Get user's cart</td>
                                <td class="text-blue-600">Customer</td>
                                <td>-</td>
                            </tr>
                            <tr>
                                <td class="font-mono text-sm">/api/Cart/UpdateItem</td>
                                <td><span class="px-2 py-1 rounded text-xs font-semibold method-put">PUT</span></td>
                                <td>Update cart item</td>
                                <td class="text-blue-600">Customer</td>
                                <td class="font-mono text-xs">UpdateCartitemDto</td>
                            </tr>
                            <tr>
                                <td class="font-mono text-sm">/api/Cart/RemoveItem</td>
                                <td><span class="px-2 py-1 rounded text-xs font-semibold method-delete">DELETE</span></td>
                                <td>Remove cart item</td>
                                <td class="text-blue-600">Customer</td>
                                <td class="font-mono text-xs">RemoveCartItemDto</td>
                            </tr>
                            <tr>
                                <td class="font-mono text-sm">/api/Cart/Checkout</td>
                                <td><span class="px-2 py-1 rounded text-xs font-semibold method-post">POST</span></td>
                                <td>Process checkout</td>
                                <td class="text-blue-600">Customer</td>
                                <td class="font-mono text-xs">CheckoutRequestDTO</td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>

            <!-- Order Management -->
            <div class="mb-8">
                <h3 class="text-2xl font-semibold text-gray-800 mb-4 flex items-center">
                    <i class="fas fa-clipboard-list text-indigo-500 mr-2"></i>Order Management
                </h3>
                <div class="overflow-x-auto">
                    <table class="endpoint-table w-full">
                        <thead>
                            <tr>
                                <th>Endpoint</th>
                                <th>Method</th>
                                <th>Description</th>
                                <th>Authorization</th>
                                <th>Request Body</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td class="font-mono text-sm">/api/Order/CreateOrder</td>
                                <td><span class="px-2 py-1 rounded text-xs font-semibold method-post">POST</span></td>
                                <td>Create new order</td>
                                <td class="text-blue-600">Customer</td>
                                <td class="font-mono text-xs">CreateOrderDto</td>
                            </tr>
                            <tr>
                                <td class="font-mono text-sm">/api/Order/GetOrderById/{orderId}</td>
                                <td><span class="px-2 py-1 rounded text-xs font-semibold method-get">GET</span></td>
                                <td>Get order by ID</td>
                                <td class="text-purple-600">Admin/Vendor</td>
                                <td>-</td>
                            </tr>
                            <tr>
                                <td class="font-mono text-sm">/api/Order/GetAllOrders</td>
                                <td><span class="px-2 py-1 rounded text-xs font-semibold method-get">GET</span></td>
                                <td>Get all orders (paginated)</td>
                                <td class="text-purple-600">Admin/Vendor</td>
                                <td>Query params</td>
                            </tr>
                            <tr>
                                <td class="font-mono text-sm">/api/Order/GetOrdersByUserName/{userName}</td>
                                <td><span class="px-2 py-1 rounded text-xs font-semibold method-get">GET</span></td>
                                <td>Get user's orders</td>
                                <td class="text-blue-600">Customer</td>
                                <td>-</td>
                            </tr>
                            <tr>
                                <td class="font-mono text-sm">/api/Order/UpdateOrderStatus/{orderId}</td>
                                <td><span class="px-2 py-1 rounded text-xs font-semibold method-put">PUT</span></td>
                                <td>Update order status</td>
                                <td class="text-purple-600">Admin/Vendor</td>
                                <td class="font-mono text-xs">OrderStatus</td>
                            </tr>
                            <tr>
                                <td class="font-mono text-sm">/api/Order/CancelOrder/{orderId}</td>
                                <td><span class="px-2 py-1 rounded text-xs font-semibold method-post">POST</span></td>
                                <td>Cancel order</td>
                                <td class="text-blue-600">Customer</td>
                                <td>-</td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>

        <!-- DTOs -->
        <div class="bg-white rounded-lg shadow-lg p-8 mb-8">
            <h2 class="text-3xl font-bold text-gray-900 mb-6 flex items-center">
                <i class="fas fa-database text-green-600 mr-3"></i>Data Transfer Objects (DTOs)
            </h2>
            <div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
                <div class="bg-gray-50 p-4 rounded-lg border">
                    <h3 class="font-semibold text-gray-900 mb-2 flex items-center">
                        <i class="fas fa-user-plus text-blue-500 mr-2"></i>RegisterDTO
                    </h3>
                    <ul class="text-sm text-gray-600 space-y-1">
                        <li>• UserName</li>
                        <li>• Email</li>
                        <li>• PhoneNumber</li>
                        <li>• Password</li>
                    </ul>
                </div>
                <div class="bg-gray-50 p-4 rounded-lg border">
                    <h3 class="font-semibold text-gray-900 mb-2 flex items-center">
                        <i class="fas fa-sign-in-alt text-green-500 mr-2"></i>LoginDto
                    </h3>
                    <ul class="text-sm text-gray-600 space-y-1">
                        <li>• UserName</li>
                        <li>• Password</li>
                    </ul>
                </div>
                <div class="bg-gray-50 p-4 rounded-lg border">
                    <h3 class="font-semibold text-gray-900 mb-2 flex items-center">
                        <i class="fas fa-box text-purple-500 mr-2"></i>ProductCreateDto
                    </h3>
                    <ul class="text-sm text-gray-600 space-y-1">
                        <li>• Name</li>
                        <li>• Description</li>
                        <li>• Price</li>
                        <li>• CategoryId</li>
                        <li>• Stock</li>
                    </ul>
                </div>
                <div class="bg-gray-50 p-4 rounded-lg border">
                    <h3 class="font-semibold text-gray-900 mb-2 flex items-center">
                        <i class="fas fa-shopping-cart text-orange-500 mr-2"></i>AddtocartDto
                    </h3>
                    <ul class="text-sm text-gray-600 space-y-1">
                        <li>• ProductId</li>
                        <li>• Quantity</li>
                        <li>• Username</li>
                    </ul>
                </div>
                <div class="bg-gray-50 p-4 rounded-lg border">
                    <h3 class="font-semibold text-gray-900 mb-2 flex items-center">
                        <i class="fas fa-receipt text-indigo-500 mr-2"></i>CreateOrderDto
                    </h3>
                    <ul class="text-sm text-gray-600 space-y-1">
                        <li>• CustomerUsername</li>
                        <li>• ShippingAddress</li>
                        <li>• PaymentMethod</li>
                        <li>• OrderItems</li>
                    </ul>
                </div>
                <div class="bg-gray-50 p-4 rounded-lg border">
                    <h3 class="font-semibold text-gray-900 mb-2 flex items-center">
                        <i class="fas fa-tags text-yellow-500 mr-2"></i>CreateCategoryDto
                    </h3>
                    <ul class="text-sm text-gray-600 space-y-1">
                        <li>• Name</li>
                        <li>• Description</li>
                        <li>• ParentCategoryId</li>
                    </ul>
                </div>
            </div>
        </div>

        <!-- API Response Format -->
        <div class="bg-white rounded-lg shadow-lg p-8 mb-8">
            <h2 class="text-3xl font-bold text-gray-900 mb-6 flex items-center">
                <i class="fas fa-code-branch text-blue-600 mr-3"></i>Standardized API Response
            </h2>
            <div class="code-block">
                <pre class="text-green-400 text-sm">
<span class="text-gray-400">{</span>
  <span class="text-blue-400">"statusCode"</span>: <span class="text-yellow-400">200</span>,
  <span class="text-blue-400">"success"</span>: <span class="text-yellow-400">true</span>,
  <span class="text-blue-400">"data"</span>: <span class="text-gray-400">{}</span>,
  <span class="text-blue-400">"message"</span>: <span class="text-green-400">"Operation completed successfully"</span>,
  <span class="text-blue-400">"errors"</span>: <span class="text-gray-400">[]</span>
<span class="text-gray-400">}</span>
                </pre>
            </div>
        </div>

        <!-- Setup & Installation -->
        <div class="bg-white rounded-lg shadow-lg p-8 mb-8">
            <h2 class="text-3xl font-bold text-gray-900 mb-6 flex items-center">
                <i class="fas fa-cogs text-purple-600 mr-3"></i>Setup & Installation
            </h2>
            <div class="space-y-6">
                <div class="flex items-start">
                    <div class="bg-blue-100 rounded-full p-2 mr-4 mt-1">
                        <span class="text-blue-600 font-bold">1</span>
                    </div>
                    <div>
                        <h3 class="font-semibold text-gray-900 mb-2">Clone Repository</h3>
                        <div class="code-block">
                            <pre class="text-green-400 text-sm">git clone https://github.com/yourusername/Trepo.git</pre>
                        </div>
                    </div>
                </div>
                
                <div class="flex items-start">
                    <div class="bg-blue-100 rounded-full p-2 mr-4 mt-1">
                        <span class="text-blue-600 font-bold">2</span>
                    </div>
                    <div>
                        <h3 class="font-semibold text-gray-900 mb-2">Update Connection String</h3>
                        <p class="text-gray-600 mb-2">Update the connection string in <code class="bg-gray-200 px-2 py-1 rounded">appsettings.json</code></p>
                        <div class="code-block">
                            <pre class="text-green-400 text-sm">
<span class="text-gray-400">{</span>
  <span class="text-blue-400">"ConnectionStrings"</span>: <span class="text-gray-400">{</span>
    <span class="text-blue-400">"DefaultConnection"</span>: <span class="text-green-400">"Server=YOUR_SERVER;Database=TrepoDB;Trusted_Connection=true;TrustServerCertificate=true;"</span>
  <span class="text-gray-400">}</span>
<span class="text-gray-400">}</span>
                            </pre>
                        </div>
                    </div>
                </div>

                <div class="flex items-start">
                    <div class="bg-blue-100 rounded-full p-2 mr-4 mt-1">
                        <span class="text-blue-600 font-bold">3</span>
                    </div>
                    <div>
                        <h3 class="font-semibold text-gray-900 mb-2">Restore Dependencies</h3>
                        <div class="code-block">
                            <pre class="text-green-400 text-sm">dotnet restore</pre>
                        </div>
                    </div>
                </div>

                <div class="flex items-start">
                    <div class="bg-blue-100 rounded-full p-2 mr-4 mt-1">
                        <span class="text-blue-600 font-bold">4</span>
                    </div>
                    <div>
                        <h3 class="font-semibold text-gray-900 mb-2">Apply Migrations</h3>
                        <div class="code-block">
                            <pre class="text-green-400 text-sm">dotnet ef database update</pre>
                        </div>
                    </div>
                </div>

                <div class="flex items-start">
                    <div class="bg-blue-100 rounded-full p-2 mr-4 mt-1">
                        <span class="text-blue-600 font-bold">5</span>
                    </div>
                    <div>
                        <h3 class="font-semibold text-gray-900 mb-2">Run Application</h3>
                        <div class="code-block">
                            <pre class="text-green-400 text-sm">dotnet run</pre>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- Architecture -->
        <div class="bg-white rounded-lg shadow-lg p-8 mb-8">
            <h2 class="text-3xl font-bold text-gray-900 mb-6 flex items-center">
                <i class="fas fa-sitemap text-indigo-600 mr-3"></i>Project Architecture
            </h2>
            <div class="grid grid-cols-1 md:grid-cols-3 gap-6">
                <div class="bg-gradient-to-br from-blue-50 to-blue-100 p-6 rounded-lg border border-blue-200">
                    <h3 class="font-semibold text-gray-900 mb-3 flex items-center">
                        <i class="fas fa-layer-group text-blue-600 mr-2"></i>API Layer
                    </h3>
                    <ul class="text-sm text-gray-600 space-y-1">
                        <li>• Controllers</li>
                        <li>• JWT Authentication</li>
                        <li>• Authorization Policies</li>
                        <li>• Request/Response Models</li>
                    </ul>
                </div>
                <div class="bg-gradient-to-br from-green-50 to-green-100 p-6 rounded-lg border border-green-200">
                    <h3 class="font-semibold text-gray-900 mb-3 flex items-center">
                        <i class="fas fa-server text-green-600 mr-2"></i>Core Layer
                    </h3>
                    <ul class="text-sm text-gray-600 space-y-1">
                        <li>• Business Logic</li>
                        <li>• Service Interfaces</li>
                        <li>• Domain Entities</li>
                        <li>• DTOs</li>
                    </ul>
                </div>
                <div class="bg-gradient-to-br from-purple-50 to-purple-100 p-6 rounded-lg border border-purple-200">
                    <h3 class="font-semibold text-gray-900 mb-3 flex items-center">
                        <i class="fas fa-database text-purple-600 mr-2"></i>Infrastructure Layer</h3>
                    <ul class="text-sm text-gray-600 space-y-1">
                        <li>• Entity Framework</li>
                        <li>• Repository Pattern</li>
                        <li>• Database Context</li>
                        <li>• Data Access</li>
                    </ul>
                </div>
            </div>
        </div>

        <!-- Contributing -->
        <div class="bg-white rounded-lg shadow-lg p-8 mb-8">
            <h2 class="text-3xl font-bold text-gray-900 mb-6 flex items-center">
                <i class="fas fa-hands-helping text-orange-600 mr-3"></i>Contributing
            </h2>
            <div class="bg-orange-50 border border-orange-200 rounded-lg p-6">
                <p class="text-gray-700 mb-4">
                    Contributions are welcome! We follow a standard GitHub workflow for contributions.
                </p>
                <div class="flex flex-wrap gap-2">
                    <span class="bg-orange-100 text-orange-800 px-3 py-1 rounded-full text-sm">1. Fork the repository</span>
                    <span class="bg-orange-100 text-orange-800 px-3 py-1 rounded-full text-sm">2. Create a feature branch</span>
                    <span class="bg-orange-100 text-orange-800 px-3 py-1 rounded-full text-sm">3. Make your changes</span>
                    <span class="bg-orange-100 text-orange-800 px-3 py-1 rounded-full text-sm">4. Submit a pull request</span>
                </div>
            </div>
        </div>

        <!-- Security Features -->
        <div class="bg-white rounded-lg shadow-lg p-8 mb-8">
            <h2 class="text-3xl font-bold text-gray-900 mb-6 flex items-center">
                <i class="fas fa-shield-alt text-red-600 mr-3"></i>Security Features
            </h2>
            <div class="grid grid-cols-1 md:grid-cols-2 gap-6">
                <div class="space-y-4">
                    <div class="flex items-center">
                        <i class="fas fa-key text-blue-500 mr-3"></i>
                        <span class="font-semibold">JWT Token Authentication</span>
                    </div>
                    <div class="flex items-center">
                        <i class="fas fa-user-shield text-green-500 mr-3"></i>
                        <span class="font-semibold">Role-Based Authorization</span>
                    </div>
                    <div class="flex items-center">
                        <i class="fas fa-lock text-red-500 mr-3"></i>
                        <span class="font-semibold">Secure Password Hashing</span>
                    </div>
                </div>
                <div class="space-y-4">
                    <div class="flex items-center">
                        <i class="fas fa-check-circle text-purple-500 mr-3"></i>
                        <span class="font-semibold">Input Validation</span>
                    </div>
                    <div class="flex items-center">
                        <i class="fas fa-database text-yellow-500 mr-3"></i>
                        <span class="font-semibold">SQL Injection Protection</span>
                    </div>
                    <div class="flex items-center">
                        <i class="fas fa-eye-slash text-indigo-500 mr-3"></i>
                        <span class="font-semibold">Data Encryption</span>
                    </div>
                </div>
            </div>
        </div>

        <!-- Footer -->
        <div class="text-center py-8 border-t border-gray-200">
            <p class="text-gray-600 mb-4">
                <i class="fas fa-code text-blue-500 mr-2"></i>
                Built with passion using ASP.NET Core and modern development practices
            </p>
            <div class="flex justify-center space-x-4 text-sm text-gray-500">
                <span>🚀 Scalable Architecture</span>
                <span>•</span>
                <span>🔒 Enterprise Security</span>
                <span>•</span>
                <span>📈 Performance Optimized</span>
                <span>•</span>
                <span>🛡️ Production Ready</span>
            </div>
        </div>
    </div>
</body>
</html>

