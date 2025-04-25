using LifeSlim.Core.EventLoading;
using LifeSlim.Core.Model;
using LifeSlim.Core.System;
using LifeSlim.Infrastructure.Factories;

namespace LifeSlim.Infrastructure.Simulation;

public class SimulationEngine
{
    private readonly MovementSystem _movementSystem;
    private readonly World _world;
    Factory factory

    public SimulationEngine(World world, MovementSystem movementSystem,bool loadDefaultEvents = true)
    {
        _world = world;
        _movementSystem = movementSystem;
        if (loadDefaultEvents) EventLoader.LoadDefaultEvents(_world);
        new Factory(_world);
    }

    public void Tick()
    {
        
        _world.YearTime++;
        Console.WriteLine($"Avanzo un anio del time : {_world.YearTime}");

        var creatureFactory = (RaceFactory)factory().GetFactory("race");
        creatureFactory.Build();
       
        
        
        // 1. Ejecutar eventos globales
        var eventsToTrigger = _world.ScheduledEvents
            .Where(e => e.TriggerYear == _world.YearTime)
            .ToList();
        Console.WriteLine($"Lista de eventoos en ese anio : {eventsToTrigger}");

        foreach (var ev in eventsToTrigger)
        {
            ev.Apply(_world);
            Console.WriteLine("Aplico eventos");
        }
        
        // 2. Mover criaturas
        foreach (var creature in _world.Creatures)
        {
            _movementSystem.Move(_world, creature);
            Console.WriteLine("Muevo criaturas");
        }

        // 3. Reproducción
        
        

        // 4. Envejecimiento y muerte
        foreach (var creature in _world.Creatures)
        {
            creature.Age++;
        }

        _world.Creatures.RemoveAll(c => c.IsAlive==false);
        Console.WriteLine($"🕒 Año {_world.YearTime}...");
    }
}
