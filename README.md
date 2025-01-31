# Auction System

## Overview

The **Auction System** is a web-based application that allows users to create and participate in online auctions. It is designed using the **ASP.NET Core MVC framework**, ensuring a scalable, maintainable, and modular architecture. The system follows a **three-tier architecture** with a clear separation of **presentation layer, business logic layer, and data access layer**.

## Features

- **User Authentication & Authorization**
  - Registration and login via **ASP.NET Identity**.
  - Role-based access control for **users and administrators**.
- **Auction Management**
  - Users can create new auctions with a **name, description, starting price, and expiration date**.
  - Auctions can be edited to update the **description** but not the core details.
  - A listing of **all active auctions**, sorted by expiration date.
  - Auction details, including **bids**, are viewable in real time.
- **Bidding System**
  - Users can place bids on auctions (excluding their own auctions).
  - Bids must be higher than the **current highest bid or starting price**.
  - Users can view **all auctions they have bid on**.
- **Auction Completion & History**
  - Users can view a list of **won auctions** once they are completed.
- **Administrator Panel**
  - Administrators can **view all users and their auctions**.
  - Ability to **remove users or auctions** when necessary.

## Technologies Used

- **Backend:** ASP.NET Core 8 (MVC, Identity, Dependency Injection)
- **Frontend:** Razor Views, HTML, CSS, JavaScript
- **Database:** Microsoft SQL Server, Entity Framework Core
- **Authentication:** ASP.NET Identity
- **Architecture:** Three-tier with Repository Pattern
- **Version Control:** Git & GitHub

## Installation & Setup

### Prerequisites

Ensure you have the following installed:

- **.NET 8 SDK**
- **SQL Server (or compatible database)**
- **Visual Studio (or preferred IDE)**
- **Git (for version control)**

### Steps to Run the Application

1. **Clone the repository:**
   ```sh
   git clone https://github.com/yourusername/auction-system.git
   cd auction-system
   ```
2. **Configure the database:**
   - Update `appsettings.json` with the correct database connection string.
   - Run migrations:
   ```sh
   dotnet ef database update
   ```
3. **Run the application:**
   ```sh
   dotnet run
   ```
4. **Access the application:**
   - Open `http://localhost:5000/` in a web browser.

## Contributing

We welcome contributions to improve the auction system. To contribute:

1. Fork the repository.
2. Create a new feature branch: `git checkout -b feature-name`
3. Commit changes and push: `git push origin feature-name`
4. Open a pull request.

## License

This project is licensed under the **MIT License**.

## Contact

For any questions or issues, please open an issue in the repository or contact the project maintainers.

---

**Note:** Ensure the `appsettings.json` file is properly configured before running the application.

n System

## Overview

The **Auction System** is a web-based application that allows users to create and participate in online auctions. It is designed using the **ASP.NET Core MVC framework**, ensuring a scalable, maintainable, and modular architecture. The system follows a **three-tier architecture** with a clear separation of **presentation layer, business logic layer, and data access layer**.

## Features

- **User Authentication & Authorization**
  - Registration and login via **ASP.NET Identity**.
  - Role-based access control for **users and administrators**.
- **Auction Management**
  - Users can create new auctions with a **name, description, starting price, and expiration date**.
  - Auctions can be edited to update the **description** but not the core details.
  - A listing of **all active auctions**, sorted by expiration date.
  - Auction details, including **bids**, are viewable in real time.
- **Bidding System**
  - Users can place bids on auctions (excluding their own auctions).
  - Bids must be higher than the **current highest bid or starting price**.
  - Users can view **all auctions they have bid on**.
- **Auction Completion & History**
  - Users can view a list of **won auctions** once they are completed.
- **Administrator Panel**
  - Administrators can **view all users and their auctions**.
  - Ability to **remove users or auctions** when necessary.

## Technologies Used

- **Backend:** ASP.NET Core 8 (MVC, Identity, Dependency Injection)
- **Frontend:** Razor Views, HTML, CSS, JavaScript
- **Database:** Microsoft SQL Server, Entity Framework Core
- **Authentication:** ASP.NET Identity
- **Architecture:** Three-tier with Repository Pattern
- **Version Control:** Git & GitHub

## Installation & Setup

### Prerequisites

Ensure you have the following installed:

- **.NET 8 SDK**
- **SQL Server (or compatible database)**
- **Visual Studio (or preferred IDE)**
- **Git (for version control)**

### Steps to Run the Application

1. **Clone the repository:**
   ```sh
   git clone https://github.com/yourusername/auction-system.git
   cd auction-system
   ```
2. **Configure the database:**
   - Update `appsettings.json` with the correct database connection string.
   - Run migrations:
   ```sh
   dotnet ef database update
   ```
3. **Run the application:**
   ```sh
   dotnet run
   ```
4. **Access the application:**
   - Open `http://localhost:5000/` in a web browser.


---

**Note:** Ensure the `appsettings.json` file is properly configured before running the application.


