using LifeSlim.Core.Builders;
using LifeSlim.Core.Factories;
using LifeSlim.Core.Interface;
using LifeSlim.Core.Model;

namespace LifeSlim.Core.System;

public class ObjectsSystem
{
    public readonly World _world;
    public readonly ICreatureFactory _creatureFactory;

    public ObjectsSystem(World world, ICreatureFactory creatureFactory)
    {
        _world = world;
        _creatureFactory = creatureFactory;
    }


    public void AddCreatures(int amount)
    {
        while (_world.Creatures.Count < amount)
        {
            try
            {
                var race = new RaceBuilder().WhitName("Orco")
                    .WhitColorCode("#fffef")
                    .WhitStats(1, 1, 1, 1, 1)
                    .Build();

                var creature = _creatureFactory.CreateCreature(_world, race);
                _world.Creatures.Add(creature);
                _world.CreaturePositions.Add($"{creature.Position.X},{creature.Position.Y}", creature.Id.ToString());
                Console.WriteLine("EL COUNT ES " + _world.Creatures.Count);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear criatura: {ex.Message}");
            }
        }

        for (var i = 0; i < _world.Width; i++)
        {
            for (var j = 0; j < _world.Height; j++)
            {
                Console.Write(_world.CreaturePositions.ContainsKey($"{i},{j}") ? "*" : "-");
            }

            Console.WriteLine();
        }
    }

    public void RemoveCreatures()
    {
        _world.Creatures.RemoveAll(c => c.IsAlive==false);
    }

}