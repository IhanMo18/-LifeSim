using LifeSlim.Core.ValueObjects;

namespace LifeSlim.Core.Model;

public class Food(Position position) : MapObject(position)
{
    public void Eat(Creature creature)
    {
        creature.Hunger -= 20;
        creature.Health += 10;
        
        Console.WriteLine("El hambre ha disminuido "+creature.Hunger+",la salud aumento "+creature.Health);
    }

    public override string ObjType=>"Food";
}