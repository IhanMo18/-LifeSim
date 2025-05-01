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
            await _engine.Tick();
            Console.WriteLine("Tick ejecutado");
            await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken); // Simular cada 10s
        }
    }
}