using System.IO;
using System.IO.MemoryMappedFiles;

namespace spooky;

class Program {
    static void Main(string[] args) {
        
        bool exit = false;       
        Level map = default!;

        
        if (args[0] != null) {
            map = new Level(args[0]);
        } else {
            Console.WriteLine("No level file loaded.");
        }

        map.Generate();
        Room current = map.Start;

        while (!exit) {
            Console.Clear();
            Console.WriteLine("Find the elevator!");
            Console.WriteLine("Current room: " + current.Name);
            current.Draw();

            Console.WriteLine("Please enter a direction!");
            Console.Write("Direction: ");
            string? input = Console.ReadLine();

            if (input != null) {
                current = map.Move(input, current);
            }      

            if (current.Name == "ELEVATOR") {
                exit = true;
            }
        }

        Console.WriteLine("You Win!");
        
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
        // If the direction is not null, print the respective direction.
        string northLabel = north != null ? "N" : "-";
        string southLabel = south != null ? "S" : "-";
        string eastLabel = east != null ? "E" : "|";
        string westLabel = west != null ? "W" : "|";

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

    public Room Move(string desiredDirection, Room current) {
        Room origin = current;
        switch (desiredDirection.ToUpper()) {
            case "NORTH": current = current.north; break;
            case "EAST": current = current.east; break;
            case "SOUTH": current = current.south; break;
            case "WEST": current = current.west; break;
        }

        if (current != null) {
            return current;
        } else {
            Console.WriteLine("You cannot move that way!");
            Console.ReadLine();
            return origin;
        }
    }

    public void Generate() {
        // Dequeue top line to generate room names.
        string roomNamesLine = MapString.Dequeue();
        string[] roomNames = roomNamesLine.Split(" ");

        // Add main rooms to the list.
        foreach (string roomName in roomNames) {
            Rooms.Add(new Room(roomName));
        }

        //Make sure start reference has first room.
        Start = Rooms[0];

        // Process connections between rooms.
        foreach (string line in MapString) {
            string[] words = line.Split(" > ");
            
            // Validate that the line has 3 parts: origin, direction, destination.
            if (words.Length != 3) {
                Console.WriteLine($"Invalid connection line: {line}");
                continue;
            }

            string originName = words[0];
            string direction = words[1];
            string destinationName = words[2];

            // Find origin and destination rooms
            Room? origin = Rooms.FirstOrDefault(r => r.Name == originName);
            Room? destination = Rooms.FirstOrDefault(r => r.Name == destinationName);

            if (origin == null || destination == null) {
                Console.WriteLine($"Error: Room not found (Origin: {originName}, Destination: {destinationName})");
                continue;
            }

            // Update references for possible movement direction.
            switch (direction.ToUpper()) {
                case "NORTH": origin.north = destination; break;
                case "EAST": origin.east = destination; break;
                case "SOUTH": origin.south = destination; break;
                case "WEST": origin.west = destination; break;
                default:
                    Console.WriteLine($"Invalid direction: {direction}");
                    break;
            }
        }
    }
}