namespace LifeSlim.Core.Interface;

public interface IBuilder<out T>
{
    T Build();
}