using LifeSlim.Core.Model;
using LifeSlim.Core.Movement;

namespace LifeSlim.Core.System;

public class MovementSystem(MovementStrategyFactory strategyFactory)
{
    
    public void Move(World world, Creature crature)
    {
        var strategy = strategyFactory.GetStrategy(crature);
        crature.Position=strategy.NextPosition(world,crature);
    }
}