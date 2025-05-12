
using JsonSubTypes;
using LifeSlim.Core.ValueObjects;
using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json;

namespace LifeSlim.Core.Model;

[JsonConverter(typeof(JsonSubtypes), "ObjType")]
[JsonSubtypes.KnownSubType(typeof(Creature), "Creature")]
[JsonSubtypes.KnownSubType(typeof(Food), "Food")]
public abstract class MapObject(Position position)
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Position Position { get; set; } = position;
    
    public abstract  string ObjType { get; }
}