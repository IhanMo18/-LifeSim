using LifeSlim.Core.Util;

namespace LifeSlim.Core.Model;

public class Creature
{
    public Creature(Guid raceId, Dna dna)
    {
        RaceId = raceId;
        Dna = dna;
    }
    
    public int Hp { get; set; } = 100;
    
    public Guid Id { get; private set; } = Guid.NewGuid();
    
    public Dna Dna { get; private set; }
    public int Age { get; private set; } = 0;
    public bool IsAlive { get; private set; } = true;
    public Guid RaceId { get; private set; }

    public void AgeOneYear()
    {
        Age++;
        if (Age < 25) //edad generica
        {
            IsAlive = false;
        }
    }

    public bool CanReproduce()
    {
        return IsAlive && Age >=5 && Hp >= 50;
    }

    public Creature ReproduceWith(Creature partner)
    {
        if (this.CanReproduce() && partner.CanReproduce())
        {
            var childDna = Dna.Combine(this.Dna,partner.Dna);
            var childRaceId = SelectRandomRaceId(this.RaceId,partner.RaceId);
            
            return new Creature(
                childRaceId, 
                childDna);
        }
        throw new Exception("Creature can't reproduce");
    }

    public void Mutate(Mutation mutation)
    {
        var random = new Random();
        
        if (mutation.ShouldApply(random))
        {
            switch (mutation.Stat)
            {
                case StatType.Strength: Dna.Stats.Strength += mutation.ChangeAmount; break;
                case StatType.Speed: Dna.Stats.Speed += mutation.ChangeAmount; break;
                case StatType.Vision: Dna.Stats.Vision += mutation.ChangeAmount; break;
                case StatType.Defense: Dna.Stats.Defense += mutation.ChangeAmount; break;
                case StatType.Aggression: Dna.Stats.Aggression += mutation.ChangeAmount; break;
            }
            Console.WriteLine($"{mutation.Stat} is mutated");
        }
        else
        {
            Console.WriteLine($"{mutation.Stat} is not mutated");
        }
    }
    
    public void Die()
    {
        IsAlive = false;
    }
    
    public static Guid SelectRandomRaceId(Guid raceIdA, Guid raceIdB)
    {
        return Random.Shared.Next(0, 2) == 0 ? raceIdA : raceIdB;
    }

}
