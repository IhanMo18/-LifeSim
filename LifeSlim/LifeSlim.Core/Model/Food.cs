using LifeSlim.Core.ValueObjects;

namespace LifeSlim.Core.Model;

public class Food(Position position) : MapObject(position)
{
    public bool IsConsumed { get; private set; }
   
    public void Eat(Creature creature)
    {
        creature.Hunger -= 20;
        creature.Health += 10;
        
        Console.WriteLine($"[CONSUMIENDO] Creature {creature.Id} ate food {Id}");
    }
    public void MarkAsConsumed() => IsConsumed = true;
    public override string ObjType=>"Food";
}