using LifeSlim.Core.Factories;
using LifeSlim.Core.Interface;
using LifeSlim.Core.Model;
using LifeSlim.Core.Movement;

namespace LifeSlim.Core.System;

public class MovementSystem(MovementStrategyFactory strategyFactory)
{
    public void MoveCreature(World world, Creature creature)
    {
        world.grid[creature.Position.X ,creature.Position.Y] = "";
        var strategy = strategyFactory.GetStrategy(creature);
        var nextPosition =  strategy.NextPosition(world, creature);
       
        Console.WriteLine("Tengo proxima posicion "+nextPosition.X+" "+nextPosition.Y);
        
        if (world.IsPositionValid(nextPosition.X, nextPosition.Y))  // Validar si la nueva posición es válida antes de mover
        {
            creature.MoveTo(nextPosition);
            Console.WriteLine("Me movi de posicion con strategy "+ strategy );
            world.grid[nextPosition.X, nextPosition.Y] = creature.Id.ToString();
        }
        else
        {
            // Opcional: Podrías hacer algo si no puede moverse, como quedarse quieto o buscar otra estrategia
            // En este caso, simplemente no se mueve.
        }
    }
}