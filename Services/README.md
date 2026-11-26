# Services

This folder contains the repository and business logic for Hotel Grandioso.

The purpose is to separate database operations from business rules, making the application easier to maintain, test, and extend.

## IHotelRepository.cs

Defines the interface for database operations involving rooms, guests, bookings, and spa treatments.

Using an interface reduces coupling and allows services to depend on an abstraction rather than directly on Entity Framework Core.

## HotelRepository.cs

Implements `IHotelRepository` using EF Core and `HotelDbContext`.

Responsibilities:
- Retrieve rooms, guests, bookings, and spa treatments.
- Check room availability using LINQ.
- Prevent overlapping room bookings using a database transaction.
- Create and cancel room reservations.
- Save spa reservations.

The Repository Pattern centralizes database access, keeping database operations separate from business logic.

## BookingService.cs

Handles the business rules for room reservations.

Responsibilities:
- Validate guests, rooms, and booking dates.
- Calculate the total booking price.
- Check room availability.
- Create and cancel bookings.

Booking rules are kept separate from the user interface to improve reusability and maintainability.

## SpaService.cs

Handles spa-related business operations.

Responsibilities:
- Retrieve available spa treatments.
- Validate guests and treatments.
- Create spa reservations.

Separating spa functionality from room booking logic follows the Single Responsibility Principle (SRP).

## Design Principles

The application demonstrates:

- **Repository Pattern:** Centralizes database operations.
- **Single Responsibility Principle (SRP):** Each class has a clear responsibility.
- **Dependency Inversion Principle (DIP):** Services depend on `IHotelRepository`.
- **Constructor Dependency Injection:** Dependencies are provided through constructors.
- **Separation of Concerns:** Database access, business logic, and user interaction are handled separately.

The console interface is implemented in `UI/HotelMenu.cs`, while `Program.cs` initializes the application.
