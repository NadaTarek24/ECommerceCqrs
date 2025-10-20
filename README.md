# 🛒 ECommerce CQRS Web API

A clean **ASP.NET Core 9 Web API** that implements the **CQRS (Command Query Responsibility Segregation)** pattern.  
It manages customers, products, and orders, separating *read* (queries) and *write* (commands) operations for clarity and scalability.

---

## 🌟 Features
- 🧍 Add and view customers  
- 🛍️ Create orders with multiple products  
- 🚚 Update order status to **Delivered** (reduces stock)  
- 🗃️ Entity Framework Core (Code-First + Migrations)  
- ✅ FluentValidation for input validation  
- 🧩 MediatR for CQRS pattern  
- 🧾 Swagger UI for easy API testing  

---

## 🧱 Project Architecture

| Layer | Description |
|-------|--------------|
| **ECommerce.Domain** | Entity models (Customer, Product, Order, OrderItem). |
| **ECommerce.Application** | CQRS logic: Commands, Queries, DTOs, Handlers, Validators, and AutoMapper profiles. |
| **ECommerce.Persistence** | Database context, migrations, and seeding with EF Core. |
| **ECommerce.Api** | Entry point — exposes the RESTful API endpoints using controllers. |

---

## ⚙️ Setup & Run Locally

### 1️⃣ Clone the repo
```bash
git clone https://github.com/NadaTarek24/ECommerceCqrs.git
cd ECommerceCqrs
