using LifeSlim.Core.Model;
using LifeSlim.Core.ValueObjects;

namespace LifeSlim.Infrastructure.Data;

public interface ISerializer
{
    public Task SaveToJson(World word);
    public Task<Creature> GetCreature(World world,Position position);
    
    public Task<World?> GetWorld();
}