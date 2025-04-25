using LifeSlim.Core.Interface;
using LifeSlim.Core.Model;
using LifeSlim.Core.Services;
using LifeSlim.Core.ValueObjects;

namespace LifeSlim.Infrastructure.Factories;

public class CreatureFactory : ICreatureFactory
{
    private readonly World _world;
    private readonly Race _race;

    public CreatureFactory(World world, Race race)
    {
        _world = world;
        _race = race;
    }

    public IFactory Build()
    {
        Position position = _world.GenerateFreePosition();
        Dna dna = Dna.FromBaseStats(_race.BaseStats);

        return new Creature(_race.Id, dna, position);
    }

    public Creature BuildChild(Creature creature, Creature partner)
    {
        return ReproductionService.Reproduce(_world,creature,partner);
    }
}