using LifeSlim.Core.Combat;
using LifeSlim.Core.Interface;
using LifeSlim.Core.Model;

namespace LifeSlim.Core.Factories;

public class CombatStrategyFactory : ICombatStrategyFactory
{
    private readonly ICombatStrategy _fightingStrategy;
    private readonly ICombatStrategy _submittingStrategy;

    public CombatStrategyFactory()
    {
        _submittingStrategy = new SubmitStrategy();
        _fightingStrategy = new FightingStrategy();
    }


    public ICombatStrategy GetCombatStrategy(Creature creature,Creature adjacentCreature)
    {
        if (creature.CanEngage(adjacentCreature) && adjacentCreature.ShouldSubmitTo(creature))
        {
            return _submittingStrategy;
        }
        
        return _fightingStrategy;
    }
}