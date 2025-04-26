namespace LifeSlim.Application.Interfaces;

public interface ICommandHandler<in TCommand,TResult>
{
    Task<TResult> Handle(TCommand command);    
}