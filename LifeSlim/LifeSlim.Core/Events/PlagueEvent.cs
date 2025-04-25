using LifeSlim.Core.Model;

namespace LifeSlim.Core.Events;

public class PlagueEvent(int triggerYear) : WorldEvent("Plague", triggerYear)
{
    public override void Apply(World world)
    {
        // Matar el 10% aleatorio de criaturas
        var rand = new Random();
        var deaths = (int)(world.Creatures.Count * 0.1);
        var toKill = world.Creatures.OrderBy(c => rand.Next()).Take(deaths).ToList();

        foreach (var c in toKill)
        {
            c.Health = 0;
        }

        Console.WriteLine($"[Año {world.YearTime}] Una plaga ha matado a {deaths} criaturas.");
    }
}