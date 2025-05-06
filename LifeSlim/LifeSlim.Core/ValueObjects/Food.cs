using LifeSlim.Core.Model;

namespace LifeSlim.Core.ValueObjects;

public class Food
{
    public Position Position { get; set; }

    public static string Eat(Creature creature)
    {
        creature.Dna.Stats.Strength += 10;
        creature.Hunger -= 20;
        creature.Health += 10;
        
        return "El hambre ha disminuido 20ptos,la salud aumento 20ptos y la fuerza 10pts";
    }
}