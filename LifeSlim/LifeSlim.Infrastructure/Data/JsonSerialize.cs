using System.Text.Json;
using LifeSlim.Core.Model;
using LifeSlim.Core.ValueObjects;

namespace LifeSlim.Infrastructure.Data;

public class JsonSerialize : ISerializer
{
    public Task SaveToJson(World world)
    {
        var json = JsonSerializer.Serialize(world, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText("World.json", json);
        return Task.CompletedTask;
    }

    public Task<Creature> GetCreature(World world,Position position)
    {
        var jsonFromFile = File.ReadAllText("World.json");
        var words = JsonSerializer.Deserialize<List<Creature>>(jsonFromFile);
        var found = words?.FirstOrDefault(w => w.Position == position);
        return Task.FromResult<Creature>(found ?? null);
    }

    public Task<World?> GetWorld()
    {
        var jsonFromFile = File.ReadAllText("World.json");
        return Task.FromResult(JsonSerializer.Deserialize<World>(jsonFromFile));
    }
}