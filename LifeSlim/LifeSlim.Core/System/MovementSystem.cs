using LifeSlim.Core.Model;
using LifeSlim.Core.Movement;

namespace LifeSlim.Core.System;

public class MovementSystem(MovementStrategyFactory strategyFactory)
{

    public void Move(World world, Creature crature)
    {
        var strategy = strategyFactory.GetStrategy(crature);
        crature.Position = strategy.NextPosition(world, crature);
        Console.WriteLine($"Moviendo criaturacon raza {crature.RaceId} a la posicion {crature.Position} aplicando" +
                          $"la estrategia {strategy}");
    }
}