using LifeSlim.Application;
using Microsoft.Extensions.Hosting;

namespace LifeSlim.Infrastructure.Simulation;

public class SimulationHostedService : BackgroundService
{
    private readonly SimulationEngine _engine;

    public SimulationHostedService(SimulationEngine engine)
    {
        _engine = engine;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _engine.Tick();
            Console.WriteLine("Tick ejecutado");
            await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken); // Simular cada 10s
        }
    }
}