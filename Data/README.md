
# Data

This folder contains the database configuration and initialization logic for the Hotel Grandioso booking system.

The application uses Entity Framework Core (EF Core) Code First with SQLite.

## Files

| File | Description |
|------|-------------|
| `HotelDbContext.cs` | Defines the database entities using DbSet, configures SQLite, and manages entity relationships. |
| `DatabaseSeeder.cs` | Inserts the initial hotel rooms, room types, and spa treatments. |

EF Core migration files are stored separately in the `Migrations` folder.

## Database Configuration

The database connection string is read from the `HOTEL_DB` environment variable rather than being hardcoded in the application.

Example:

`HOTEL_DB=Data Source=C:\path\to\hotel.db`

SQLite database files are excluded from Git using `.gitignore`.

## EF Core Code First and Migrations

The database is created and managed using EF Core Code First migrations.

Commands used in Visual Studio:

- `Add-Migration InitialCreate`
- `Update-Database`

The initial migration creates six tables:

- Rooms
- RoomTypes
- Guests
- Bookings
- Treatments
- SpaBookings

EF Core manages the primary keys, foreign keys, relationships, and indexes.

The application also applies pending migrations automatically during startup using `Database.Migrate()`.

## Initial Data

The `DatabaseSeeder` class initializes the database with:

| Entity | Count |
|--------|-------|
| Room types | 3 |
| Hotel rooms | 15 |
| Spa treatments | 4 |

The seeder uses LINQ `Any()` to check whether records already exist before inserting the initial data.

This helps prevent duplicate seed data when the application restarts.

## Separation of Responsibilities

The Data layer is responsible for database configuration and initialization.

- **HotelDbContext:** Manages EF Core database configuration and entity mappings.
- **DatabaseSeeder:** Populates the database with initial hotel data.
- **HotelRepository:** Handles database read/write operations in the Services layer.
- **BookingService and SpaService:** Handle business rules separately from database configuration.
- **HotelMenu:** Handles user interaction in the UI layer.

This separation improves maintainability and follows object-oriented design principles.

## Database Technology

SQLite is used because it is lightweight and does not require a separate database server.

All database operations are performed using Entity Framework Core and LINQ, without manually written SQL queries.

The database is stored locally in a SQLite file, which is excluded from version control.
