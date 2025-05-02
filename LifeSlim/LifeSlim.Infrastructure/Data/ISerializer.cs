using LifeSlim.Core.Model;
using LifeSlim.Core.ValueObjects;

namespace LifeSlim.Infrastructure.Data;

public interface ISerializer
{
    public Task SaveToJson(World word);
    public Task<Creature> Get(World world,Position position);
}