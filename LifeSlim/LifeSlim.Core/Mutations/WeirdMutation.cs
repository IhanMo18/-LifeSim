using LifeSlim.Core.Interface;
using LifeSlim.Core.Model;
using LifeSlim.Core.Util;
using LifeSlim.Core.ValueObjects;

namespace LifeSlim.Core.Mutations;

public class WeirdMutation : IMutationStrategy
{

    public void Mutate(Mutation mutation, Creature creature)
    {
        var random = new Random();
        
        if (mutation.ShouldApply(random))
        {
            switch (mutation.Stat)
            {
                case StatType.Strength: creature.Dna.Stats.Strength += mutation.ChangeAmount * 3; 
                    creature.Dna.Mutations.Add(mutation);
                    break;
                case StatType.Speed: creature.Dna.Stats.Speed += mutation.ChangeAmount * 3; 
                    creature.Dna.Mutations.Add(mutation);
                    break;
                case StatType.Vision: creature.Dna.Stats.Vision += mutation.ChangeAmount * 3; 
                    creature.Dna.Mutations.Add(mutation);
                    break;
                case StatType.Defense: creature.Dna.Stats.Defense += mutation.ChangeAmount * 3; 
                    creature.Dna.Mutations.Add(mutation);
                    break;
                case StatType.Aggression: creature.Dna.Stats.Aggression += mutation.ChangeAmount * 3; 
                    creature.Dna.Mutations.Add(mutation);
                    break;
            }
            Console.WriteLine($"{mutation.Stat} just receive a Special Mutation!!!! + {mutation.ChangeAmount} points");
        }
        else
        {
            Console.WriteLine($"{mutation.Stat} is not mutated");
        }  
    }
}