using LifeSlim.Core.Interface;
using LifeSlim.Core.Model;

namespace LifeSlim.Core.Movement;

public class MovementStrategyFactory
{
    private readonly IMovementStrategy _random;
    private readonly IMovementStrategy _flee;
    private readonly IMovementStrategy _forage;
    
    public MovementStrategyFactory()
    {
        _random = new RandomMovementStrategy();
        _flee = new FleeStrategy();
        _forage = new ForageStrategy();
    }
    
    
    public IMovementStrategy GetStrategy(Creature creature)
    {
        if (creature.Health < 30)
            return _flee;

        return creature.Hunger > 70 ? _forage : _random;
    }
}