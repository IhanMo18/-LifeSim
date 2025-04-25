using LifeSlim.Core.Model;
using LifeSlim.Core.Movement;

namespace LifeSlim.Core.System;

public class MovementSystem(MovementStrategyFactory strategyFactory)
{
    
    public void Move(World world, Creature crature)
    {
        var strategy = strategyFactory.GetStrategy(crature);
        var position=strategy.NextPosition(world,crature);
        world.MoveCreature(crature,position.X,position.Y);
    }
}