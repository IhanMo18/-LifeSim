using LifeSlim.Core.ValueObjects;

namespace LifeSlim.Core.Model;

public class MapObject(Position position)
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Position Position { get; set; } = position;
}