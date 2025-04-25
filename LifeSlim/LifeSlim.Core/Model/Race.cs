using LifeSlim.Core.Exceptions;

namespace LifeSlim.Core.Model;

public class Race
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Name { get; private set; }
    public string ColorCode { get; private set; } 
    public Stats BaseStats { get; private set; }
    public List<Creature> Creatures { get; private set; }
    public int AvailableStatPoints { get; private set; } = 5;
    public bool IsAlive { get; private set; } = true;
    
    public int Age { get; private set; } = 0;

    public Race(string name,string colorCode,List<Creature> creatures, Stats baseStats)
    {
        Name = name;
        ColorCode = colorCode;
        Creatures = creatures;
        BaseStats = baseStats;
    }

    public void EvolveStats(int points,string statName)
    {
        if (AvailableStatPoints < points)
        {
            throw new NoAvailableStatsPoints();
        }
        AvailableStatPoints -= points;

        switch (statName.ToLower())
        {
            case "strength": BaseStats.Strength += points; break;
            case "speed": BaseStats.Speed += points; break;
            case "vision": BaseStats.Vision += points; break;
            case "defense": BaseStats.Defense += points; break;
            case "aggression": BaseStats.Aggression += points; break;
            default:
                throw new NoSuchStat();
        }
        Console.WriteLine($"Your Stats for Race has been updated.\n {BaseStats.ToString()}");
    }

    public void AddCreature(Creature creature)
    {
        if (Creatures.Contains(creature)) throw new RaceContainsTheCreature();
        Creatures.Add(creature);
    }

    public void AdvanceYear()
    {
        Age++;
        if(Age >= 20) IsAlive = false;
    }
}
