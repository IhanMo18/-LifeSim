using LifeSlim.Core.Interface;
using LifeSlim.Core.Model;
using LifeSlim.Core.Pathfinding;
using LifeSlim.Core.ValueObjects;

namespace LifeSlim.Core.Movement;

public class HuntStrategy : IMovementStrategy
{
    private readonly IMovementStrategy _randomStrategy;

    public HuntStrategy()
    {
        _randomStrategy = new RandomMovementStrategy(); // La estrategia aleatoria si no hay presas
    }

    public Position NextPosition(World world, Creature creature)
    {
        var allPrey = VisionService.FindCreaturesByVision(world, creature, creature.Dna.Stats.Vision);

        if (allPrey.Count == 0)
        {
            Console.WriteLine("No hay presas cerca, cambiando a estrategia aleatoria");
            return _randomStrategy.NextPosition(world, creature); // Si no hay presas, movimiento aleatorio
        }

        var closestPrey = allPrey
            .OrderBy(p =>
                Math.Abs(p.Position.X - creature.Position.X) +
                Math.Abs(p.Position.Y - creature.Position.Y))
            .First();

        var predictedPos = PredictNextPosition(closestPrey);
        var dx = Math.Clamp(predictedPos.X - creature.Position.X, -1, 1);
        var dy = Math.Clamp(predictedPos.Y - creature.Position.Y, -1, 1);

        return new Position(creature.Position.X + dx, creature.Position.Y + dy);
    }

    private Position PredictNextPosition(Creature target)
    {
        return new Position(
            target.Position.X + target.CurrentDirection.X,
            target.Position.Y + target.CurrentDirection.Y
        );
    }
}