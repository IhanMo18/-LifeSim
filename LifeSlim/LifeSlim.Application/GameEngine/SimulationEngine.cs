using LifeSlim.Application.Interfaces;
using LifeSlim.Application.UseCases.Race.Commands;
using LifeSlim.Application.UseCases.Race.CommandsHandler;
using LifeSlim.Core.Builders;
using LifeSlim.Core.Interface;
using LifeSlim.Core.Model;
using LifeSlim.Core.System;
using LifeSlim.Core.ValueObjects;

namespace LifeSlim.Application.GameEngine;

public class SimulationEngine
{
    private readonly MovementSystem _movementSystem;
    private readonly World _world;
    private readonly ICreatureFactory _creatureFactory;

    public SimulationEngine(World world, MovementSystem movementSystem, ICreatureFactory creatureFactory)
    {
        _world = world;
        _movementSystem = movementSystem;
        _creatureFactory = creatureFactory;
    }

    public async Task Tick()
    {
        _world.YearTime++;

        // 1. Ejecutar eventos globales
        var eventsToTrigger = _world.ScheduledEvents
            .Where(e => e.TriggerYear == _world.YearTime)
            .ToList();

        // foreach (var ev in eventsToTrigger)
        // {
        //     ev.Apply(_world);
        // }
        
        //Crear Raza
        // CreateRaceCommand createRaceCommand = new CreateRaceCommand
        // {
        //     BaseStats = new (1,1,1,1,1),
        //     Name = "Gorgons",
        //     ColorCode = "#421eaf"
        // };
        // CreateRaceCommandHandler createRaceCommandHandler = new CreateRaceCommandHandler();
        //
        // var race = await createRaceCommandHandler.Handle(createRaceCommand); 
        
        
        

        
        if (_world.Creatures.Count<3)
        {
           
            try
            {
                var race = new RaceBuilder().WhitName("Orco").WhitColorCode("efffe").WhitStats(1,1,3,1,1)
                    .Build();
                var creature = _creatureFactory.CreateCreature(_world, race);
                var creature2 = _creatureFactory.CreateCreature(_world, race);
                if (creature != null && creature.Position != null)
                {
                    _world.Creatures.Add(creature);
                    _world.grid[creature.Position.X, creature.Position.Y] = $"{creature.Id}" ;
                    _world.Creatures.Add(creature2);
                    _world.grid[creature2.Position.X, creature2.Position.Y] = $"{creature2.Id}";
                }
                else
                {
                    // Manejar caso donde la criatura no se pudo crear
                    Console.WriteLine("Error: No se pudo crear la criatura.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear criatura: {ex.Message}");
            }  
        }
        //Aniadir Criaturas 
        // for (int i = 0; i < 5; i++)
        // {
        //     
        //     // _world.Creatures.Add(_creatureFactory.CreateCreature(_world, new Race("Gorgons","#42afee",new Stats(1,1,1,1,1))));
        // }

       
        for (var i = 0; i < _world.Width; i++)
        {
            for (var j = 0; j < _world.Height; j++)
            {
                if (!string.IsNullOrWhiteSpace(_world.grid[i,j]))
                {
                    Console.Write("*");
                }
                Console.Write("-");
            }
            Console.WriteLine();
        }
        
        // 2. Mover criaturas
        foreach (var creature in _world.Creatures)
        {
            _movementSystem.MoveCreature(_world, creature);
        }

        // 3. Reproducción

        // 4. Envejecimiento y muerte
        foreach (var creature in _world.Creatures)
        {
            creature.Age++;
        }

        _world.Creatures.RemoveAll(c => c.IsAlive==false);
        Console.WriteLine($"🕒 Año {_world.YearTime}...");
        // TODO: Guardar estado o hacer log si quieres
    }
}
