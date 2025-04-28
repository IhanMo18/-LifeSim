using LifeSlim.Core.Interface;
using LifeSlim.Core.ValueObjects;

namespace LifeSlim.Core.Mutations;

public class MutationStrategyFactory
{
    
    private readonly IMutationStrategy _normalMutationStrategy = new NormalMutation();
    private readonly IMutationStrategy _weirdMutationStrategy = new WeirdMutation();

    public IMutationStrategy GetMutationStrategy(Mutation mutation)
    {
        if (mutation.Probability < 0.3)
        {
            return _weirdMutationStrategy;
        }
        
        return _normalMutationStrategy;
    }
}