using LifeSlim.Core.Interface;
using LifeSlim.Core.Model;
using LifeSlim.Core.ValueObjects;

namespace LifeSlim.Core.Factories;

public class CreatureFactory : ICreatureFactory
{
    public Creature CreateCreature(World world, Race race)
    {
        var spawnPosition = world.GenerateFreePosition();
        var dna = Dna.FromBaseStats(race.BaseStats);
        
        return new Creature(race.Id, dna, spawnPosition);
    }
}