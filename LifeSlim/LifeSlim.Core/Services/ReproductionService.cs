using LifeSlim.Core.Model;
using LifeSlim.Core.ValueObjects;

namespace LifeSlim.Core.Services;

public class ReproductionService
{
    public static Creature Reproduce(World world, Creature creature,Creature partner)
    {
        var position = world.GenerateFreePosition();
        
        if (creature.CanReproduce() && partner.CanReproduce())
        {
            var childDna = Dna.Combine(creature.Dna,partner.Dna);
            var childRaceId = SelectRandomRaceId(creature.RaceId,partner.RaceId);
            
            
            return new Creature(
                childRaceId, 
                childDna,
                position);
        }
        Console.WriteLine("No pueden reproducirse");
        throw new Exception("No pueden reproducirse");
    }
    
    private static Guid SelectRandomRaceId(Guid raceIdA, Guid raceIdB)
    {
        return Random.Shared.Next(0, 2) == 0 ? raceIdA : raceIdB;
    }
    
}