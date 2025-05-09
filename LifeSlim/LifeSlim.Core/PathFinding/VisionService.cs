using System.Collections.Generic;
using LifeSlim.Core.Interface;
using LifeSlim.Core.Model;
using LifeSlim.Core.ValueObjects;

namespace LifeSlim.Core.Pathfinding;

public class VisionService : IVisionService
{

    public List<MapObject?> FindNearbyMapObjects(World world, Creature creature, int vision)
    {
        var nearObjects = new List<MapObject?>();

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
                var foundMapObject = world.GetObjectAt(x, y);
                
                if (foundMapObject !=  null)
                {
                    nearObjects.Add(foundMapObject);
                }
            }
        }
        
        return nearObjects;
    }


}