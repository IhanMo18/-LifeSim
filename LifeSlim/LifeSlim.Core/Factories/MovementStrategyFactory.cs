using LifeSlim.Core.Interface;
using LifeSlim.Core.Model;
using LifeSlim.Core.Movement;

public class MovementStrategyFactory
{
    private readonly IMovementStrategy _random;
    private readonly IMovementStrategy _flee;
    private readonly IMovementStrategy _forage;
    private readonly IMovementStrategy _hunt;
    private readonly IMovementStrategy _attack;

    public MovementStrategyFactory()
    {
        _random = new RandomMovementStrategy(); // Ya tienes esta estrategia
        _flee = new FleeStrategy();
        _forage = new ForageStrategy();
        _hunt = new HuntStrategy(); // Aquí no cambiamos nada
    }

    public IMovementStrategy GetStrategy(Creature creature)
    {
        if (!creature.IsAlive)
        {
            Console.WriteLine("Movimiento random");
            return _random;
        }

        if (creature.Health < 30)
        {
            Console.WriteLine("Movimiento de huir");
            return _flee;
        }

        if (creature.Hunger > 70)
        {
            Console.WriteLine("Movimiento hambre");
            return _forage;
        }

        Console.WriteLine("Movimiento de caza");
        return _hunt; // Si no, comportamiento de cazar por defecto
    }
}