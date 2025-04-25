using LifeSlim.Core.Model;

namespace LifeSlim.Core.Interface;

public interface ICreatureFactory : IFactory
{
    IFactory Build();
}