using LifeSlim.Core.Interface;
using LifeSlim.Core.Model;
using LifeSlim.Core.ValueObjects;

namespace LifeSlim.Infrastructure.Factories;

public class RaceFactory : IRaceFactory
{
    public IFactory Build()
    {
        return new Race();
    }
}