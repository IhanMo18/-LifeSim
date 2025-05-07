using LifeSlim.Core.Model;
using LifeSlim.Core.ValueObjects;

namespace LifeSlim.Application.Interfaces;

public interface IDataWorld
{
    public Task<Creature> GetCreatureFromJson(Position position);
    
    public Task<World?> GetWorldFromJson();
    public Task Save();
}