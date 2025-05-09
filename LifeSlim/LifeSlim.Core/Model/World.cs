using LifeSlim.Core.Events;
using LifeSlim.Core.System;
using LifeSlim.Core.ValueObjects;

namespace LifeSlim.Core.Model;
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
        
        // Dictionary<(int, int), List<object>> worldMap = new Dictionary<(int, int), List<object>>();
    
        public Creature? GetCreatureAt(int x, int y)
        {
            return MapObjects
                .OfType<Creature>() // 👈 Filtra solo Creatures
                .FirstOrDefault(c => c.Position.X == x && c.Position.Y == y && c.IsAlive);
        }

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
        
        
        public bool MoveCreature(Creature creature, int newX, int newY)
        {
            var currentPos = creature.Position;

            // Validaciones de mundo
            if (newX < 0 || newX >= Width || newY < 0 || newY >= Height)
                return false;

            // Actualiza grid y worldMap si aplica
            CreaturePositions[$"{currentPos.X},{currentPos.Y}"] = "";
            CreaturePositions[$"{currentPos.X},{currentPos.Y}"] = creature.Id.ToString(); //quiero guardar los id de las criaturas en las posiciones 

            var oldKey = $"{currentPos.X},{currentPos.Y}";
            // var newKey = (newX, newY);

            if (CreaturePositions.ContainsKey(oldKey))
                CreaturePositions.Remove(oldKey);

            // if (!worldMap.ContainsKey(newKey))
            //     worldMap[newKey] = new List<object>();

            // worldMap[newKey].Add(creature);

            // Actualiza posición de la criatura
            creature.Position = new Position(x:newX, y:newY);

            return true;
        }
    }