using LifeSlim.Core.Builders;
using LifeSlim.Core.Interface;
using LifeSlim.Core.Model;

namespace LifeSlim.Core.System;

public class ObjectsSystem
{
    private readonly World _world;
    private readonly ICreatureFactory _creatureFactory;
    private readonly IFoodFactory _foodFactory;

    public ObjectsSystem(World world, ICreatureFactory creatureFactory, IFoodFactory foodFactory)
    {
        _world = world;
        _creatureFactory = creatureFactory;
        _foodFactory = foodFactory;
    }
    private static int RandomBetween(int a, int b)
    {
        var min = Math.Min(a, b);
        var max = Math.Max(a, b);
        return Random.Shared.Next(min, max + 1);
    }

    public void AddCreatures(int amount)
    {
        while (_world.MapObjects.OfType<Creature>().Count() < amount)
        {
            try
            {
                var race = new RaceBuilder().WhitName("Orco")
                    .WhitColorCode("#fffef")
                    .WhitStats(RandomBetween(1,10), RandomBetween(1,10), RandomBetween(1,10), RandomBetween(1,10), RandomBetween(1,10))
                    .Build();

                var creature = _creatureFactory.CreateCreature(_world, race);
                _world.MapObjects.Add(creature);
                _world.CreaturePositions.Add($"{creature.Position.X},{creature.Position.Y}", creature.Id.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear criatura: {ex.Message}");
            }
        }
        

        if (!_world.MapObjects.OfType<Food>().Any())
        {
            int foodCount = _world.MapObjects.OfType<Creature>().Count() * 2;
            for (int i = 0; i < foodCount; i++)
            {
                var food = _foodFactory.CreateFood();
                _world.MapObjects.Add(food);
                _world.CreaturePositions.Add($"{food.Position.X},{food.Position.Y}", food.Id.ToString());
            }
        }

        for (var i = 0; i < _world.Width; i++)
        {
            for (var j = 0; j < _world.Height; j++)
            {
                if (_world.CreaturePositions.TryGetValue($"{i},{j}", out var mapObjectId))
                {
                    var mapObject = _world.MapObjects.FirstOrDefault(mo =>
                        string.Equals($"{mo.Id}", mapObjectId));

                    Console.Write(mapObject?.GetType().Name == "Food" ? "#" : "*");
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