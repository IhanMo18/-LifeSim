using LifeSlim.Core.Model;

namespace LifeSlim.Core.System;

public class EventSystem
{
    private readonly World _world;

    public EventSystem(World world)
    {
        _world = world;
    }

    public void EventApply()
    {
        _world.YearTime++;
        var eventsToTrigger = _world.ScheduledEvents
            .Where(e => e.TriggerYear == _world.YearTime)
            .ToList();

        foreach (var ev in eventsToTrigger)
        {
            ev.Apply(_world);
        }
    }
}