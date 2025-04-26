using LifeSlim.Application.Interfaces;
using LifeSlim.Core.ValueObjects;

namespace LifeSlim.Application.UseCases.Race.Commands;

public class CreateRaceCommand : ICommand<Core.Model.Race>
{
    public string? Name { get; set; }
    public string? ColorCode {get; set;}
    public Stats? BaseStats { get; set; }
}