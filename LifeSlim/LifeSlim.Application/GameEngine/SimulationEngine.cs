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
    private  World _world;
    private readonly ObjectsSystem _objectsSystem;
    private readonly IHubContext<GameHub> _hubContext;
    private readonly EventSystem _eventSystem;
    private bool _isWorldLoaded = false;
    
    
    
    public SimulationEngine(World world, MovementSystem movementSystem,EventSystem eventSystem
        ,ObjectsSystem objectsSystem,IHubContext<GameHub> hubContext)
    {
        _world = world;
        _movementSystem = movementSystem;
        _objectsSystem = objectsSystem;
        _eventSystem = eventSystem;
        _hubContext = hubContext;
    }

    public async Task Tick(IDataWorld dataWorld)
    {
        if (!_isWorldLoaded) await LoadWorld(dataWorld); 
        
        _eventSystem.EventApply();
        _objectsSystem.RemoveCreatures();
        await _movementSystem.MoveCreaturesInWorld(_world);
        _objectsSystem.ShowCreaturesInMap();
        _objectsSystem.Mutate();
       
        
        await _hubContext.Clients.All.SendAsync("ReceiveUpdate", _world);
        await dataWorld.Save();
        Console.WriteLine($"🕒 Año {_world.YearTime}...");
    }


    private async Task LoadWorld(IDataWorld dataWorld)
    {
        var newWorld = await dataWorld.GetWorldFromJson();
        
        if (newWorld != null)
        { 
            Console.WriteLine("Mundo cargado"); 
            _world.YearTime = newWorld.YearTime; 
            _world.MapObjects = newWorld.MapObjects;
            _world.CreaturePositions = newWorld.CreaturePositions;
            _world.YearTime = newWorld.YearTime;
            _world.ScheduledEvents = newWorld.ScheduledEvents;
            _isWorldLoaded = true;
        }
        else
        {
            Console.WriteLine("Cree criaturas"); 
            _objectsSystem.AddCreatures(3);
        }
    }
}
