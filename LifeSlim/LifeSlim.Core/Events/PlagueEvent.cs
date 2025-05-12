using LifeSlim.Core.Model;

namespace LifeSlim.Core.Events;

public class PlagueEvent(int triggerYear) : WorldEvent("Plague", triggerYear)
{
    
     public override string EventType=>"Plague";
    public override void Apply(World world)
    {
        throw new NotImplementedException();
    }
}