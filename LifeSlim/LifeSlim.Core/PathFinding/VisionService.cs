using System.Collections.Generic;
using LifeSlim.Core.Model;
using LifeSlim.Core.ValueObjects;

namespace LifeSlim.Core.Pathfinding;

public  static class VisionService
{

    public static List<Creature> FindCreaturesByVision(World world, Creature creature, int vision)
    {
        var creaturesFindList = new List<Creature>();

        for (int dx = -vision; dx <= vision; dx++)
        {
            for (int dy = -vision; dy <= vision; dy++)
            {
                // Ignora la posición central (donde está la criatura)
                if (dx == 0 && dy == 0)
                    continue;

                int x = creature.Position.X + dx;
                int y = creature.Position.Y + dy;

                // Verifica si las coordenadas están dentro de los límites del mundo
                if (x < 0 || x >= world.Width || y < 0 || y >= world.Height)
                    continue;

                // Obtener criatura u objeto en la posición actual
                var foundCreature = world.GetObjectPosition(x, y);
                if (foundCreature != null)
                {
                    creaturesFindList.Add(foundCreature);
                }

                // Si también deseas incluir objetos, descomenta y adapta esta parte:
                // var foundObject = world.GetObjectAtPosition(x, y);
                // if (foundObject != null)
                // {
                //     objetosVisibles.Add(foundObject);
                // }
            }
        }

        return creaturesFindList;
    }


}