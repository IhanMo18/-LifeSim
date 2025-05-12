using LifeSlim.Core.Builders;
using LifeSlim.Core.Interface;
using LifeSlim.Core.Model;

namespace LifeSlim.Core.System;

public class ObjectsSystem(World world, ICreatureFactory creatureFactory, IFoodFactory foodFactory)
{
    private static int RandomBetween(int a, int b)
    {
        var min = Math.Min(a, b);
        var max = Math.Max(a, b);
        return Random.Shared.Next(min, max + 1);
    }

    public void AddCreatures(int amount)
    {
        while (world.MapObjects.OfType<Creature>().Count() < amount)
        {
            try
            {
                var race = new RaceBuilder().WhitName("Orco")
                    .WhitColorCode("#fffef")
                    .WhitStats(RandomBetween(1,10), RandomBetween(1,10), RandomBetween(1,10), RandomBetween(1,10), RandomBetween(1,10))
                    .Build();

                var creature = creatureFactory.CreateCreature(world, race);
                world.MapObjects.Add(creature);
                world.CreaturePositions.Add($"{creature.Position.X},{creature.Position.Y}", creature.Id.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear criatura: {ex.Message}");
            }
        }
        AddFoodByCreature();
    }

    public void RemoveCreatures()
    {
        world.MapObjects.OfType<Creature>().ToList().RemoveAll(c => c.IsAlive==false);
    }
    
    private void AddFoodByCreature()
    {
        if (world.MapObjects.OfType<Food>().Any()) return;
        var foodCount = world.MapObjects.OfType<Creature>().Count() * 2;
        
        for (var i = 0; i < foodCount; i++)
        {
            try
            {
                var food = foodFactory.CreateFood();
                world.MapObjects.Add(food);
                world.CreaturePositions.Add($"{food.Position.X},{food.Position.Y}", food.Id.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error al crear la comida : {e.Message}");
            }
            
        }
    }
    
    
    public void ShowMap()
    {
        for (int y = 0; y < world.Height; y++)
        {
            for (int x = 0; x < world.Width; x++)
            {
                var mapObject = world.MapObjects
                    .FirstOrDefault(obj => obj.Position.X == x && obj.Position.Y == y);

                if (mapObject != null)
                {
                    if (mapObject.ObjType == "Creature")
                        Console.Write("*");
                    else if (mapObject.ObjType == "Food")
                        Console.Write("#");
                    else
                        Console.Write("?");
                }
                else
                {
                    Console.Write("-");
                }
            }
            Console.WriteLine();
        }
    }



}