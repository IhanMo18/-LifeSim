using LifeSlim.Core.Util;

namespace LifeSlim.Core.Model;

public class Mutation
{
    public StatType Stat { get; private set; }
    public int ChangeAmount { get; private set; }
    public double Probability { get; private set; }

    public Mutation(StatType stat, int changeAmount, double probability)
    {
        Stat = stat;
        ChangeAmount = changeAmount;
        Probability = probability;
    }

    // Este método decide si la mutación se aplica o no
    public bool ShouldApply(Random rng)
    {
        return rng.NextDouble() < Probability;
    }

    public override string ToString()
    {
        var sign = ChangeAmount >= 0 ? "+" : "";
        return $"{Stat}: {sign}{ChangeAmount} ({Probability * 100:0.0}% chance)";
    }
}