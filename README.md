# ShopCx Inventory Manager

A .NET 6-based inventory management service for the ShopCx demo application with intentionally vulnerable endpoints. This service handles product inventory, stock levels, product information, and provides comprehensive product management capabilities for the e-commerce platform.

## Security Note

⚠️ **This is an intentionally vulnerable application for security testing purposes. Do not deploy in production or sensitive environments.**

## Overview

The Inventory Manager is a .NET 6 Web API that provides comprehensive product and inventory management capabilities for the ShopCx platform. It handles product creation, updates, searches, file uploads, and stock management using SQL Server as the backend database with intentionally vulnerable SQL injection patterns.

## Key Features

- **Product Management**: Create, update, search, and manage product catalog
- **Inventory Tracking**: Monitor stock levels and product availability
- **Product Search**: Advanced search functionality with flexible queries
- **File Upload**: Handle product images and documentation
- **SQL Server Integration**: Robust database storage and queries
- **RESTful API**: Standard HTTP endpoints for product operations
- **Swagger Documentation**: Comprehensive API documentation
- **Dynamic Query Building**: Flexible SQL query construction
- **Product Creation**: Dynamic product insertion with JSON deserialization
- **File System Operations**: Product image storage and management

## Technology Stack

- **.NET 6**: Modern web framework
- **ASP.NET Core Web API**: RESTful service framework
- **SQL Server**: Relational database for product data
- **System.Data.SqlClient**: SQL Server data access
- **Newtonsoft.Json**: JSON serialization
- **Swashbuckle**: OpenAPI/Swagger documentation
- **Entity Framework Core**: Data access framework (included)
- **JWT Bearer Authentication**: Token-based authentication (included)
