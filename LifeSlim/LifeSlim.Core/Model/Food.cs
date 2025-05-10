using LifeSlim.Core.ValueObjects;

namespace LifeSlim.Core.Model;

public class Food(Position position) : MapObject(position)
{
    public bool IsConsumed { get; private set; }

    public void Eat(Creature creature)
    {
        if (IsConsumed) return;// Bloquear para otros movimientos
        creature.Hunger -= 30;
        creature.Health += 20;
        
        Console.WriteLine($"[CONSUMIENDO] Creature {creature.Id} ate food {Id}");
    }

    public void MarkAsConsumed() => IsConsumed = true;
}