using LifeSlim.Core.Model;
using LifeSlim.Core.ValueObjects;

namespace LifeSlim.Core.Interface;

public interface IMovementStrategy
{
    public Task<Position> NextPosition(World world,Creature creature);
}