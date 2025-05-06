using LifeSlim.Core.Interface;
using LifeSlim.Core.Model;
using LifeSlim.Core.ValueObjects;

namespace LifeSlim.Core.Movement;

public class HuntStrategy : IMovementStrategy
{
    private readonly IMovementStrategy _randomStrategy;
    private readonly IVisionService _visionService;

    // Evento o callback que se puede manejar desde afuera
    public static event Action<Creature, Creature, Position>? OnPotentialCollision;

    public HuntStrategy(IVisionService visionService)
    {
        _randomStrategy = new RandomMovementStrategy();
        _visionService = visionService;
    }

    public async Task<Position> NextPosition(World world, Creature creature)
    {
        var allPrey = (await _visionService.FindCreaturesByVision(world, creature, creature.Dna.Stats.Vision))
            .Where(c => c != creature)
            .ToList();

        if (allPrey == null || allPrey.Count == 0)
        {
            Console.WriteLine("No hay presas cerca, cambiando a estrategia aleatoria");
            return await _randomStrategy.NextPosition(world, creature); // <-- Este también debe ser async
        }

        var closestPrey = allPrey
            .OrderBy(p => Math.Abs(p.Position.X - creature.Position.X) +
                          Math.Abs(p.Position.Y - creature.Position.Y))
            .First();

        var preyNextPos = PredictNextPosition(closestPrey);
        var nextPosition = new Position(
            creature.Position.X + Math.Clamp(preyNextPos.X - creature.Position.X, -1, 1),
            creature.Position.Y + Math.Clamp(preyNextPos.Y - creature.Position.Y, -1, 1)
        );

        if (preyNextPos.Equals(nextPosition))
        {
            OnPotentialCollision?.Invoke(creature, closestPrey, nextPosition);
        }

        return nextPosition;
    }


    private static Position PredictNextPosition(Creature target)
    {
        return new Position(
            target.Position.X + target.CurrentDirection.X,
            target.Position.Y + target.CurrentDirection.Y
        );
    }
}
