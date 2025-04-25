using LifeSlim.Core.Events;

namespace LifeSlim.Core.Entities;

public class World
{
    
    public List<Race> Races { get; set; } = new();
    public int YearTime { get; set; }
    public List<WorldEvent> ScheduledEvents { get; set; } = new();
    
}