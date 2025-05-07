using LifeSlim.Core.Interface;
using LifeSlim.Core.Model;
using LifeSlim.Core.ValueObjects;

namespace LifeSlim.Application.Service;

public class VisionService : IVisionService
{
    private readonly World _world;

    public VisionService(World world)
    {
        _world = world;
    }

    public Task<List<Creature>> FindCreaturesByVision(World world, Creature creature, int vision)
    {
        var creatures = new List<Creature>();

        for (int dx = -vision; dx <= vision; dx++)
        {
            for (int dy = -vision; dy <= vision; dy++)
            {
                if (dx == 0 && dy == 0) continue; // Omitirse a sí mismo

                int x = creature.Position.X + dx;
                int y = creature.Position.Y + dy;

                if (x < 0 || x >= world.Width || y < 0 || y >= world.Height) continue;

                var found = GetCreatureInPosition(new Position(x, y));

                if (found != null)
                {
                    creatures.Add(found);
                }
            }
        }

        return Task.FromResult(creatures);
    }

    private Creature? GetCreatureInPosition(Position position)
    {
        var key = $"{position.X},{position.Y}";

        if (_world.CreaturePositions.TryGetValue(key, out var creatureId))
        {
            return _world.MapObjects.OfType<Creature>().FirstOrDefault(c => c.Id.ToString() == creatureId);
        }

        return null;
    }
}