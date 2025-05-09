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


    public ICombatStrategy GetCombatStrategy(Creature creature,List<MapObject> nearbyObjects)
    {
        foreach (var presuntEnemy in nearbyObjects.OfType<Creature>())
        {
            
                return _submittingStrategy;
            
            // if (creature.CanEngage(presuntEnemy))
            // {
            //     return _fightingStrategy;
            // }
        }

        return _fightingStrategy;
    }
}