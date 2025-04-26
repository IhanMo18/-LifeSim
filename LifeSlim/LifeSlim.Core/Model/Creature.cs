
using LifeSlim.Core.ValueObjects;

namespace LifeSlim.Core.Model;

public class Creature 
{
    public Creature(Guid raceId, Dna dna,Position position)
    {
        RaceId = raceId;
        Dna = dna;
        Position = position;
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
    public void Mutate(Mutation mutation)
    {
        Dna.ApplyMutation(mutation);
    }
    public void Die()
    {
        IsAlive = false;
    }
    

}
