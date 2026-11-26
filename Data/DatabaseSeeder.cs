public static class DatabaseSeeder
{
    public static void Seed(HotelDbContext db)
    {
        // Seed room types only if they do not already exist
        if (!db.RoomTypes.Any())
        {
            db.RoomTypes.AddRange(
                new RoomType { Name = "Standard", Description = "Standard hotel room" },
                new RoomType { Name = "Luxury", Description = "Luxury hotel suite" },
                new RoomType { Name = "Bridal", Description = "Bridal suite" }
            );

            db.SaveChanges();
        }

        // Retrieve the room types before assigning rooms.
        RoomType standard = db.RoomTypes.First(r => r.Name == "Standard");
        RoomType luxury = db.RoomTypes.First(r => r.Name == "Luxury");
        RoomType bridal = db.RoomTypes.First(r => r.Name == "Bridal");

        // Prevent duplicate initial rooms when the application restarts
        if (!db.Rooms.Any())
        {
            for (int number = 101; number <= 105; number++)
            {
                db.Rooms.Add(new Room
                {
                    RoomNumber = number,
                    Floor = 1,
                    SizeM2 = 80,
                    PricePerNight = 800m,
                    RoomTypeId = standard.Id
                });
            }

            for (int number = 201; number <= 205; number++)
            {
                db.Rooms.Add(new Room
                {
                    RoomNumber = number,
                    Floor = 2,
                    SizeM2 = 100,
                    PricePerNight = 1000m,
                    RoomTypeId = standard.Id
                });
            }

            for (int number = 301; number <= 303; number++)
            {
                db.Rooms.Add(new Room
                {
                    RoomNumber = number,
                    Floor = 3,
                    SizeM2 = 120,
                    PricePerNight = 1200m,
                    RoomTypeId = standard.Id
                });
            }

            db.Rooms.Add(new Room
            {
                RoomNumber = 304,
                Floor = 3,
                SizeM2 = 180,
                PricePerNight = 2800m,
                RoomTypeId = luxury.Id
            });

            db.Rooms.Add(new Room
            {
                RoomNumber = 305,
                Floor = 3,
                SizeM2 = 220,
                PricePerNight = 3500m,
                RoomTypeId = bridal.Id
            });

            db.SaveChanges();
        }

        // Add spa treatments only once to avoid duplicates
        if (!db.Treatments.Any())
        {
            db.Treatments.AddRange(
                new Treatment
                {
                    Name = "Spa Entrance",
                    DurationMinutes = 60,
                    Price = 150m
                },
                new Treatment
                {
                    Name = "Massage",
                    DurationMinutes = 60,
                    Price = 950m
                },
                new Treatment
                {
                    Name = "Pedicure - Big Toe",
                    DurationMinutes = 30,
                    Price = 350m
                },
                new Treatment
                {
                    Name = "Pedicure - Whole Foot",
                    DurationMinutes = 60,
                    Price = 650m
                }
            );

            db.SaveChanges();
        }
    }
}
