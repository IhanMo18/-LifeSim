using LifeSlim.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace LifeSlim.Infrastructure;

public class CommandDispatcher : ICommandDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public CommandDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public Task<TResult> Send<TCommand, TResult>(TCommand command) where TCommand : ICommand<TResult>
    {
        var scope = _serviceProvider.CreateScope();
        var handler = scope.ServiceProvider.GetService<ICommandHandler<TCommand, TResult>>();
        return handler!.Handle(command);
    }
}