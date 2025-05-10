using System.Runtime.Serialization;
using LifeSlim.Application.Hubs;
using LifeSlim.Application.Interfaces;
using LifeSlim.Application.UseCases.Race.Commands;
using LifeSlim.Core.Builders;
using LifeSlim.Core.Interface;
using LifeSlim.Core.Model;
using LifeSlim.Core.Services;
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
    private readonly ObjectsSystem _objectsSystem;
    private readonly IHubContext<GameHub> _hubContext;
    private readonly EventSystem _eventSystem;
    
    
    
    public SimulationEngine(World world, MovementSystem movementSystem,EventSystem eventSystem
        ,ObjectsSystem objectsSystem,MutationSystem mutationSystem,
        IHubContext<GameHub> hubContext)
    {
        _world = world;
        _movementSystem = movementSystem;
        _objectsSystem = objectsSystem;
        _mutationSystem = mutationSystem;
        _eventSystem = eventSystem;
        _hubContext = hubContext;
    }

    public async Task Tick(IServiceProvider serviceProvider)
    {
        var scope = serviceProvider.CreateScope();
        var dataWorld = scope.ServiceProvider.GetRequiredService<IDataWorld>();
        // _eventSystem.EventApply();
        _objectsSystem.AddCreatures(7);
        _objectsSystem.RemoveCreatures();
        Console.WriteLine($"{_world.MapObjects.Count}");
        await _movementSystem.MoveCreaturesInWorld(_world);
        Console.WriteLine($"{_world.MapObjects.Count}");
        foreach (var c in _world.MapObjects.OfType<Creature>())
        {
            _mutationSystem.Mutate(c);    
        }
        
        await _hubContext.Clients.All.SendAsync("ReceiveUpdate", _world);
        await dataWorld.Save();
        Console.WriteLine($"🕒 Año {_world.YearTime}...");
    }
}
