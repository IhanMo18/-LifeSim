using LifeSlim.Core.Model;

namespace LifeSlim.Core.ValueObjects;

public class Potion
{
    public Position Position { get; set; }
    
    public static string DrinkPotion(Creature creature)
    {
        creature.Health += 5;
        creature.Hunger -= 5;
        creature.Dna.Stats.Strength += 10;
        creature.Dna.Stats.Defense -= 12;

        return "El hambre ha disminuido 5ptos,la salud aumento 5ptos,la fuerza 10pts,y la defensa disminuyo 12ptos";
    }
}