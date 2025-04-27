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
       
        private bool IsOcupied(int x, int y)
        {
            return grid[x, y] != null;
        }
        
        
        public bool MoveCreature(Creature creature, int newX, int newY)
        {
            var currentPos = creature.Position;

            // Validaciones de mundo
            if (newX < 0 || newX >= Width || newY < 0 || newY >= Height)
                return false;

            // Actualiza grid y worldMap si aplica
            grid[currentPos.X, currentPos.Y] = "";
            grid[newX, newY] = creature.Id.ToString(); //quiero guardar los id de las criaturas en las posiciones 

            var oldKey = (currentPos.X, currentPos.Y);
            var newKey = (newX, newY);

            if (worldMap.ContainsKey(oldKey))
                worldMap[oldKey].Remove(creature);

            if (!worldMap.ContainsKey(newKey))
                worldMap[newKey] = new List<object>();

            worldMap[newKey].Add(creature);

            // Actualiza posición de la criatura
            creature.Position = new Position(x:newX, y:newY);

            return true;
        }
    }