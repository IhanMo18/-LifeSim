using Newtonsoft.Json;
using LifeSlim.Core.Model;
using LifeSlim.Core.ValueObjects;

namespace LifeSlim.Infrastructure.Data;

public class JsonSerialize : ISerializer
{
    public Task SaveToJson(World world)
    {
        var json = JsonConvert.SerializeObject(world, Formatting.Indented);
        File.WriteAllText("World.json", json);
        return Task.CompletedTask;
    }

    public Task<Creature> GetCreature(World world, Position position)
    {
        var jsonFromFile = File.ReadAllText("World.json");
        var creatures = JsonConvert.DeserializeObject<List<Creature>>(jsonFromFile);
        var found = creatures?.FirstOrDefault(w => w.Position == position);
        return Task.FromResult(found);
    }

    public Task<World?> GetWorld()
    {
        try
        {
            var jsonFromFile = File.ReadAllText("World.json");
            var world = JsonConvert.DeserializeObject<World>(jsonFromFile);
            return Task.FromResult(world);
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("🌍 Archivo World.json no encontrado. Se usará el mundo por defecto.");
            return Task.FromResult<World?>(null);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Error al cargar el mundo: {ex.Message}");
            return Task.FromResult<World?>(null);
        }
    }

}