# Data

This folder contains the database configuration and initialization logic for the Hotel Grandioso booking system.

The application uses Entity Framework Core (EF Core) Code First with SQLite.

## Files

| File | Description |
|---|---|
| `HotelDbContext.cs` | Configures the SQLite connection and defines the database entities using DbSet. |
| `DatabaseSeeder.cs` | Inserts the initial hotel rooms, room types, and spa treatments. |

The EF Core migration files are stored in the `Migrations` folder.

## Database Configuration

The database connection string is read from the `HOTEL_DB` environment variable, following the assignment requirements.

Example:

`HOTEL_DB=Data Source=C:\path\to\hotel.db`

The connection string is not hardcoded in the C# source code.

SQLite database files are excluded from Git using `.gitignore`.


## Database Migrations

The database was created using EF Core Code First migrations.

Commands used in Visual Studio:

- `Add-Migration InitialCreate`
- `Update-Database`

The migration creates six tables:

- Rooms
- RoomTypes
- Guests
- Bookings
- Treatments
- SpaBookings

Primary keys, foreign keys, and indexes are managed by EF Core.

## Initial Data

The `DatabaseSeeder` class inserts:

| Entity | Count |
|---|---|
| Room types | 3 |
| Hotel rooms | 15 |
| Spa treatments | 4 |

The seeder uses LINQ `Any()` to check whether initial records already exist before inserting them.

This avoids inserting duplicate initial data when the application restarts.

## Separation of Responsibilities

The Data folder handles database configuration and initialization.

Database read/write operations will be handled by repositories in the Services folder.

Booking validation, room availability, and price calculations will be handled separately in the business logic.

This keeps database access separate from the user interface and other application logic.


 **EF Core:**
 
 It is a `DbContext`-class and migrations. The connection string is read from an environment variable or a local file included in `.gitignore`, never from the code.

- **Serverdatabas:**
  
  SQLite is sufficient for the task.

  No ADO.NET. Use EF Core.

