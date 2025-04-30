using LifeSlim.Core.Model;
using LifeSlim.Core.ValueObjects;

namespace LifeSlim.Core.Interface;

public interface IMutationStrategy
{
    public void Mutate(Mutation mutation,Creature creature);
}