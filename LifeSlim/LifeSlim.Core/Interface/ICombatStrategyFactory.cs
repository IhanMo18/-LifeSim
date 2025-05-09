using LifeSlim.Core.Model;

namespace LifeSlim.Core.Interface;

public interface ICombatStrategyFactory
{
    ICombatStrategy GetCombatStrategy(Creature creature,List<MapObject> mapObjects);
}