
using LifeSlim.Core.Interface;
using LifeSlim.Core.Model;
using LifeSlim.Core.Mutations;

namespace LifeSlim.Core.System;

public class MutationSystem
{
    private readonly MutationStrategyFactory _strategyFactory;
    private readonly IMutationFactory _mutationFactory;
 
    public MutationSystem(MutationStrategyFactory strategyFactory, IMutationFactory mutationFactory)
    {
        _strategyFactory = strategyFactory;
        _mutationFactory = mutationFactory;
    }

    public void Mutate(Creature creature)
    {
        var mutation = _mutationFactory.CreateMutation();
        var mutationStrategy = _strategyFactory.GetMutationStrategy(mutation);
        
        mutationStrategy.Mutate(mutation,creature);
    }
}