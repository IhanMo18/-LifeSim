using LifeSlim.Core.Model;
using LifeSlim.Core.ValueObjects;

namespace LifeSlim.Application.Interfaces;

public interface IDataWorld
{
    public Task<Creature> GetFromJson(Position position);
    public Task Save();
}