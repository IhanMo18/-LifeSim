using LifeSlim.Core.Model;

namespace LifeSlim.Core.Events;

public class AcidRainEvent(int triggerYear) : WorldEvent("AcidRain", triggerYear)
{
    
    public override string EventType=>"AcidRain";
    public override void Apply(World world)
    {
        Console.WriteLine("☣️ Lluvia Ácida! Todas las criaturas sufren daño...");

        foreach (var creature in world.MapObjects.OfType<Creature>())
        {
            var damage = (int)(creature.Health * 0.2);
            creature.Health -= damage;

            if (creature.Health < 0)
            {
                creature.Health = 0;
                creature.IsAlive = false;  
            }
            Console.WriteLine($"🐛 Criatura dañada. Salud restante: {creature.Health}");
        }
    }
}