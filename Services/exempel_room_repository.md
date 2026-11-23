# Exempel: ett Repository (RoomRepository)

Det här exemplet visar Repository-mönstret. Resten av programmet pratar med ett interface, och bara repositoryt vet hur rummen sparas.

Först interfacet, som beskriver vad som går att göra:

```csharp
public interface IRoomRepository
{
    List<Room> GetAll();
    void Add(Room room);
}
```

Reflektion: interfacet säger *vad*, inte *hur*. Ett annat repository kan implementera samma interface utan att resten av koden ändras.

Sedan implementationen med EF Core:

```csharp
public class RoomRepository : IRoomRepository
{
    private readonly HotelContext _context;

    public RoomRepository(HotelContext context)
    {
        _context = context;
    }

    public List<Room> GetAll()
    {
        return _context.Rooms.ToList();
    }
}
```

Reflektion: klassen tar emot sin kontext i konstruktorn. Då kan den bytas ut eller testas utan att skapa en riktig databas.

Och metoden som sparar. Här kan något gå fel, så den behöver felhantering:

```csharp
public void Add(Room room)
{
    _context.Rooms.Add(room);

    // Tänk på att lägga till Try Catch, databasen kan vara otillgänglig
    _context.SaveChanges();
}
```

## Hela koden

```csharp
public interface IRoomRepository
{
    List<Room> GetAll();
    void Add(Room room);
}

public class RoomRepository : IRoomRepository
{
    private readonly HotelContext _context;

    public RoomRepository(HotelContext context)
    {
        _context = context;
    }

    public List<Room> GetAll()
    {
        return _context.Rooms.ToList();
    }

    public void Add(Room room)
    {
        _context.Rooms.Add(room);

        // Tänk på att lägga till Try Catch, databasen kan vara otillgänglig
        _context.SaveChanges();
    }
}
```
