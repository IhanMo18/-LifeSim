
using LifeSlim.Application.Hubs;
using LifeSlim.Application.Interfaces;
using LifeSlim.Core.Model;
using LifeSlim.Core.System;
using Microsoft.AspNetCore.SignalR;


namespace LifeSlim.Application.GameEngine;

public class SimulationEngine(
    World world,
    MovementSystem movementSystem,
    EventSystem eventSystem,
    ObjectsSystem objectsSystem,
    IHubContext<GameHub> hubContext)
{
    private bool _isWorldLoaded = false;


    public async Task Tick(IDataWorld dataWorld)
    {
        
        if (!_isWorldLoaded) await LoadWorld(dataWorld); 
        eventSystem.EventApply();
        objectsSystem.RemoveCreatures();
        await movementSystem.MoveCreaturesInWorld(world);
        objectsSystem.ShowMap();
        await hubContext.Clients.All.SendAsync("ReceiveUpdate", world);
        await dataWorld.Save();
        Console.WriteLine($"🕒 Año {world.YearTime}...");
    }


    private async Task LoadWorld(IDataWorld dataWorld)
    {
        var newWorld = await dataWorld.GetWorldFromJson();
        
        if (newWorld != null)
        { 
            Console.WriteLine("Mundo cargado"); 
            world.YearTime = newWorld.YearTime; 
            world.MapObjects = newWorld.MapObjects;
            world.CreaturePositions = newWorld.CreaturePositions;
            world.YearTime = newWorld.YearTime;
            world.ScheduledEvents = newWorld.ScheduledEvents;
            _isWorldLoaded = true;
        }
        else
        {
            Console.WriteLine("Cree 3 criaturas"); 
            objectsSystem.AddCreatures(3);
        }
    }
}
