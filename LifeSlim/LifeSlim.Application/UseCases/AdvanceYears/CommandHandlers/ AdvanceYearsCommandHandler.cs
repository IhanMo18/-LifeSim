using LifeSlim.Application.GameEngine;
using LifeSlim.Application.UseCases.AdvanceYears.Command;

namespace LifeSlim.Application.UseCases.AdvanceYears.CommandHandlers;

public class AdvanceYearsCommandHandler
{
    private readonly SimulationEngine _engine;

    public AdvanceYearsCommandHandler(SimulationEngine engine)
    {
        _engine=engine;
    }

    // public void Handle(AdvanceYearsCommand command)
    // {
    //     for (int i = 0; i < command.YearsToAdvance; i++)
    //     {
    //        _engine.Tick(); // Aquí correrías un año de simulación
    //     }
    // }
}