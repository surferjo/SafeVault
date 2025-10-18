# SafeVault

SafeVault is a secure backend application for user management and data storage. It implements secure coding practices to prevent common vulnerabilities such as SQL Injection and Cross-Site Scripting (XSS). The project is built using **.NET 9**, **Entity Framework Core**, and **ASP.NET Core Web API**.

---

## Features

- User registration and authentication
- Password hashing with **BCrypt**
- Role-based access control (Admin / User)
- Input validation and sanitization
- Protection against SQL Injection and XSS attacks
- In-memory and persistent database support via **EF Core**

---

## Project Structure

SafeVaultWorkspace/
├─ Controllers/ # API controllers
├─ Data/ # DbContext and repository classes
├─ Helpers/ # Input validation and output encoding
├─ Models/ # User model
├─ Middleware/ # Custom middleware (e.g., FakeAuth)
├─ Tests/ # Unit and security tests
├─ Program.cs # Application entry point
├─ SafeVault.csproj
└─ .gitignore


---

## Getting Started

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
- [Visual Studio Code](https://code.visualstudio.com/) or another IDE
- Optional: Postman or curl for testing endpoints

### Installation

1. Clone the repository:

git clone https://github.com/YOUR_USERNAME/SafeVault.git
cd SafeVaultWorkspace

2. Restore dependencies:

dotnet restore


3. Build the project:

dotnet build


4. Run the application:

dotnet run --project SafeVault


The API will be available at https://localhost:5001 (default ASP.NET Core port).

### Running Tests

Unit and security tests are included in the SafeVault.Tests project. Run them with:

dotnet test

### Security Features

SQL Injection prevention: All database operations use parameterized queries and proper input validation.

XSS prevention: User input is sanitized before storage and HTML output is encoded.

Password security: User passwords are hashed with BCrypt and never stored in plain text.