using LifeSlim.Core.Util;
using LifeSlim.Core.ValueObjects;

namespace LifeSlim.Core.Interface;

public interface IMutationFactory
{
    Mutation CreateMutation();
}