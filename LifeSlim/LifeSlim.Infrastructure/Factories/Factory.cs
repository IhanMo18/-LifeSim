using LifeSlim.Core.Interface;
using LifeSlim.Core.Model;

namespace LifeSlim.Infrastructure.Factories;

public class Factory
{
    private readonly World _world;
    private readonly Race _race;

    public Factory(World world, Race race)
    {
        _world = world;
        _race = race;
    }

    public IFactory GetFactory(string factoryType)
    {
        switch (factoryType)
        {
            case"creature":
                return new CreatureFactory(_world, _race);
            default:
                throw new ArgumentException($"Unknown factory type: {factoryType}");
        }
    }
}

