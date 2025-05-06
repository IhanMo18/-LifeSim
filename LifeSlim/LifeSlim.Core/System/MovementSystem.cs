using LifeSlim.Core.Model;

public class MovementSystem(MovementStrategyFactory strategyFactory)
{
    public async Task Move(World world, Creature creature)
    {
        var currentKey = $"{creature.Position.X},{creature.Position.Y}";
        world.CreaturePositions.Remove(currentKey); // Elimina la posición actual

        var strategy = strategyFactory.GetStrategy(creature);
        var nextPosition = await strategy.NextPosition(world, creature);
        var newKey = $"{nextPosition.X},{nextPosition.Y}";

        while (world.CreaturePositions.ContainsKey(newKey))
        {
            strategy = strategyFactory.GetStrategy(creature);
            nextPosition = await strategy.NextPosition(world, creature);
            newKey = $"{nextPosition.X},{nextPosition.Y}";
        }

        creature.Position = nextPosition;
        world.CreaturePositions[newKey] = creature.Id.ToString();
    }


    public async void MoveCreaturesInWorld(World world)
    {
        foreach (var creature in world.Creatures)
        {
            await Move(world, creature);
            creature.AgeOneYear();                      
        }
    }
}