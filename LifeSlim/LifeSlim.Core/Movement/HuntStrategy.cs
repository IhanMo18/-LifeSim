using LifeSlim.Core.Interface;
using LifeSlim.Core.Model;
using LifeSlim.Core.Pathfinding;
using LifeSlim.Core.ValueObjects;

namespace LifeSlim.Core.Movement;

public class HuntStrategy : IMovementStrategy
{
    private readonly IMovementStrategy _randomStrategy;

    // Evento o callback que se puede manejar desde afuera
    public static event Action<Creature, Creature, Position>? OnPotentialCollision;

    public HuntStrategy()
    {
        _randomStrategy = new RandomMovementStrategy();
    }

    public Position NextPosition(World world, Creature creature)
    {
        var allPrey = VisionService.FindCreaturesByVision(world, creature, creature.Dna.Stats.Vision)
            .Where(c => c != creature) // evitar auto detección
            .ToList();

        if (allPrey.Count == 0)
        {
            Console.WriteLine("No hay presas cerca, cambiando a estrategia aleatoria");
            return _randomStrategy.NextPosition(world, creature);
        }

        var closestPrey = allPrey
            .OrderBy(p =>
                Math.Abs(p.Position.X - creature.Position.X) +
                Math.Abs(p.Position.Y - creature.Position.Y))
            .First();

        var predictedPreyPos = PredictNextPosition(closestPrey);

        // Movimiento de la criatura hacia la presa
        var dx = Math.Clamp(predictedPreyPos.X - creature.Position.X, -1, 1);
        var dy = Math.Clamp(predictedPreyPos.Y - creature.Position.Y, -1, 1);
        var nextPosition = new Position(creature.Position.X + dx, creature.Position.Y + dy);

        // Detectar posible colisión
        var preyNextPos = PredictNextPosition(closestPrey);
        if (preyNextPos.Equals(nextPosition))
        {
            // Disparar evento de colisión
            OnPotentialCollision?.Invoke(creature, closestPrey, nextPosition);
        }

        return nextPosition;
    }

    private Position PredictNextPosition(Creature target)
    {
        return new Position(
            target.Position.X + target.CurrentDirection.X,
            target.Position.Y + target.CurrentDirection.Y
        );
    }
}
