using LifeSlim.Core.Factories;
using LifeSlim.Core.Interface;
using LifeSlim.Core.Model;
using LifeSlim.Core.ValueObjects;

namespace LifeSlim.Core.System;

public class MovementSystem
{
    private readonly ICombatStrategyFactory _combatStrategyFactory;
    private readonly MovementStrategyFactory _movementStrategyFactory;
    List<MapObject> mapObjectsToRemove = new List<MapObject>();
    public MovementSystem(MovementStrategyFactory strategyFactory, ICombatStrategyFactory combatStrategyFactory)
    {
        _combatStrategyFactory = combatStrategyFactory;
        _movementStrategyFactory = strategyFactory;
    }

    private async Task Move(World world, Creature creature)
    {
        var currentKey = $"{creature.Position.X},{creature.Position.Y}";
        world.CreaturePositions.Remove(currentKey); // Elimina la posición actual

        var strategy = _movementStrategyFactory.GetStrategy(creature);
        var nextPosition = await strategy.NextPosition(world, creature);
        var newKey = $"{nextPosition.X},{nextPosition.Y}";

        while (world.CreaturePositions.ContainsKey(newKey))
        {
            strategy = _movementStrategyFactory.GetStrategy(creature);
            nextPosition = await strategy.NextPosition(world, creature);
            newKey = $"{nextPosition.X},{nextPosition.Y}";
        }
        creature.MoveTo(nextPosition);
        world.CreaturePositions[newKey] = creature.Id.ToString();

        var posicionesAdyacentes = new List<Position>
        {
            new Position(creature.Position.X - 1, creature.Position.Y), // Izquierda
            new Position(creature.Position.X + 1, creature.Position.Y), // Derecha
            new Position(creature.Position.X, creature.Position.Y - 1), // Arriba
            new Position(creature.Position.X, creature.Position.Y + 1) // Abajo
        };
        
        foreach (var pos in posicionesAdyacentes)
        {
            if (pos.X >= 0 && pos.X < world.Width && pos.Y >= 0 && pos.Y < world.Height)
            {
                if(world.CreaturePositions.TryGetValue($"{pos.X},{pos.Y}",out var idEnCelda))
                {
                    var objetoAdyacente = world.MapObjects.FirstOrDefault(c => c.Id.ToString() == idEnCelda);
                    
                    if (objetoAdyacente is Creature creatureAdyacente)
                    {
                        _combatStrategyFactory.GetCombatStrategy(creature,world.MapObjects);
                    }
                    if (objetoAdyacente is Food foodAdyacente)
                    {
                        foodAdyacente.Eat(creature);
                        mapObjectsToRemove.Add(foodAdyacente);
                        world.CreaturePositions.Remove($"{foodAdyacente.Position.X},{foodAdyacente.Position.Y}");
                        Console.WriteLine($"Creature {creature.Id} ate food {foodAdyacente.Id}");
                    }
                }
            }
        }
    }


    public async Task MoveCreaturesInWorld(World world)
    {
        foreach (var creature in world.MapObjects.OfType<Creature>())
        {
            await Move(world, creature);
            creature.AgeOneYear();                      
        }

        foreach (var food in mapObjectsToRemove)
        {
            world.MapObjects.Remove(food);
        }
        mapObjectsToRemove.Clear();
    }
}