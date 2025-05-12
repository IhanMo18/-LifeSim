using System.Windows.Input;
using LifeSlim.Application;
using LifeSlim.Application.GameEngine;
using LifeSlim.Application.Interfaces;
using LifeSlim.Application.UseCases.Race.Commands;
using LifeSlim.Application.UseCases.Race.CommandsHandler;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LifeSlim.Infrastructure.Simulation;

public class SimulationHostedService : BackgroundService
{
    private readonly SimulationEngine _engine;
    private readonly IServiceProvider _serviceProvider;

    public SimulationHostedService(SimulationEngine engine, IServiceProvider serviceProvider)
    {
        _engine = engine;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var scope = _serviceProvider.CreateScope();
            var dataWorld = scope.ServiceProvider.GetRequiredService<IDataWorld>();
            await _engine.Tick(dataWorld);
            Console.WriteLine("Tick ejecutado");
            await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken); 
        }
    }
}