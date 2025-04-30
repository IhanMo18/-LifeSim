using LifeSlim.Core.Interface;
using LifeSlim.Core.Model;
using LifeSlim.Core.ValueObjects;

namespace LifeSlim.Core.Builders;

public class RaceBuilder : IRaceBuilder
{
    private string _name;
    private string _colorCode;
    private Stats _stats;
    
    public IRaceBuilder WhitName(string name)
    {
        _name = name;
        return this;
    }

    public IRaceBuilder WhitColorCode(string colorCode)
    {
        _colorCode = colorCode;
        return this;
    }

    public IRaceBuilder WhitStats(int strength, int speed, int vision, int defense, int aggression)
    {
       _stats =  new Stats(strength, speed, vision, defense, aggression);
       return this;
    }
    
    
    public Race Build()
    {
         return new Race(_name,_colorCode,_stats);
    }
}