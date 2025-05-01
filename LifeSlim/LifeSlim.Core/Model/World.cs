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
        private List<object> worldMap = new List<object>();
    
    
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
        //Buscar si hay una criatura en una posición específica
        public void IsCreatureAt(Position pos, out Creature creature)
        {
            // creature = worldMap.FirstOrDefault(c => c. == pos.X && c.Position.Y == pos.Y);
            // return creature != null;
            creature = null;
        }


        public bool SetInWorld(Creature creature)
        {
            // Verificar si ya existe un objeto en la misma posición
            bool positionOccupied = worldMap.Any(obj =>
            {
                if (obj is Creature otherCreature)
                {
                    return otherCreature.Position.X == creature.Position.X && otherCreature.Position.Y == creature.Position.Y;
                }
                // Si necesitas verificar otros tipos de objetos, puedes agregarlos aquí.
                return false;
            });

            if (positionOccupied)
            {
                Console.WriteLine("Error: Ya existe un objeto en esta posición.");
                return false;  // No agregar la criatura si la posición ya está ocupada.
            }

            // Si no hay objetos en la misma posición, agregar la criatura al mundo
            worldMap.Add(creature);
            return true;
        }
    }
    