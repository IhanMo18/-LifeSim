using LifeSlim.Core.Model;
using LifeSlim.Core.System;

namespace LifeSlim.Application;

public class SimulationEngine
{
    private readonly MovementSystem _movementSystem;
    private readonly World _world;

    public SimulationEngine(World world, MovementSystem movementSystem)
    {
        _world = world;
        _movementSystem = movementSystem;
    }

    public void Tick()
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

        // 2. Mover criaturas
        foreach (var creature in _world.Creatures)
        {
            _movementSystem.Move(_world, creature);
        }

        // 3. Reproducción

        // 4. Envejecimiento y muerte
        foreach (var creature in _world.Creatures)
        {
            creature.Age++;
        }

        _world.Creatures.RemoveAll(c => c.IsAlive()==false);
        Console.WriteLine($"🕒 Año {_world.YearTime}...");
        // TODO: Guardar estado o hacer log si quieres
    }
}
