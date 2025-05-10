using LifeSlim.Core.Factories;
using LifeSlim.Core.Interface;
using LifeSlim.Core.Model;
using LifeSlim.Core.Movement;
using LifeSlim.Core.ValueObjects;

namespace LifeSlim.Core.System;

public class MovementSystem
{
    private readonly ICombatStrategyFactory _combatStrategyFactory;
    private readonly MovementStrategyFactory _movementStrategyFactory;
    public MovementSystem(MovementStrategyFactory strategyFactory, ICombatStrategyFactory combatStrategyFactory)
    {
        _combatStrategyFactory = combatStrategyFactory;
        _movementStrategyFactory = strategyFactory;
    }

    private async Task Move(World world, Creature creature)
    {
        VerifyAdyacents(world,creature);
        
        var currentKey = $"{creature.Position.X},{creature.Position.Y}";
        world.CreaturePositions.Remove(currentKey); // Elimina la posición actual

        // var strategy = _movementStrategyFactory.GetStrategy(creature);
        // var nextPosition = await strategy.NextPosition(world, creature);
        // var newKey = $"{nextPosition.X},{nextPosition.Y}";

        const int maxRetries = 5;
        int retries = 0;
        Position nextPosition;
        string newKey;
        IMovementStrategy strategy;

        do
        {
            strategy = _movementStrategyFactory.GetStrategy(creature);
            nextPosition = await strategy.NextPosition(world, creature);
            newKey = $"{nextPosition.X},{nextPosition.Y}";
            retries++;
        } while (world.CreaturePositions.ContainsKey(newKey) && retries < maxRetries);

        // Si se superan los reintentos, mover aleatoriamente
        if (retries >= maxRetries)
        {
            var randomStrategy = new RandomMovementStrategy();
            nextPosition = await randomStrategy.NextPosition(world, creature);
            newKey = $"{nextPosition.X},{nextPosition.Y}";
        }
        
        creature.MoveTo(nextPosition);
        world.CreaturePositions[newKey] = creature.Id.ToString();
    }


    public async Task MoveCreaturesInWorld(World world)
    {
        foreach (var creature in world.MapObjects.OfType<Creature>())
        {
            await Move(world, creature);
            creature.AgeOneYear();                      
        }
        RemoveMapObjects(world);    
    }

    private void VerifyAdyacents(World world,Creature creature)
    {
        var adjacents = new List<Position>
        {
            new Position(creature.Position.X - 1, creature.Position.Y), // Izquierda
            new Position(creature.Position.X + 1, creature.Position.Y), // Derecha
            new Position(creature.Position.X, creature.Position.Y - 1), // Arriba
            new Position(creature.Position.X, creature.Position.Y + 1) // Abajo
        };
        
        foreach (var pos in adjacents)
        {
            if (pos.X >= 0 && pos.X < world.Width && pos.Y >= 0 && pos.Y < world.Height)
            {
                if(world.CreaturePositions.TryGetValue($"{pos.X},{pos.Y}",out var idEnCelda))
                {
                    var adjacentObject = world.MapObjects.FirstOrDefault(c => c.Id.ToString() == idEnCelda);
                    
                    if (adjacentObject is Creature creatureAdyacente)
                    {
                        _combatStrategyFactory.GetCombatStrategy(creature,creatureAdyacente);
                    }
                    
                    if (adjacentObject is Food { IsConsumed: true }) 
                        continue;
                        
                    // MovementSystem.cs (en VerifyAdyacents)
                    if (adjacentObject is Food { IsConsumed: false } foodAdyacente)
                    {
                        foodAdyacente.Eat(creature);
                        foodAdyacente.MarkAsConsumed();
                    }
                }
            }
        }
    }

    private void RemoveMapObjects(World world)
    {
        List<Food> foodsToRemove = new List<Food>();
        
        Console.WriteLine("Removing map objects "+world.MapObjects.Count);
        
        foreach (var food in world.MapObjects.OfType<Food>())
        {
            if (food.IsConsumed)
            {
                foodsToRemove.Add(food);
            }
        }

        foreach (var food in foodsToRemove)
        {
            world.MapObjects.Remove(food);
            world.CreaturePositions.Remove($"{food.Position.X},{food.Position.Y}");
        }

        if (foodsToRemove.Count > 0)
        {
            foodsToRemove.Clear(); 
        }
        Console.WriteLine("Removing map objects finished "+ world.MapObjects.Count);
    }
}