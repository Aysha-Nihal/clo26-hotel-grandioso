# HotelGrandioso — kodmappen

Här ligger själva programmet, Hotel Grandioso. Varje mapp har en egen README som beskriver vad som ska ligga där.

| Mapp | Innehåll |
|------|----------|
| `Models/` | Classes that represent what you are handling (entities) |
| `Data/` | Database connection and context (EF Core) |
| `Services/` | Logic that connects models and data, for example, Repository |
| `UI/` | The interface: console, Blazor, or MVC |

`Program.cs` is the starting point. It should be brief and simply connect the parts.

## Tips

- Place each class in its own file, with the same name as the class.
- Keep names in Swedish in printouts and the UI, and in English in the code (classes, methods, variables).
- Comments in English, and explain the "why" rather than the "what."
