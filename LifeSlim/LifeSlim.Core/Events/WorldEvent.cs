using LifeSlim.Core.Model;

namespace LifeSlim.Core.Events;

public abstract class WorldEvent
{
    public WorldEvent(string eventName, int triggerYear)
    {
        EventName = eventName;
        TriggerYear = triggerYear;
    }
    
    public string EventName{get;set;}
    public int TriggerYear{get;set;}
    
    
    public abstract void Apply(World World);
}