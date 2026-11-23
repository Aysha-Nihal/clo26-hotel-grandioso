// RoomType.cs :— defines the room categories
public class RoomType
{
    public int Id { get; set; }  // PF to each room category

    public string Name { get; set; } = string.Empty; // Name of the room type (Single Room, Double Room, Suite)

    public string Description { get; set; } = string.Empty;   // Additional information about the room category

    // One room type can have multiple rooms(Groups rooms by type without duplicating data)
    public List<Room> Rooms { get; set; } = new();
}

