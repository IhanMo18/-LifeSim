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
    
    // Dictionary<(int, int), List<GameObject>> worldMap = new Dictionary<(int, int), List<GameObject>>();
    
    
}