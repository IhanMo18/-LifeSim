using LifeSlim.Core.Exceptions;
using LifeSlim.Core.Util;
using LifeSlim.Core.ValueObjects;

namespace LifeSlim.Core.Model;

public class Race
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Name { get; private set; }
    public string ColorCode { get; private set; } 
    public List<Creature> Creatures { get;set; }
    
    public Stats BaseStats { get;set; }
    
    public int AvailableStatPoints { get; private set; } = 5;
    public int Age { get; private set; } = 0;

    public Race(string name,string colorCode, Stats baseStats)
    {
        Name = name;
        ColorCode = colorCode;
        BaseStats = baseStats;
    }

    public void AddCreature(Creature creature)
    {
        if (Creatures.Contains(creature)) throw new RaceContainsTheCreature();
        Creatures.Add(creature);
    }

    public void AdvanceYear()
    {
        Age++;
    }
    
    public void EvolveCreatureStats(int points,StatType stat,Guid creatureId)
    {
        if (AvailableStatPoints < points)
        {
            throw new NoAvailableStatsPoints();
        }
        AvailableStatPoints -= points;
        
        var creature = Creatures.FirstOrDefault(c => c.Id == creatureId) ?? throw new NoSuchCreatureException();

        switch (stat)
        {
            case StatType.Strength: creature.Dna.Stats.Strength += points; break;
            case StatType.Speed: creature.Dna.Stats.Speed += points; break;
            case StatType.Vision: creature.Dna.Stats.Vision += points; break;
            case StatType.Defense: creature.Dna.Stats.Defense += points; break;
            case StatType.Aggression: creature.Dna.Stats.Aggression += points; break;
            default:
                throw new NoSuchStat();
        }
        Console.WriteLine($"Your Stats has been updated.\n {creature.Dna.Stats.ToString()}");
    }
}
