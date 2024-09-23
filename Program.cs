using System.IO;

namespace spooky;

class Program {
    static void Main(string[] args) {
        
        Level map = default!;
        
        if (args[0] != null) {
            map = new Level(args[0]);
        } else {
            Console.WriteLine("No level file loaded.");
        }

        map.Generate();
        
    }
}

public class Room {
    
    public string Name { get; set; } = default!;
    public Room north { get; set; } = default!;
    public Room south { get; set; } = default!;
    public Room east { get; set; } = default!;
    public Room west { get; set; } = default!;

    public Room(string name) {
        Name = name;
    }

    public void Draw() {
        // If the direction is not null, print the respective direction, otherwise print empty space
        string northLabel = this.north != null ? "N" : "-";
        string southLabel = this.south != null ? "S" : "-";
        string eastLabel = this.east != null ? "E" : "|";
        string westLabel = this.west != null ? "W" : "|";

        // Draw the room with correct output for each direction
        Console.WriteLine($" ---{northLabel}---");
        Console.WriteLine($"|       |");
        Console.WriteLine($"|       |");
        Console.WriteLine($"{westLabel}       {eastLabel}");
        Console.WriteLine($"|       |");
        Console.WriteLine($"|       |");
        Console.WriteLine($" ---{southLabel}---");
    }

    public override string ToString() {
        return "Room";
    }
}

public class Level {
    public Room Start { get; set; } = default!;
    public List<Room> Rooms { get; set; } = new();
    public Queue<string> MapString { get; set; } = new();

    public Level(string levelFile) {
        try {
            var lines = File.ReadAllLines(levelFile);
            MapString = new Queue<string>(lines);  // Convert string[] to Queue<string>
        }
        catch (Exception e) {
            Console.WriteLine("Exception: " + e.Message);
        }
    }

    public void Generate() {
        string roomNamesLine = MapString.Dequeue();  
        
        string[] rooms = roomNamesLine.Split(" ");

        foreach (string roomName in rooms) {
            Rooms.Add(new Room(roomName));
        }

    }
}