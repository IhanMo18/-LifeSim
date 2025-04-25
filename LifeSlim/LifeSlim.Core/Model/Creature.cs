using LifeSlim.Core.Util;

namespace LifeSlim.Core.Model;

public class Creature
{
    public Creature(Guid raceId, Dna dna)
    {
        RaceId = raceId;
        Dna = dna;
    }
    
    public int Health { get; set; } = 100;

    public int Hunger { get; set; } = 0;
    
    public Position Position { get; set; }
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Dna Dna { get; set; }
    public int Age { get; set; } = 0;
    public bool IsAlive { get; set; } = true;
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
        return IsAlive && Age >=5 && Health >= 50;
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
        Dna.ApplyMutation(mutation);
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
