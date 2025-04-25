using LifeSlim.Core.Model;

namespace LifeSlim.Core.Interface;

public interface IRaceFactory : IFactory
{
    IFactory Build();
}