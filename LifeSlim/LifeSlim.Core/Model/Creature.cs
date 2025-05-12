
using LifeSlim.Core.ValueObjects;

namespace LifeSlim.Core.Model;

public class Creature : MapObject
{
    public Creature(Guid raceId, Dna dna,Position position) : base(position)
    {
        RaceId = raceId;
        Dna = dna;
    }
    
    public int Health { get; set; } = RandomBetween(1,80);
    public int Hunger { get; set; } = RandomBetween(1,100);
    public Dna Dna { get; set; }
    private int Age { get; set; } = 0;
    public bool IsAlive { get; set; } = true;
    
    public bool IsPair { get; set; } = false;
    public Guid RaceId { get; private set; }
    
    public (int X, int Y) CurrentDirection { get; private set; }
    
    public override string ObjType=>"Creature";

    public void AgeOneYear()
    {
        Age++;
        if (Age > 25) //edad generica
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
    
    public bool ShouldFleeFrom(Creature pursuer) =>
        this.Health < 50 || 
        pursuer.Dna.Stats.Strength > this.Dna.Stats.Strength ||
        this.Dna.Stats.Aggression < 8;

    public bool CanEngage(Creature target) =>
        this.Dna.Stats.Aggression >= 8 &&
        this.Health > 30 &&
        this.Dna.Stats.Strength >= target.Dna.Stats.Strength;

    public bool ShouldSubmitTo(Creature pursuer) =>
        this.Health < 20 &&
        pursuer.Dna.Stats.Strength > this.Dna.Stats.Strength + 4;
    
    public void MoveTo(Position newPosition)
    {
        CurrentDirection = (
            newPosition.X - Position.X,
            newPosition.Y - Position.Y
        );
            
        Position = newPosition;
    }
    
    private static int RandomBetween(int a, int b)
    {
        var min = Math.Min(a, b);
        var max = Math.Max(a, b);
        return Random.Shared.Next(min, max + 1);
    }
}
