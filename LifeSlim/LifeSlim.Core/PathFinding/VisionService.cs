
using LifeSlim.Core.Interface;
using LifeSlim.Core.Model;

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

                var foundMapObject = world.GetObjectAt(x, y);
            
                // 👇 Filtrar comida consumida o en proceso de consumo
                if (foundMapObject is Food { IsConsumed: true })
                    continue;
                
                if (foundMapObject != null)
                {
                    nearObjects.Add(foundMapObject);
                }
            }
        }
        
        return nearObjects;
    }


}