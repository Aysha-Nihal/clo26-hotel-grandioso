
# UI

This folder contains the console-based user interface for the Hotel Grandioso booking system.

The application uses a menu-driven interface that allows users to manage hotel room reservations and spa bookings.

## HotelMenu.cs

The `HotelMenu` class handles user interaction, including displaying menus, reading input, and presenting results.

### Available Features

1. Show all hotel rooms with their types and prices.
2. Show available rooms for a selected date range.
3. Book a room for a guest.
4. Cancel an existing room reservation.
5. View all current room bookings.
6. Show available spa treatments and prices.
7. Book a spa treatment for a guest.
0. Exit the application.

## Input Validation

The console interface validates user input to prevent common errors.

Examples include:
- Checking that room numbers and treatment IDs are valid integers.
- Validating date formats using `DateTime.TryParseExact()`.
- Ensuring check-out dates are later than check-in dates.
- Preventing empty guest names.
- Confirming cancellations before deleting reservations.

Invalid input displays an appropriate message instead of terminating the application.

## Separation of Responsibilities

The UI layer is responsible only for user interaction.

- `HotelMenu.cs` handles menus, input, and output.
- `BookingService.cs` handles room booking rules and price calculations.
- `SpaService.cs` handles spa reservations.
- `HotelRepository.cs` handles database operations using Entity Framework Core.

This separation follows the Single Responsibility Principle (SRP) and keeps the console interface independent of database implementation details.

## Application Startup

`Program.cs` initializes the database, repository, and services before creating and starting `HotelMenu`.

The menu continues running until the user selects option `0` to exit.
