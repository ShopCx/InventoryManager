# ShopCx Inventory Manager

This is the inventory management service for the ShopCx demo application. It handles product inventory, stock levels, and product information.

## Purpose
This service is designed for application security testing and education purposes. It intentionally contains various security vulnerabilities to demonstrate common issues in web applications.

## Architecture
- Written in C# (.NET Core)
- RESTful API for inventory management
- SQL Server database backend
- Entity Framework Core for data access

## Known Vulnerabilities
This service intentionally contains various security issues for educational purposes:
- SQL Injection vulnerabilities using string concatenation
- Insecure direct object references
- Missing input validation
- Hardcoded database credentials
- Outdated dependencies with known vulnerabilities
- Insecure configuration management
- Missing security headers
- Insecure file upload handling
- Path traversal vulnerabilities
- Insecure deserialization

## Setup
```bash
# Build the service
dotnet build

# Run the service
dotnet run
```

## API Documentation
API documentation is available at `/swagger/index.html` when the service is running.

## License
MIT License - See LICENSE file for details

## Security Notice
This application is intentionally vulnerable and should only be used in controlled environments for security testing and education purposes. 