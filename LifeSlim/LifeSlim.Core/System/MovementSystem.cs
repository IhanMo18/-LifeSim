using LifeSlim.Core.Model;
using LifeSlim.Core.Movement;

namespace LifeSlim.Core.System;

public class MovementSystem(MovementStrategyFactory strategyFactory)
{
    public void Move(World world, Creature creature)
    {
        world.grid[creature.Position.X, creature.Position.Y] = "";
        var strategy = strategyFactory.GetStrategy(creature);
        creature.Position=strategy.NextPosition(world,creature);
        world.grid[creature.Position.X,creature.Position.Y]=creature.Id.ToString();   
    }
}