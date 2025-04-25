using LifeSlim.Core.Events;

namespace LifeSlim.Core.Model;

public class World
{
    public World(int width, int height)
    {
        Width = width;
        Height = height;
        grid = new int[width, height];
    }
    
    public int Width { get; }
    public int Height { get; }
    
    private int[,] grid;
    
    public List<Creature> Creatures { get; set; } = new();
    public int YearTime { get; set; }
    public List<WorldEvent> ScheduledEvents { get; set; } = new();
    Dictionary<(int, int), List<GameObject>> worldMap = new Dictionary<(int, int), List<GameObject>>();
    
    public bool MoveCreature(Creature creature, int newX, int newY)
    {
        var currentPos = creature.Position;

        // Validaciones de mundo
        if (newX < 0 || newX >= Width || newY < 0 || newY >= Height)
            return false;

        // Actualiza grid y worldMap si aplica
        grid[currentPos.X, currentPos.Y] = null;
        grid[newX, newY] = creature;

        var oldKey = (currentPos.X, currentPos.Y);
        var newKey = (newX, newY);

        if (worldMap.ContainsKey(oldKey))
            worldMap[oldKey].Remove(creature);

        if (!worldMap.ContainsKey(newKey))
            worldMap[newKey] = new List<GameObject>();

        worldMap[newKey].Add(creature);

        // Actualiza posición de la criatura
        creature.Position = new Position { X = newX, Y = newY };

        return true;
    }
}