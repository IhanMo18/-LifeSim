namespace LifeSlim.Core.Model;

public class Dna
{
    public Stats Stats { get; private set; }
    public List<Mutation> Mutations { get; private set; }

    public Dna(Stats stats, List<Mutation> mutations)
    {
        Stats = stats;
        Mutations = mutations;
    }

    public static Dna Combine(Dna parentA, Dna parentB)
    {
        // Mezclamos los Stats de ambos padres (promedio o aleatorio entre los dos)
        var newStats = new Stats(
            strength: RandomBetween(parentA.Stats.Strength, parentB.Stats.Strength),
            speed: RandomBetween(parentA.Stats.Speed, parentB.Stats.Speed),
            vision: RandomBetween(parentA.Stats.Vision, parentB.Stats.Vision),
            defense: RandomBetween(parentA.Stats.Defense, parentB.Stats.Defense),
            aggression: RandomBetween(parentA.Stats.Aggression, parentB.Stats.Aggression)
        );

        // Heredamos algunas mutaciones (por ejemplo, 50% de cada padre)
        var inheritedMutations = new List<Mutation>();
        inheritedMutations.AddRange(parentA.Mutations.OrderBy(x => Guid.NewGuid()).Take(parentA.Mutations.Count / 2));
        inheritedMutations.AddRange(parentB.Mutations.OrderBy(x => Guid.NewGuid()).Take(parentB.Mutations.Count / 2));

        return new Dna(newStats, inheritedMutations);
    }

    private static int RandomBetween(int a, int b)
    {
        var min = Math.Min(a, b);
        var max = Math.Max(a, b);
        return Random.Shared.Next(min, max + 1);
    }

    // public Dna ApplyMutation(Mutation mutation);
}
