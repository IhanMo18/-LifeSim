using LifeSlim.Application.Hubs;
using LifeSlim.Application.Interfaces;
using LifeSlim.Application.UseCases.Race.Commands;
using LifeSlim.Core.Interface;
using LifeSlim.Core.Model;
using LifeSlim.Core.System;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;


namespace LifeSlim.Application.GameEngine;

public class SimulationEngine
{
    private readonly MovementSystem _movementSystem;
    private readonly MutationSystem _mutationSystem;
    private readonly World _world;
    private readonly ICreatureFactory _creatureFactory;
    
    private readonly IHubContext<GameHub> _hubContext;
    
    
    
    public SimulationEngine(World world, MovementSystem movementSystem, ICreatureFactory creatureFactory, MutationSystem mutationSystem, IHubContext<GameHub> hubContext)
    {
        _world = world;
        _movementSystem = movementSystem;
        _creatureFactory = creatureFactory;
        _mutationSystem = mutationSystem;
        _hubContext = hubContext;
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
        
        
        // if (_world.Creatures.<10)
        // {
        //     for (int i = 0; i < 10; i++)
        //     {

        
        while (_world.Creatures.Count<5)
        {
            try
            {
                var creature = _creatureFactory.CreateCreature(_world, race);
                _world.Creatures.Add(creature);
                // _world.grid[creature.Position.X, creature.Position.Y] = $"{creature.Id}" ;
                _world.CreaturePositions.Add($"{creature.Position.X},{creature.Position.Y}", creature.Id.ToString());
                Console.WriteLine("EL COUNT ES "+_world.Creatures.Count);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear criatura: {ex.Message}");
            }    
        }

        foreach (var crea in _world.Creatures)
        {
            Console.WriteLine("El id es "+crea.Id);
        }


        foreach (var crea in _world.CreaturePositions)
        {
            Console.WriteLine("Posiciones viejas "+crea.Key);
        }
        
        // 2. Mover criaturas ,mutar criaturas,envejecer
        foreach (var creature in _world.Creatures)
        {
            Console.WriteLine("El Position es "+creature.Position.X+","+creature.Position.Y);
            _movementSystem.Move(_world, creature);     //Mueve las criaturas
            Console.WriteLine("La nueva position es "+creature.Position.X+","+creature.Position.Y);
            
            // _mutationSystem.Mutate(creature);           //Muta las criaturas
            creature.AgeOneYear();                      //envejece las criaturas
        }

        foreach (var crea in _world.CreaturePositions)
        {
            Console.WriteLine("Posiciones actuales "+crea.Key);
        }
        
        
        //Mostrar Criaturas
        for (var i = 0; i < _world.Width; i++)
        {
            for (var j = 0; j < _world.Height; j++)
            {
                Console.Write(_world.CreaturePositions.ContainsKey($"{i},{j}") ? "*" : "-");
            }
            Console.WriteLine();
        }
        
        //Reproducirse
        // foreach (var creature in _world.Creatures)
        // {
        //     ReproductionSystem.Reproduce(_world,creature); // se puede inyectar world
        // }

        _world.Creatures.RemoveAll(c => c.IsAlive==false);
        Console.WriteLine($"🕒 Año {_world.YearTime}...");
        // TODO: Guardar estado o hacer log si quieres
        
        // var snapshot = new WorldSnapshot(_world);
        
        // Notificar a los suscriptores (ej: interfaz)
        await _hubContext.Clients.All.SendAsync("ReceiveUpdate", _world);
    }
}

public record WorldSnapshot(
    World World)
{}
