using LifeSlim.Core.Interface;
using LifeSlim.Core.Model;

namespace LifeSlim.Infrastructure.Factories;

public class Factory
{
    private readonly World _world;

    public Factory(World world)
    {
        _world = world;
    }

    public IFactory GetFactory(string factoryType,string name)
    {
        switch (factoryType)
        {
            case"creature":
                return new CreatureFactory(_world, (Race)GetFactory("race"));
            case"race":
                return new RaceFactory();
            default:
                throw new ArgumentException($"Unknown factory type: {factoryType}");
        }
    }
}

