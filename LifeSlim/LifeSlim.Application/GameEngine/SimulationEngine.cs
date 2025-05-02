using System.Runtime.Serialization;
using LifeSlim.Application.Hubs;
using LifeSlim.Application.Interfaces;
using LifeSlim.Application.UseCases.Race.Commands;
using LifeSlim.Core.Builders;
using LifeSlim.Core.Interface;
using LifeSlim.Core.Model;
using LifeSlim.Core.System;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;


namespace LifeSlim.Application.GameEngine;

public class SimulationEngine
{
    private readonly MovementSystem _movementSystem;
    private readonly MutationSystem _mutationSystem;
    private readonly World _world;
    private readonly ICreatureFactory _creatureFactory;
    
    private readonly IHubContext<GameHub> _hubContext;
    
    
    
    public SimulationEngine(World world, MovementSystem movementSystem, ICreatureFactory creatureFactory, 
        MutationSystem mutationSystem, IHubContext<GameHub> hubContext)
    {
        _world = world;
        _movementSystem = movementSystem;
        _creatureFactory = creatureFactory;
        _mutationSystem = mutationSystem;
        _hubContext = hubContext;
    }

    public async Task Tick(IServiceProvider serviceProvider)
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
        
        while (_world.Creatures.Count<5)
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
                Console.WriteLine("EL COUNT ES "+_world.Creatures.Count);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear criatura: {ex.Message}");
            }    
        }

        foreach (var crea in _world.CreaturePositions)
        {
            Console.WriteLine("Posiciones viejas "+crea.Key);
        }
        
        foreach (var creature in _world.Creatures)
        {
            _movementSystem.Move(_world, creature);     
            // _mutationSystem.Mutate(creature);           
            creature.AgeOneYear();                      
        }

        foreach (var crea in _world.CreaturePositions)
        {
            Console.WriteLine("Posiciones actuales "+crea.Key);
        }
        
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
        var scope = serviceProvider.CreateScope();
        var dataWorld = scope.ServiceProvider.GetRequiredService<IDataWorld>();
        
        await dataWorld.Save();
        
        
        await _hubContext.Clients.All.SendAsync("ReceiveUpdate", _world);
    }
}
