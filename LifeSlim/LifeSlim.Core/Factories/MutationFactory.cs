using System.Collections;
using LifeSlim.Core.Interface;
using LifeSlim.Core.Util;
using LifeSlim.Core.ValueObjects;

namespace LifeSlim.Core.Factories;

public class MutationFactory : IMutationFactory
{
    
    private readonly ArrayList _statTypes =
    [
        StatType.Strength,
        StatType.Defense,
        StatType.Aggression,
        StatType.Speed,
        StatType.Vision
    ];
    
    public Mutation CreateMutation()
    {
        Random random = new Random();
        var statTypesCount = _statTypes.Count;
        var randomStatType = (StatType)_statTypes[random.Next(statTypesCount)]!;
        
        return new Mutation(
            randomStatType,
            random.Next(5),
            random.NextDouble()
        );
    }
}