using LifeSlim.Application;
using LifeSlim.Application.GameEngine;
using LifeSlim.Application.Interfaces;
using LifeSlim.Application.UseCases.Race.Commands;
using LifeSlim.Application.UseCases.Race.CommandsHandler;
using LifeSlim.Core.Factories;
using LifeSlim.Core.Interface;
using LifeSlim.Core.Model;
using LifeSlim.Core.Movement;
using LifeSlim.Core.Mutations;
using LifeSlim.Core.System;
using LifeSlim.Infrastructure;
using LifeSlim.Infrastructure.Simulation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSingleton<World>(new World(10, 10)); // Mundo único y compartido
// builder.Services.Scan(scan => scan  //Registra todos los CommandsHandlers
//     .FromAssembliesOf(typeof(CreateRaceCommandHandler))
//     .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<,>)))
//     .AsImplementedInterfaces()
//     .WithScopedLifetime());
builder.Services.AddScoped<ICommandDispatcher,CommandDispatcher>();
builder.Services.AddSingleton<ICreatureFactory, CreatureFactory>();
builder.Services.AddSingleton<IMutationFactory, MutationFactory>();
builder.Services.AddSingleton<MovementStrategyFactory>();
builder.Services.AddSingleton<MutationStrategyFactory>();
builder.Services.AddSingleton<MutationSystem>();
builder.Services.AddSingleton<MovementSystem>();
builder.Services.AddSingleton<SimulationEngine>();
builder.Services.AddHostedService<SimulationHostedService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
    {
        var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                ))
            .ToArray();
        return forecast;
    })
    .WithName("GetWeatherForecast");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}