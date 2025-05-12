using LifeSlim.Core.Factories;
using LifeSlim.Core.Interface;
using LifeSlim.Core.Model;
using LifeSlim.Core.Movement;
using LifeSlim.Core.ValueObjects;

namespace LifeSlim.Core.System;

public class MovementSystem(MovementStrategyFactory strategyFactory, ICombatStrategyFactory combatStrategyFactory)
{
    private async Task Move(World world, Creature creature)
    {
        VerifyAdyacents(world,creature);
        
        var currentKey = $"{creature.Position.X},{creature.Position.Y}";
        world.CreaturePositions.Remove(currentKey); // Elimina la posición actual

        const int maxRetries = 5;
        var retries = 0;
        Position nextPosition;
        string newKey;

        do
        {
            var strategy = strategyFactory.GetStrategy(creature);
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
        var adyacents = new List<Position>
        {
            new Position(creature.Position.X - 1, creature.Position.Y), // Izquierda
            new Position(creature.Position.X + 1, creature.Position.Y), // Derecha
            new Position(creature.Position.X, creature.Position.Y - 1), // Arriba
            new Position(creature.Position.X, creature.Position.Y + 1) // Abajo
        };

        foreach (var pos in adyacents.Where(pos => pos.X >= 0 && pos.X < world.Width && pos.Y >= 0 && pos.Y < world.Height))
        {
            if (!world.CreaturePositions.TryGetValue($"{pos.X},{pos.Y}", out var idEnCelda)) continue;
            var adjacentObject = world.MapObjects.FirstOrDefault(c => c.Id.ToString() == idEnCelda);
                    
            switch (adjacentObject)
            {
                case Creature creatureAdyacente:
                    combatStrategyFactory.GetCombatStrategy(creature,creatureAdyacente);
                    break;
                case Food { IsConsumed: true }:
                    continue;
                // MovementSystem.cs (en VerifyAdyacents)
                case Food { IsConsumed: false } foodAdyacente:
                    foodAdyacente.Eat(creature);
                    foodAdyacente.MarkAsConsumed();
                    break;
            }
        }
    }

    private void RemoveMapObjects(World world)
    {
        Console.WriteLine("Removing map objects "+world.MapObjects.Count);

        var foodsToRemove = world.MapObjects.OfType<Food>().Where(food => food.IsConsumed).ToList();

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