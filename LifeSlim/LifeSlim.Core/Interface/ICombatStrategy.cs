using LifeSlim.Core.Model;

namespace LifeSlim.Core.Interface;

public interface ICombatStrategy
{
    void Execute(Creature self,Creature enemy);
}