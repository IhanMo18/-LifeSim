using LifeSlim.Core.Interface;
using LifeSlim.Core.Model;
using LifeSlim.Core.ValueObjects;

namespace LifeSlim.Core.Movement;

public class HuntStrategy : IMovementStrategy
{
    public HuntStrategy(Creature closestPrey)
    {
        ClosestPrey = closestPrey;
    }

    public Creature ClosestPrey { get; set; }

    // Evento o callback que se puede manejar desde afuera
    public static event Action<Creature, Creature, Position>? OnPotentialCollision;

    public Task<Position> NextPosition(World world, Creature creature)
    {
        var preyNextPos = PredictNextPosition(ClosestPrey!);
        var nextPosition = new Position(
            creature.Position.X + Math.Clamp(preyNextPos.X - creature.Position.X, -1, 1),
            creature.Position.Y + Math.Clamp(preyNextPos.Y - creature.Position.Y, -1, 1)
        );

        if (preyNextPos.Equals(nextPosition))
        {
            OnPotentialCollision?.Invoke(creature, ClosestPrey, nextPosition);
        }

        return Task.FromResult(nextPosition);
    }


    private static Position PredictNextPosition(Creature target)
    {
        return new Position(
            target.Position.X + target.CurrentDirection.X,
            target.Position.Y + target.CurrentDirection.Y
        );
    }
}
