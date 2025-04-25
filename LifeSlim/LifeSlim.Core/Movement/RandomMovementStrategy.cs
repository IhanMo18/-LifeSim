using LifeSlim.Core.Model;

namespace LifeSlim.Core.Movement;

public class RandomMovementStrategy :IMovementStrategy
{
    private readonly Random _random = new Random();
    
    public Position NextPosition(World world, Creature creature)
    {
        var dx = _random.Next(-1, 2);
        var dy = _random.Next(-1, 2);
        var newX = creature.Position.X + dx;
        var newY = creature.Position.Y + dy;
        
        if(newX < 0 || newX >= world.Width || newY < 0 || newY >= world.Height) return creature.Position;
        
        return new Position(newX, newY);
    }
}