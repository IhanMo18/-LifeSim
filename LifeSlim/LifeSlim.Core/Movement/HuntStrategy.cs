using LifeSlim.Core.Interface;
using LifeSlim.Core.Model;
using LifeSlim.Core.Pathfinding;
using LifeSlim.Core.ValueObjects;

namespace LifeSlim.Core.Movement;

public class HuntStrategy : IMovementStrategy
{
    public Position NextPosition(World world, Creature creature)
    {
        var targetPos = BfsNavigator.FindNearestCreature(world, creature,creature.Dna.Stats.Vision);

        if (targetPos == null) return new RandomMovementStrategy().NextPosition(world, creature);
        
        
        if (world.IsCreatureAt(targetPos, out var targetCreature))
        {
            // Aquí intentamos predecir hacia dónde irá la criatura
            var predictedPos = PredictNextPosition(targetCreature);
            Console.WriteLine($"Predicted position: {predictedPos}");

            // Ahora el cazador se mueve hacia la posición predicha
            var dx = Math.Clamp(predictedPos.X - creature.Position.X, -1, 1);
            var dy = Math.Clamp(predictedPos.Y - creature.Position.Y, -1, 1);
            Console.WriteLine($"Me mueevo hacia la posicion predicha");

            return new Position(creature.Position.X + dx, creature.Position.Y + dy);
        }
        else
        {
            // Si por alguna razón no encontramos criatura (por ejemplo, ya se movió), movemos hacia targetPos normal
            var dx = Math.Clamp(targetPos.X - creature.Position.X, -1, 1);
            var dy = Math.Clamp(targetPos.Y - creature.Position.Y, -1, 1);
            Console.WriteLine($"movemos hacia targetPos normal");

            return new Position(creature.Position.X + dx, creature.Position.Y + dy);
        }

        // Si no encuentra objetivos, usa movimiento aleatorio
    }

    private Position PredictNextPosition(Creature target)
    {
        // Esta predicción es súper básica: asume que el objetivo sigue moviéndose en la misma dirección
        return new Position(
            target.Position.X + target.CurrentDirection.X,
            target.Position.Y + target.CurrentDirection.Y
        );
    }
}