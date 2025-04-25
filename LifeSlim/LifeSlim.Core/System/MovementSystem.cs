using LifeSlim.Core.Entities;

namespace LifeSlim.Core.Movement;

public class MovementSystem(MovementStrategyFactory strategyFactory)
{
    
    public void Move(World world, Crature crature)
    {
        var strategy = strategyFactory.GetStrategy(crature);
        crature.position=strategy.NextPosition(world,crature);
    }
}