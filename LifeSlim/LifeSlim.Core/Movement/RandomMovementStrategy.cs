using LifeSlim.Core.Interface;
using LifeSlim.Core.Model;
using LifeSlim.Core.ValueObjects;

namespace LifeSlim.Core.Movement;

public class RandomMovementStrategy :IMovementStrategy
{
   private readonly Random _random = new Random();

   public Position NextPosition(World world, Creature creature)
   {
      // Generar un número aleatorio entre 0-3 para las 4 direcciones
      int direction = _random.Next(0, 4);
    
      int newX = creature.Position.X;
      int newY = creature.Position.Y;

      switch (direction)
      {
         case 0: // Derecha
            newX += 1;
            break;
         case 1: // Izquierda
            newX -= 1;
            break;
         case 2: // Arriba
            newY -= 1;
            break;
         case 3: // Abajo
            newY += 1;
            break;
      }

      // Validar límites del mundo
      if (newX < 0 || newX >= world.Width || newY < 0 || newY >= world.Height)
      {
         return creature.Position;
      }
    
      return new Position(newX, newY);
   }
}