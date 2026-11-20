# Exempel: en modellklass (Room)

Det här exemplet visar hur en modellklass ser ut. Den innehåller data och enkel logik, men vet inget om hur den sparas.

Först en enkel klass med egenskaper:

```csharp
public class Room
{
    public int Id { get; set; }
    public int RoomNumber { get; set; }
    public int Floor { get; set; }
    public int SizeM2 { get; set; }
    public decimal PricePerNight { get; set; }
}
```

Lägg märke till att klassen bara beskriver ett rum. Inga anrop till databasen, ingen utskrift.

Logiken om pris ligger i klassen, och vakten kommer först:

```csharp
public void ChangePrice(decimal newPrice)
{
    // Vakt: kontrollera innan vi ändrar något
    if (newPrice <= 0)
    {
        throw new ArgumentOutOfRangeException(nameof(newPrice), "Priset måste vara större än noll.");
    }

    PricePerNight = newPrice;
}
```

Reflektion: vakten gör att ett rum aldrig kan få ett ogiltigt pris. Det är ett exempel på att en klass ansvarar för sina egna regler.

## Hela koden

```csharp
public class Room
{
    public int Id { get; set; }
    public int RoomNumber { get; set; }
    public int Floor { get; set; }
    public int SizeM2 { get; set; }
    public decimal PricePerNight { get; set; }

    public void ChangePrice(decimal newPrice)
    {
        // Vakt: kontrollera innan vi ändrar något
        if (newPrice <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(newPrice), "Priset måste vara större än noll.");
        }

        PricePerNight = newPrice;
    }
}
```
