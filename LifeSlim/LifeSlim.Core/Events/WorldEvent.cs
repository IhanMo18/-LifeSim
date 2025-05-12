using Newtonsoft.Json;
using JsonSubTypes;
using LifeSlim.Core.Model;

namespace LifeSlim.Core.Events;


[JsonConverter(typeof(JsonSubtypes), "EventType")]
[JsonSubtypes.KnownSubType(typeof(AcidRainEvent), "AcidRain")]
[JsonSubtypes.KnownSubType(typeof(PlagueEvent), "Plague")]
public abstract class WorldEvent
{
    public WorldEvent(string eventName, int triggerYear)
    {
        EventName = eventName;
        TriggerYear = triggerYear;
    }
    
    public string EventName{get;set;}
    public int TriggerYear{get;set;}
    
    public abstract string EventType { get; } 
    
    
    
    public abstract void Apply(World World);
}