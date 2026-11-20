# Exempel: en konsolmeny som använder ett repository

Det här exemplet visar hur gränssnittet pratar med repositoryt, och inte med databasen direkt. Texten till användaren är på svenska.

Först kopplas delarna ihop:

```csharp
using var context = new HotelContext();
IRoomRepository repository = new RoomRepository(context);
```

Reflektion: gränssnittet känner bara till `IRoomRepository`. Det är den raden som bestämmer vilken implementation som används.

Sedan menyn, som visas i en loop tills användaren väljer att avsluta:

```csharp
bool running = true;

while (running)
{
    Console.WriteLine("1. Visa alla rum");
    Console.WriteLine("2. Lägg till rum");
    Console.WriteLine("0. Avsluta");

    string? choice = Console.ReadLine();

    if (choice == "1")
    {
        foreach (Room room in repository.GetAll())
        {
            Console.WriteLine($"Rum {room.RoomNumber}, våning {room.Floor}, {room.PricePerNight} kr/natt");
        }
    }
    else if (choice == "2")
    {
        Console.Write("Rumsnummer: ");

        if (!int.TryParse(Console.ReadLine(), out int roomNumber))
        {
            Console.WriteLine("Rumsnumret måste vara ett heltal.");
            continue;
        }

        // Resten av fälten fyller ni i själva
        repository.Add(new Room { RoomNumber = roomNumber, Floor = 1, SizeM2 = 80, PricePerNight = 800 });
        Console.WriteLine("Rummet är tillagt.");
    }
    else if (choice == "0")
    {
        running = false;
    }
    else
    {
        Console.WriteLine("Okänt val, försök igen.");
    }
}
```

Reflektion: menyn sköter bara inmatning och utskrift. Logiken ligger i repositoryt, och klassen `Room` vet ingenting om konsolen.

## Hela koden

```csharp
using var context = new HotelContext();
IRoomRepository repository = new RoomRepository(context);

bool running = true;

while (running)
{
    Console.WriteLine("1. Visa alla rum");
    Console.WriteLine("2. Lägg till rum");
    Console.WriteLine("0. Avsluta");

    string? choice = Console.ReadLine();

    if (choice == "1")
    {
        foreach (Room room in repository.GetAll())
        {
            Console.WriteLine($"Rum {room.RoomNumber}, våning {room.Floor}, {room.PricePerNight} kr/natt");
        }
    }
    else if (choice == "2")
    {
        Console.Write("Rumsnummer: ");

        if (!int.TryParse(Console.ReadLine(), out int roomNumber))
        {
            Console.WriteLine("Rumsnumret måste vara ett heltal.");
            continue;
        }

        // Resten av fälten fyller ni i själva
        repository.Add(new Room { RoomNumber = roomNumber, Floor = 1, SizeM2 = 80, PricePerNight = 800 });
        Console.WriteLine("Rummet är tillagt.");
    }
    else if (choice == "0")
    {
        running = false;
    }
    else
    {
        Console.WriteLine("Okänt val, försök igen.");
    }
}
```
