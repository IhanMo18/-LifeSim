using LifeSlim.Application.Command;
using LifeSlim.Application.GameEngine;
using LifeSlim.Core.Model;

namespace LifeSlim.Application.CommandHandlers;

public class AdvanceYearsCommandHandler
{
    private readonly SimulationEngine _engine;

    public AdvanceYearsCommandHandler(SimulationEngine engine)
    {
        _engine=engine;
    }

    public void Handle(AdvanceYearsCommand command)
    {
        for (int i = 0; i < command.YearsToAdvance; i++)
        {
           _engine.Tick(); // Aquí correrías un año de simulación
        }
    }
}