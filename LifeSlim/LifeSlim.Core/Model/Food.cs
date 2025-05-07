using LifeSlim.Core.ValueObjects;

namespace LifeSlim.Core.Model;

public class Food(Position position) : MapObject(position)
{
    public void Eat(Creature creature)
    {
        // creature.Dna.Stats.Strength += 10;
        creature.Hunger -= 20;
        creature.Health += 10;
        
        Console.WriteLine("El hambre ha disminuido "+creature.Hunger+",la salud aumento "+creature.Health);
    }
}