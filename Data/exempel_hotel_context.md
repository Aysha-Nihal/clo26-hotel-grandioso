# Exempel: en databaskontext (EF Core)

Det här exemplet visar hur en `DbContext` kopplar klasser till en databas. Det behövs paketet `Microsoft.EntityFrameworkCore.Sqlite` (`dotnet add package Microsoft.EntityFrameworkCore.Sqlite`).

Först en `DbSet` som berättar för EF Core att det finns en tabell med rum:

```csharp
using Microsoft.EntityFrameworkCore;

public class HotelContext : DbContext
{
    public DbSet<Room> Rooms => Set<Room>();
}
```

Reflektion: en `DbSet<Room>` motsvarar en tabell. Varje rad är ett `Room`-objekt.

Sedan konfigurationen. Connectionstring ligger aldrig i koden, utan i en miljövariabel:

```csharp
protected override void OnConfiguring(DbContextOptionsBuilder options)
{
    // Connectionstring hämtas från miljövariabel, aldrig i koden
    string? connectionString = Environment.GetEnvironmentVariable("HOTEL_DB");

    // Vakt: stanna tidigt om den saknas
    if (string.IsNullOrEmpty(connectionString))
    {
        throw new InvalidOperationException("Miljövariabeln HOTEL_DB saknas.");
    }

    options.UseSqlite(connectionString);
}
```

Reflektion: felet syns direkt vid start, inte senare när något försöker spara. Värdet kan vara `Data Source=hotel.db`.

## Hela koden

```csharp
using Microsoft.EntityFrameworkCore;

public class HotelContext : DbContext
{
    public DbSet<Room> Rooms => Set<Room>();

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // Connectionstring hämtas från miljövariabel, aldrig i koden
        string? connectionString = Environment.GetEnvironmentVariable("HOTEL_DB");

        // Vakt: stanna tidigt om den saknas
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("Miljövariabeln HOTEL_DB saknas.");
        }

        options.UseSqlite(connectionString);
    }
}
```
