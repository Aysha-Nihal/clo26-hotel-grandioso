# Models
This folder contains the entity classes used in the Hotel Grandioso booking system.

Each class represents a specific part of the hotel domain. The models contain properties, relationships, and simple business rules. Database operations and user interface logic are handled separately.


## Entity Classes

| Class | Description |
|---|---|
| `Room.cs` | Represents a hotel room with its number, floor, size, nightly price, and room type. |
| `RoomType.cs` | Defines the room categories: Standard, Luxury, and Bridal. |
| `Guest.cs` | Stores guest information required for hotel and spa reservations. |
| `Booking.cs` | Connects a guest to a room for a specific check-in and check-out period. |
| `Treatment.cs` | Defines available spa treatments, including their duration and price. |
| `SpaBooking.cs` | Connects a guest to a selected spa treatment on a specific date and time. |


## Entity Relationships
- One RoomType can have multiple Room records.
- One Room can have multiple Booking records for different periods.
- One Guest can have multiple room bookings.
- One Guest can have multiple spa bookings.
- One Treatment can be associated with multiple spa bookings.

These relationships are implemented using foreign keys and navigation properties.


## Model Responsibilities
The models follow the Single Responsibility Principle by representing the hotel's domain entities.

Simple validation logic, such as the ChangePrice() method in Room, belongs to the model.

More complex business rules, including room availability checking and prevention of overlapping bookings, will be implemented in the service layer.

Database access is handled separately using Entity Framework Core.


## GDPR
The Guest entity stores only the guest's ID and name.

Unnecessary personal information, such as hair color, height, and religion, is excluded to follow the GDPR principle of data minimisation.

A more detailed GDPR explanation is provided in the main project README.
