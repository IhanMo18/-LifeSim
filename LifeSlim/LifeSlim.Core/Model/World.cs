using JsonSubTypes;
using LifeSlim.Core.Events;
using LifeSlim.Core.ValueObjects;
using Newtonsoft.Json;

namespace LifeSlim.Core.Model;

[JsonConverter(typeof(JsonSubtypes), "ObjType")]
[JsonSubtypes.KnownSubType(typeof(Food), "Food")]
[JsonSubtypes.KnownSubType(typeof(Creature), "Creature")]
public class World
    {
        public World(int width, int height)
        {
            Width = width;
            Height = height;
            // grid = new string[width, height];
            ScheduledEvents.Add(new AcidRainEvent(10));
            ScheduledEvents.Add(new AcidRainEvent(20));
        }
    
        public int Width { get; set; }
        public int Height { get; set; }
    
        // public string[,] grid { get; set; }
        
        public Dictionary<string,string> CreaturePositions { get; set; } = new Dictionary<string, string>();
    
        public List<MapObject> MapObjects { get; set; } = new List<MapObject>();
        public int YearTime { get; set; }
        public List<WorldEvent> ScheduledEvents { get; set; } = new();
        
        
        public MapObject? GetObjectAt(int x, int y)
        {
            return MapObjects // 👈 Filtra solo Creatures
                .FirstOrDefault(c => c.Position.X == x && c.Position.Y == y);
        }
    
        public Position GenerateFreePosition()
        {
            var random = new Random();
        
            int x, y;
            do
            {
                x = random.Next(0,Width);
                y = random.Next(0, Height);
            } 
            while (IsOcupied(x, y));
            Console.WriteLine($"{x}-{y}");
            return new Position(x,y);
        }
       
        private bool IsOcupied(int x, int y)
        {
            return CreaturePositions.ContainsKey($"{x},{y}");
            //return grid[x, y] != null;
        }
        
    }