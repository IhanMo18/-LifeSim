using LifeSlim.Core.Model;
using LifeSlim.Core.Movement;

namespace LifeSlim.Core.System;

public class MovementSystem(MovementStrategyFactory strategyFactory)
{
    public void Move(World world, Creature creature)
    {
        if (world.CreaturePositions.ContainsKey($"{creature.Position.X},{creature.Position.Y}"))
        {
            world.CreaturePositions.Remove($"{creature.Position.X},{creature.Position.Y}");    
        }
        var strategy = strategyFactory.GetStrategy(creature);
        var nextPosition=strategy.NextPosition(world,creature);
        creature.Position = nextPosition;
        world.CreaturePositions.Add($"{nextPosition.X},{nextPosition.Y}",creature.Id.ToString());
    }
}