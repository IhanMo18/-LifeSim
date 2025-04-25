using LifeSlim.Core.Model;

namespace LifeSlim.Core.Movement.Interface;

public interface IMovementStrategy
{
    public Position NextPosition(World world,Creature creature);
}