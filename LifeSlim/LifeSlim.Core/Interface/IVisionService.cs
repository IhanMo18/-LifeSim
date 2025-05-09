using LifeSlim.Core.Model;
using LifeSlim.Core.ValueObjects;

namespace LifeSlim.Core.Interface;

public interface IVisionService
{

        public List<MapObject?> FindNearbyMapObjects(World world, Creature creature, int vision);
}