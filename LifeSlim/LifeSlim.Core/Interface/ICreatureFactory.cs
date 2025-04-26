using LifeSlim.Core.Model;

namespace LifeSlim.Core.Interface;

public interface ICreatureFactory
{
    Creature CreateCreature(World world, Race race);
}