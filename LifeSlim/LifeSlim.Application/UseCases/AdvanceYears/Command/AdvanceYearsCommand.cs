namespace LifeSlim.Application.UseCases.AdvanceYears.Command;

public class AdvanceYearsCommand
{
    public int YearsToAdvance { get; }

    public AdvanceYearsCommand(int years)
    {
        YearsToAdvance = years;
    }
}