using LifeSlim.Core.Model;
using LifeSlim.Core.Services;
using LifeSlim.Core.ValueObjects;

namespace LifeSlim.Core.System;

public static class ReproductionSystem
{
   
    
    public static void Reproduce(World world,Creature creature,List<Creature> newCreatures)
    {
        var posicionesAdyacentes = new List<Position>
        {
            new Position(creature.Position.X - 1, creature.Position.Y), // Izquierda
            new Position(creature.Position.X + 1, creature.Position.Y), // Derecha
            new Position(creature.Position.X, creature.Position.Y - 1), // Arriba
            new Position(creature.Position.X, creature.Position.Y + 1)  // Abajo
        };

        var criaturasCercanas = new List<Creature>();
        foreach (var pos in posicionesAdyacentes)
        {
            if (pos.X >= 0 && pos.X < world.Width && pos.Y >= 0 && pos.Y < world.Height)
            {
                if(world.CreaturePositions.TryGetValue($"{pos.X},{pos.Y}",out var idEnCelda))
                {
                    var criaturaAdyacente = world.MapObjects.OfType<Creature>().FirstOrDefault(c => c.Id.ToString() == idEnCelda);
                    
                    if (criaturaAdyacente != null && criaturaAdyacente != creature)
                    {
                        criaturasCercanas.Add(criaturaAdyacente);
                    }
                }
            }
        }
        
        if (criaturasCercanas.Count >= 2)
        {
            var pareja = criaturasCercanas[Random.Shared.Next(criaturasCercanas.Count)];
            if (creature.CanReproduce() && pareja.CanReproduce())
            {
                try
                {
                    var hijo = ReproductionService.Reproduce(world, creature, pareja);
                    newCreatures.Add(hijo);
                    world.CreaturePositions[$"{hijo.Position.X},{hijo.Position.Y}"] = hijo.Id.ToString();
                    Console.WriteLine($"¡{creature.Id} se reprodujo con {pareja.Id}!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al reproducirse: {ex.Message}");
                }
            }
        }
    }
    
}