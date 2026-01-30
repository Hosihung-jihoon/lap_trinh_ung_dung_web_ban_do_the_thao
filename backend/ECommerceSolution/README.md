# E-Commerce Web API

## 📋 Prerequisites
- .NET 8 SDK
- SQL Server 2019+
- Visual Studio 2022
- Git

## 🚀 Setup Instructions

### 1. Clone Repository
```bash
git clone https://github.com/your-username/ecommerce-api.git
cd ecommerce-api
```

### 2. Update Connection String
Mở `ECommerce.API/appsettings.json` và cập nhật:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=ECommerceDB;..."
}
```

### 3. Restore Packages
```bash
dotnet restore
```

### 4. Apply Migrations & Seed Data
```bash
cd ECommerce.API
dotnet ef database update
```
Hoặc trong Visual Studio Package Manager Console:
```
Update-Database
```

### 5. Run Project
```bash
dotnet run
```
Hoặc nhấn F5 trong Visual Studio

### 6. Access Swagger
https://localhost:7xxx/swagger

## 🔑 Default Accounts
- **Admin**: admin@ecommerce.com / Admin123!