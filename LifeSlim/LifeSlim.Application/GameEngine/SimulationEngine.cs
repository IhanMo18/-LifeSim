using LifeSlim.Application.Interfaces;
using LifeSlim.Application.UseCases.Race.Commands;
using LifeSlim.Core.Interface;
using LifeSlim.Core.Model;
using LifeSlim.Core.System;


namespace LifeSlim.Application.GameEngine;

public class SimulationEngine
{
    private readonly MovementSystem _movementSystem;
    private readonly MutationSystem _mutationSystem;
    private readonly World _world;
    private readonly ICreatureFactory _creatureFactory;
    
    
    public SimulationEngine(World world, MovementSystem movementSystem, ICreatureFactory creatureFactory, MutationSystem mutationSystem)
    {
        _world = world;
        _movementSystem = movementSystem;
        _creatureFactory = creatureFactory;
        _mutationSystem = mutationSystem;
    }

    public async Task Tick(ICommandDispatcher? commandDispatcher)
    {
        _world.YearTime++;

        // 1. Ejecutar eventos globales
        var eventsToTrigger = _world.ScheduledEvents
            .Where(e => e.TriggerYear == _world.YearTime)
            .ToList();

        foreach (var ev in eventsToTrigger)
        {
            ev.Apply(_world);
        }
        
        //Crear Raza
        CreateRaceCommand createRaceCommand = new CreateRaceCommand
        {
            BaseStats = new (1,1,1,1,1),
            Name = "Gorgons",
            ColorCode = "#421eaf"
        };
        
        var race = await commandDispatcher!.Send<CreateRaceCommand, Race>(createRaceCommand);
        
        if (_world.Creatures.Count<10)
        {
            try
            {
                var creature = _creatureFactory.CreateCreature(_world, race);
                _world.Creatures.Add(creature);
                _world.grid[creature.Position.X, creature.Position.Y] = $"{creature.Id}" ;
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear criatura: {ex.Message}");
            }  
        }
        
        //Mostrar Criaturas
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
        
        // 2. Mover criaturas y mutar criaturas
        foreach (var creature in _world.Creatures)
        {
            _movementSystem.Move(_world, creature);
            _mutationSystem.Mutate(creature);
        }
        
        // 3. Reproducción

        
        // 4. Envejecimiento y muerte
        foreach (var creature in _world.Creatures)
        {
            creature.AgeOneYear();
        }

        _world.Creatures.RemoveAll(c => c.IsAlive==false);
        Console.WriteLine($"🕒 Año {_world.YearTime}...");
        // TODO: Guardar estado o hacer log si quieres
    }
}
