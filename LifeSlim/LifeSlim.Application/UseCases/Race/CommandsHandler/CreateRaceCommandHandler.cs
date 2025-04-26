using LifeSlim.Application.Interfaces;
using LifeSlim.Application.UseCases.Race.Commands;
using LifeSlim.Core.Model;

namespace LifeSlim.Application.UseCases.Race.CommandsHandler;

public class CreateRaceCommandHandler : ICommandHandler<CreateRaceCommand, Core.Model.Race>
{
    public Task<Core.Model.Race> Handle(CreateRaceCommand command)
    {
        Core.Model.Race newRace = new Core.Model.Race(command.Name, command.ColorCode, command.BaseStats);
        return Task.FromResult(newRace);
    }
}