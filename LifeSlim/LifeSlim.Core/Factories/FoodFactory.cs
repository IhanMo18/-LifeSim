using LifeSlim.Core.Interface;
using LifeSlim.Core.Model;
using LifeSlim.Core.ValueObjects;

namespace LifeSlim.Core.Factories;

public class FoodFactory : IFoodFactory
{
    private readonly World _world;

    public FoodFactory(World world)
    {
        _world = world;
    }

    public Food CreateFood()
    {
        var position = _world.GenerateFreePosition();
        return new Food(position);
    }
}