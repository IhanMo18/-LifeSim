using LifeSlim.Core.Entities;

namespace LifeSlim.Core.Movement;

public class ForageStrategy : IMovementStrategy
{
    public Position NextPosition(World world,Creature creature)
    {
        // Movimiento hacia el centro (supongamos hay más recursos)
        var dirX = creature.Position.X < world.Width / 2 ? 1 : -1;
        var dirY = creature.Position.Y < world.Height / 2 ? 1 : -1;

        return new Position(
            Math.Clamp(creature.Position.X + dirX, 0, world.Width - 1),
            Math.Clamp(creature.Position.Y + dirY, 0, world.Height - 1)
        );
    }
}
