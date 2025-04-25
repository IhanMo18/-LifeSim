using LifeSlim.Core.Entities;

namespace LifeSlim.Core.Movement;

public interface IMovementStrategy
{
    public Position NextPosition(World world,Creature creature);
}