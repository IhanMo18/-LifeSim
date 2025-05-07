using LifeSlim.Core.Builders;
using LifeSlim.Core.Factories;
using LifeSlim.Core.Interface;
using LifeSlim.Core.Model;

namespace LifeSlim.Core.System;

public class ObjectsSystem
{
    public readonly World _world;
    public readonly ICreatureFactory _creatureFactory;
    private readonly IFoodFactorie _foodFactorie;

    public ObjectsSystem(World world, ICreatureFactory creatureFactory, IFoodFactorie foodFactorie)
    {
        _world = world;
        _creatureFactory = creatureFactory;
        _foodFactorie = foodFactorie;
    }


    public void AddCreatures(int amount)
    {
        while (_world.MapObjects.OfType<Creature>().Count() < amount)
        {
            try
            {
                var race = new RaceBuilder().WhitName("Orco")
                    .WhitColorCode("#fffef")
                    .WhitStats(1, 1, 1, 1, 1)
                    .Build();

                var creature = _creatureFactory.CreateCreature(_world, race);
                _world.MapObjects.Add(creature);
                _world.CreaturePositions.Add($"{creature.Position.X},{creature.Position.Y}", creature.Id.ToString());
                Console.WriteLine("EL COUNT De Criatura ES " + _world.MapObjects.OfType<Creature>().Count());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear criatura: {ex.Message}");
            }
        }

        while (_world.MapObjects.OfType<Food>().Count() < _world.MapObjects.OfType<Creature>().Count() * 2)
        {
            var food = _foodFactorie.CreateFood();
            _world.MapObjects.Add(food);
            _world.CreaturePositions.Add($"{food.Position.X},{food.Position.Y}", food.Id.ToString());
            Console.WriteLine("EL COUNT de Food ES " + _world.MapObjects.OfType<Food>().Count());
        }

        for (var i = 0; i < _world.Width; i++)
        {
            for (var j = 0; j < _world.Height; j++)
            {
                if (_world.CreaturePositions.TryGetValue($"{i},{j}", out var mapObjectId))
                {
                    var mapObject = _world.MapObjects.FirstOrDefault(mo =>
                        string.Equals($"{mo.Id}", mapObjectId));
                    
                    if (mapObject?.GetType().Name == "Food")
                    {
                        Console.Write("#");    
                    }
                    else
                    {
                        Console.Write("*");    
                    }
                }
                Console.Write("-");    
            }

            Console.WriteLine();
        }
    }

    public void RemoveCreatures()
    {
        _world.MapObjects.OfType<Creature>().ToList().RemoveAll(c => c.IsAlive==false);
    }

}