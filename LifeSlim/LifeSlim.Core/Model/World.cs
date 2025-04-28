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
            grid = new string[width, height];
            ScheduledEvents.Add(new AcidRainEvent(10));
            ScheduledEvents.Add(new AcidRainEvent(20));
        }
    
        public int Width { get; set; }
        public int Height { get; set; }
    
        public string[,] grid { get; set; }
    
        public List<Creature> Creatures { get; set; } = new();
        public int YearTime { get; set; }
        public List<WorldEvent> ScheduledEvents { get; set; } = new();
        Dictionary<(int, int), List<object>> worldMap = new Dictionary<(int, int), List<object>>();
    
    
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
       
        public  bool IsOcupied(int x, int y)
        {
            return grid[x, y] != null;
        }
        
        public bool IsPositionValid(int x, int y)
        {
            return x >= 0 && x < Width && y >= 0 && y < Height;
        }
        
        public bool IsCreatureAt(Position pos, out Creature creature)
        {
            creature = null;
            if (!IsPositionValid(pos.X, pos.Y)) return false;
    
            if (worldMap.TryGetValue((pos.X, pos.Y), out var objects))
            {
                creature = objects.OfType<Creature>().FirstOrDefault();
                return creature != null;
            }
            return false;
        }

    }