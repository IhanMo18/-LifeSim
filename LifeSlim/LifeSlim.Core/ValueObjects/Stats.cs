namespace LifeSlim.Core.ValueObjects;

public class Stats
{
   
    public int Strength { get; set; }
    public int Speed { get; set; }
    public int Vision { get; set; }
    public int Defense { get; set; }
    public int Aggression { get; set; }

    public Stats(int strength, int speed, int vision, int defense, int aggression)
    {
        Strength = strength;
        Speed = speed;
        Vision = vision;
        Defense = defense;
        Aggression = aggression;
    }

    public override string ToString()
    {
        return $@"Strength:{Strength}
                Speed:{Speed}
                Vision:{Vision}
                Defense:{Defense}
                Aggression:{Aggression}";
    }
}
