using System.Collections.Generic;
using LifeSlim.Core.Model;
using LifeSlim.Core.ValueObjects;

namespace LifeSlim.Core.Pathfinding;

public  static class VisionService
{

    public static List<Creature> FindCreaturesByVision(World world, Creature creature, int vision)
    {
        var criaturasVisibles = new List<Creature>();

        // Recorremos el área de visión
        for (var dx = -vision; dx <= vision; dx++)
        {
            for (var dy = -vision; dy <= vision; dy++)
            {
                // Ignoramos la posición central (donde está la criatura)
                if (dx == 0 && dy == 0)
                    continue;

                var x = creature.Position.X + dx;
                var y = creature.Position.Y + dy;

                // Verifica si las coordenadas están dentro de los límites del mundo
                if (x < 0 || x >= world.Width || y < 0 || y >= world.Height)
                    continue;

                var creaturePosition = new Position(x, y);
                Creature creatureFind = null;
            
                // Verifica si hay una criatura en esa posición
                world.IsCreatureAt(creaturePosition, out creatureFind);

                if (creatureFind != null)
                {
                    criaturasVisibles.Add(creatureFind);
                }
            }
        }

        return criaturasVisibles;
    }

}