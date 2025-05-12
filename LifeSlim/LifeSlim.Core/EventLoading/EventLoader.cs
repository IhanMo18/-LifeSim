using LifeSlim.Core.Events;
using LifeSlim.Core.Model;

namespace LifeSlim.Core.EventLoading;

public static class EventLoader
{
    public static void LoadDefaultEvents(World world)
    {
        world.ScheduledEvents.AddRange(new List<WorldEvent>()
        {
            new AcidRainEvent(triggerYear: 10),
            new PlagueEvent(triggerYear:24),
        });
        Console.WriteLine("Agrego eventos de mundo");
    }
}