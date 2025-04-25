using LifeSlim.Core.Model;

namespace LifeSlim.Core.Events;

public class AcidRainEvent(int triggerYear) : WorldEvent("AcidRain", triggerYear)
{
    public override void Apply(World World)
    {
        throw new NotImplementedException();
    }
}