using LifeSlim.Core.Interface;
using LifeSlim.Core.Model;
using LifeSlim.Core.ValueObjects;

public class FleeStrategy : IMovementStrategy
{ 
    public Task<Position> NextPosition(World world, Creature creature) 
    { 
        var dirX = creature.Position.X > world.Width / 2 ? 1 : -1; 
        var dirY = creature.Position.Y > world.Height / 2 ? 1 : -1;
        
        var nextPos = new Position(
            Math.Clamp(creature.Position.X + dirX, 0, world.Width - 1), 
            Math.Clamp(creature.Position.Y + dirY, 0, world.Height - 1)
        );

        return Task.FromResult(nextPos);
    }
}