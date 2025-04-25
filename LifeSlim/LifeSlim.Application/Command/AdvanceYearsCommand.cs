namespace LifeSlim.Application.Command;

public class AdvanceYearsCommand : ICommand
{
    public int YearsToAdvance { get; }

    public AdvanceYearsCommand(int years)
    {
        YearsToAdvance = years;
    }
}