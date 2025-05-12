using LifeSlim.Core.Model;

namespace LifeSlim.Core.Interface;

public interface ICombatStrategyFactory
{
    ICombatStrategy GetCombatStrategy(Creature creature,Creature adjacentCreature);
}