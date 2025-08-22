# ShopCx Inventory Manager

This is the inventory management service for the intentionally vulnerable ShopCx demo application. It handles product inventory, stock levels, product information, and provides comprehensive product management capabilities for the e-commerce platform.

## Overview

The Inventory Manager is a .NET 6 Web API that provides comprehensive product and inventory management capabilities for the ShopCx platform. It handles product creation, updates, searches, file uploads, and stock management using SQL Server as the backend database.

## Key Features

- **Product Management**: Create, update, search, and manage product catalog
- **Inventory Tracking**: Monitor stock levels and product availability
- **Product Search**: Advanced search functionality with flexible queries
- **File Upload**: Handle product images and documentation
- **SQL Server Integration**: Robust database storage and queries
- **RESTful API**: Standard HTTP endpoints for product operations
- **Swagger Documentation**: Comprehensive API documentation

## Technology Stack

- **.NET 6**: Modern web framework
- **ASP.NET Core Web API**: RESTful service framework
- **SQL Server**: Relational database for product data
- **System.Data.SqlClient**: SQL Server data access
- **Newtonsoft.Json**: JSON serialization
- **Swashbuckle**: OpenAPI/Swagger documentation

## API Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/Product/search` | Search products by query |
| PUT | `/api/Product/{id}` | Update product information |
| POST | `/api/Product` | Create new product |
| POST | `/api/Product/upload` | Upload product images |

## Dependencies

### Required Services
- **SQL Server**: Required for product and inventory data
  - Default connection: `localhost`
  - Database: `ShopCx`
  - User: `sa` / Password: `YourStrong!Passw0rd`

## Build & Run

### Prerequisites
- .NET 6 SDK
- SQL Server running locally
- Database `ShopCx` created with product tables

### Local Development
```bash
# Restore dependencies
dotnet restore

# Build the project
dotnet build

# Run the service
dotnet run
```

The service will start on `https://localhost:5001` (HTTPS) or `http://localhost:5000` (HTTP).

### Docker
```bash
# Build Docker image
docker build -t shopcx-inventory-manager .

# Run container
docker run -p 80:80 \
  -e CONNECTION_STRING="Server=sqlserver;Database=ShopCx;User Id=sa;Password=YourStrong!Passw0rd;" \
  shopcx-inventory-manager
```

## Configuration

### Environment Variables
- `CONNECTION_STRING`: SQL Server connection string
- `ASPNETCORE_ENVIRONMENT`: Application environment

### Database Setup

Create the required SQL Server database and tables:
```sql
CREATE DATABASE ShopCx;
USE ShopCx;

CREATE TABLE Products (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(255) NOT NULL,
    Description NVARCHAR(MAX),
    Price DECIMAL(18,2) NOT NULL,
    StockQuantity INT NOT NULL DEFAULT 0,
    Category NVARCHAR(100),
    SKU NVARCHAR(50) UNIQUE,
    CreatedDate DATETIME2 DEFAULT GETUTCDATE(),
    ModifiedDate DATETIME2 DEFAULT GETUTCDATE()
);

CREATE INDEX IX_Products_Name ON Products(Name);
CREATE INDEX IX_Products_Category ON Products(Category);
CREATE INDEX IX_Products_SKU ON Products(SKU);
```

## API Usage Examples

### Search Products
```bash
curl -X GET "http://localhost:5000/api/Product/search?query=laptop"
```

### Create Product
```bash
curl -X POST http://localhost:5000/api/Product \
  -H "Content-Type: application/json" \
  -d '{
    "Name": "Gaming Laptop",
    "Description": "High-performance gaming laptop",
    "Price": 1299.99,
    "StockQuantity": 10,
    "Category": "Electronics"
  }'
```

### Update Product
```bash
curl -X PUT http://localhost:5000/api/Product/1 \
  -H "Content-Type: application/json" \
  -d '{
    "Name": "Updated Gaming Laptop",
    "Price": 1199.99,
    "StockQuantity": 15
  }'
```

### Upload Product Image
```bash
curl -X POST http://localhost:5000/api/Product/upload \
  -F "file=@product-image.jpg"
```

## Product Search

The search functionality supports:
- **Name matching**: Search by product name
- **Description search**: Full-text search in descriptions
- **Flexible queries**: Partial matching and wildcards
- **Case-insensitive**: Search regardless of case

## File Upload

Product image and document upload features:
- **Supported formats**: Images (JPG, PNG, GIF), Documents (PDF, DOC)
- **Upload directory**: `uploads/` (created automatically)
- **File validation**: Size and type restrictions
- **Error handling**: Comprehensive upload error management

## Health Check

The service includes a health check endpoint:
- **Endpoint**: `/health`
- **Returns**: Service status and database connectivity

## API Documentation

Swagger UI is available for API exploration:
- **Development**: Available at `/swagger`
- **Interactive testing**: Test endpoints directly from the browser
- **Schema documentation**: Complete request/response schemas

## Error Handling

Consistent error response format with appropriate HTTP status codes:
- **400 Bad Request**: Invalid input data
- **404 Not Found**: Product not found
- **500 Internal Server Error**: Database or system errors

## Database Management

### Product Operations
- **CRUD Operations**: Full create, read, update, delete support
- **Bulk Operations**: Efficient batch processing
- **Transaction Support**: Data consistency and rollback
- **Indexing**: Optimized search performance

### Inventory Tracking
- **Stock Levels**: Real-time inventory monitoring
- **Low Stock Alerts**: Automated inventory warnings
- **Stock Updates**: Batch and individual stock adjustments
- **Audit Trail**: Change tracking and history

## Security Note

⚠️ **This is an intentionally vulnerable application for security testing purposes. Do not deploy in production environments.**

### Known Vulnerabilities (Intentional)
- SQL injection in search queries
- Unrestricted file upload
- Missing input validation
- Hardcoded connection strings
- Insufficient access controls

## Recommended Checkmarx One Configuration
- Criticality: 3
- Cloud Insights: No
- Internet-facing: No
