using System.Collections.Generic;
using LifeSlim.Core.Model;
using LifeSlim.Core.ValueObjects;

namespace LifeSlim.Core.Pathfinding;

public static class BfsNavigator
{
        public static Position FindNearestCreature(World world, Creature searcher, int maxDepth)
        {
            var queue = new Queue<(Position pos, int depth)>();
            var visited = new HashSet<Position>();

            queue.Enqueue((searcher.Position, 0));
            visited.Add(searcher.Position);

            while (queue.Count > 0)
            {
                var (currentPos, depth) = queue.Dequeue();

                if (depth > maxDepth)
                    continue;

                if (world.IsCreatureAt(currentPos, out var foundCreature))
                {
                    if (foundCreature.Id != searcher.Id)
                    {
                        // ✨ AQUI escribimos el mensaje que querías
                        Console.WriteLine($"📢 Criatura {searcher.Id} encontró a la criatura {foundCreature.Id} en la posición ({currentPos.X}, {currentPos.Y}) durante la caza.");

                        return currentPos;
                    }
                }

                foreach (var neighbor in GetNeighbors(currentPos))
                {
                    if (world.IsPositionValid(neighbor.X, neighbor.Y) && !visited.Contains(neighbor))
                    {
                        queue.Enqueue((neighbor, depth + 1));
                        visited.Add(neighbor);
                    }
                }
            }

            return null;
        }

        private static IEnumerable<Position> GetNeighbors(Position pos)
        {
            yield return new Position(pos.X + 1, pos.Y);
            yield return new Position(pos.X - 1, pos.Y);
            yield return new Position(pos.X, pos.Y + 1);
            yield return new Position(pos.X, pos.Y - 1);
        }
        
}