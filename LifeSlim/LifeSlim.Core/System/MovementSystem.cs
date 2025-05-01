using LifeSlim.Core.Model;
using LifeSlim.Core.Movement;

namespace LifeSlim.Core.System;

public class MovementSystem(MovementStrategyFactory strategyFactory)
{
    public void Move(World world, Creature creature)
    {
        var currentKey = $"{creature.Position.X},{creature.Position.Y}";
        world.CreaturePositions.Remove(currentKey); // Elimina la posición actual

        var strategy = strategyFactory.GetStrategy(creature);
        var nextPosition = strategy.NextPosition(world, creature);
        var newKey = $"{nextPosition.X},{nextPosition.Y}";

        // ⚠️ Previene colisiones: no moverse si la posición ya está ocupada
        if (!world.CreaturePositions.ContainsKey(newKey))
        {
            creature.Position = nextPosition;
            world.CreaturePositions[newKey] = creature.Id.ToString(); // Añade sin explotar
        }
        else
        {
            //Intentar de nuevo
        }
    }

}