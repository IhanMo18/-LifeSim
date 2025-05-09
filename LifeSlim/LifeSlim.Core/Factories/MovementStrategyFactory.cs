using LifeSlim.Core.Interface;
using LifeSlim.Core.Model;
using LifeSlim.Core.Movement;

namespace LifeSlim.Core.Factories;

public class MovementStrategyFactory
{
    private readonly IMovementStrategy _random;
    private readonly IMovementStrategy _flee;
    private IMovementStrategy _forage;
    private IMovementStrategy _hunter;
    private readonly ICombatStrategyFactory _combatStrategyFactory;
    private readonly World _world;
    private readonly IVisionService _vision;

    public MovementStrategyFactory(IServiceProvider serviceProvider, IVisionService vision, ICombatStrategyFactory combatStrategyFactory, World world)
    {
        _vision = vision;
        _combatStrategyFactory = combatStrategyFactory;
        _world = world;
        _random = new RandomMovementStrategy();
        _flee = new FleeStrategy();
    }

    public IMovementStrategy GetStrategy(Creature creature)
    {
        
        var nearObjects = _vision.FindNearbyMapObjects(_world,creature,creature.Dna.Stats.Vision);

        if (nearObjects.Count == 0)
        {
            return _random;
        }
        
        //Revisamos si debe huir
        var hunters = nearObjects
            .OfType<Creature>()
            .Where(c => c != creature && c.CanEngage(creature) && !c.ShouldSubmitTo(creature) && !c.ShouldFleeFrom(creature))
            .ToList();

        if (hunters.Count != 0)
        {
            return _flee;
        }
        
        //Revisamos si hay presas en vision
        var allPrey = nearObjects
            .OfType<Creature>()
            .Where(c => c != creature && !c.CanEngage(creature) && c.ShouldFleeFrom(creature) && c.ShouldSubmitTo(creature))
            .ToList();

        if (allPrey.Count != 0)
        {
            var closestPrey = allPrey
                .OrderBy(p => Math.Abs(p.Position.X - creature.Position.X) +
                              Math.Abs(p.Position.Y - creature.Position.Y))
                .First();

            Console.WriteLine($"Hunting {creature.Id} started to hunt {closestPrey.Id}");
            _hunter = new HuntStrategy(closestPrey);
            return _hunter;
        }
        
        //Checkeamos que pueda comer chill
        if (nearObjects.OfType<Food>().Count() == 0)
        {
            return _random;
        }
        
        var closestFood = nearObjects
            .OfType<Food>()
            .OrderBy(f => Math.Abs(f.Position.X - creature.Position.X) + 
                          Math.Abs(f.Position.Y - creature.Position.Y))
            .First();

        if (closestFood != null)
        {
            Console.WriteLine($"Forage {creature.Id} tracking food {closestFood.Id}");
            _forage = new ForageStrategy(closestFood);
            return _forage;
        }
        // _combatStrategyFactory.GetCombatStrategy(creature, nearObjects!);
        
        // if (creature.Health < 30)
        // {
        //     Console.WriteLine("Movimiento de huir");
        //     return _flee;
        // }
        return _random;
    }
    
    
}