# InventoryManagementV2


## Project Overview

This project consists of several components to support the CRUD operations of products and stores:

1. **ASP.NET Core 9.0 API**:
    - Serves as the backend for managing stores and products.
    - Provides RESTful endpoints for CRUD operations.
    
2. **Blazor WebAssembly**:
    - Frontend built with Blazor WebAssembly to interact with the backend API.
    - Allows the user to manage stores and products through an interactive UI.

3. **Test Project**:
    - Includes unit tests for the API and other components of the project.
    - Tests are executed using `dotnet test`.

4. **Class Library**:
    - Contains shared models that are used by both the API and Blazor WebAssembly client.

## Tech Stack

- **Backend**: ASP.NET Core 9.0
- **Frontend**: Blazor WebAssembly
- **Database**: SQL Server (or another database provider as per configuration)
- **Testing**: xUnit for unit testing
- **Shared Models**: Class Library

## Getting Started

### Prerequisites

Make sure you have the following tools installed:

- [**.NET 9.0 SDK**](https://dotnet.microsoft.com/download/dotnet/9.0)
- [**Visual Studio 2022** or [VS Code](https://code.visualstudio.com/)] (for development)
- A database (e.g., SQL Server or SQLite) configured to store store and product information

### Clone the repository

```bash
git clone [(https://github.com/ssdody/InventoryManagementV2.git](https://github.com/ssdody/InventoryManagementV2.git)
cd InventoryManagementV2

Run the API (Backend)
cd InventoryManagement
dotnet build
dotnet run
----------------------------------------
Run Client
cd InventorymanagementBlazor
dotnet build
dotnet run

This will start the Blazor WebAssembly app, usually on https://localhost:5000
----------------------------------------
Run Tests
cd InventoryManagement.Tests
dotnet test
----------------------------------------

API Documentation
Endpoints
GET /api/stores – Get a list of all stores.
GET /api/stores/{id} – Get a store by ID.
POST /api/stores – Create a new store.
PUT /api/stores/{id} – Update an existing store.
DELETE /api/stores/{id} – Delete a store

GET /api/products – Get a list of all stores.
GET /api/products/{id} – Get a store by ID.
POST /api/products – Create a new store.
PUT /api/products/{id} – Update an existing store.
DELETE /api/products/{id} – Delete a store
