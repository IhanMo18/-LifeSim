using LifeSlim.Core.Interface;
using LifeSlim.Core.Model;
using LifeSlim.Core.Movement;
using Microsoft.Extensions.DependencyInjection;

public class MovementStrategyFactory
{
    private readonly IMovementStrategy _random;
    private readonly IMovementStrategy _flee;
    private readonly IMovementStrategy _forage;
    private readonly IMovementStrategy _attack;
    private readonly IServiceProvider _serviceProvider;

    public MovementStrategyFactory(IServiceProvider serviceProvider)
    {
        _random = new RandomMovementStrategy();
        _flee = new FleeStrategy();
        _forage = new ForageStrategy();
        _serviceProvider = serviceProvider;
    }

    public IMovementStrategy GetStrategy(Creature creature)
    {
        
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

      
        using var scope = _serviceProvider.CreateScope();
        var visionService = scope.ServiceProvider.GetRequiredService<IVisionService>();
        Console.WriteLine("Movimiento de caza");
        return new HuntStrategy(visionService);
    }
}