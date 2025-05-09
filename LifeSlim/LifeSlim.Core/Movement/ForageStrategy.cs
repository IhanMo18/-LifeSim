using LifeSlim.Core.Interface;
using LifeSlim.Core.Model;
using LifeSlim.Core.ValueObjects;

namespace LifeSlim.Core.Movement;

public class ForageStrategy : IMovementStrategy
{
    public ForageStrategy(Food closestFood)
    {
        this.ClosestFood = closestFood;
    }

    private Food ClosestFood { get; set; }
    
    public Task<Position> NextPosition(World world,Creature creature)
    {

        // 3. Calcular dirección hacia la comida
        var dirX = ClosestFood.Position.X.CompareTo(creature.Position.X);
        var dirY = ClosestFood.Position.Y.CompareTo(creature.Position.Y);

        return Task.FromResult(new Position(
            Math.Clamp(creature.Position.X + dirX, 0, world.Width - 1),
            Math.Clamp(creature.Position.Y + dirY, 0, world.Height - 1)
        ));
    }
}
