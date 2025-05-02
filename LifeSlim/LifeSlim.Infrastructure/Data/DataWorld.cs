using LifeSlim.Application.Interfaces;
using LifeSlim.Core.Model;
using LifeSlim.Core.ValueObjects;

namespace LifeSlim.Infrastructure.Data;

public class DataWorld : IDataWorld
{
    private readonly ISerializer _serializer;
    public World _world;


    public DataWorld(ISerializer serializer,World world)
    {
        _serializer = serializer;
        _world = world;
    }


    public Task Save()
    { 
       return _serializer.SaveToJson(_world);
    }

    public Task<Creature> GetFromJson(Position position)
    {
       return _serializer.Get(_world, position);
    }
}