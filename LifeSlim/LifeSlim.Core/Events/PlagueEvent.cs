using LifeSlim.Core.Entities;

namespace LifeSlim.Core.Events;

public class PlagueEvent(int triggerYear) : WorldEvent("Plague", triggerYear)
{
    public override void Apply(World world)
    {
        throw new NotImplementedException();
    }
}