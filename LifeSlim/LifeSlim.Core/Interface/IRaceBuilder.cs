using LifeSlim.Core.Model;

namespace LifeSlim.Core.Interface;

public interface IRaceBuilder : IBuilder<Race>
{
    public IRaceBuilder WhitName(string name);
    public IRaceBuilder WhitColorCode(string colorCode);
    public IRaceBuilder WhitStats(int strength, int speed, int vision, int defense, int aggression);
}