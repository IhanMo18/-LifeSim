using LifeSlim.Core.Model;
using LifeSlim.Core.ValueObjects;

namespace LifeSlim.Core.Interface;

public interface IVisionService
{

        public Task<List<Creature?>> FindCreaturesByVision(World world, Creature creature, int vision);
}