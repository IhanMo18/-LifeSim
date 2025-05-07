using System.Text.Json;
using LifeSlim.Application;
using LifeSlim.Application.GameEngine;
using LifeSlim.Application.Hubs;
using LifeSlim.Application.Interfaces;
using LifeSlim.Application.Service;
using LifeSlim.Application.UseCases.Race.CommandsHandler;
using LifeSlim.Core.Factories;
using LifeSlim.Core.Interface;
using LifeSlim.Core.Model;
using LifeSlim.Core.Movement;
using LifeSlim.Core.Mutations;
using LifeSlim.Core.System;
using LifeSlim.Infrastructure;
using LifeSlim.Infrastructure.Data;
using LifeSlim.Infrastructure.Simulation;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers()
    .AddNewtonsoftJson();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSignalR(options => options.EnableDetailedErrors = true );


builder.Services.AddSingleton<World>(new World(30, 30)); // Mundo único y compartido

builder.Services.Scan(scan => scan  //Registra todos los CommandsHandlers
    .FromAssembliesOf(typeof(CreateRaceCommandHandler))
    .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<,>)))
    .AsImplementedInterfaces()
    .WithScopedLifetime());

builder.Services.AddScoped<ICommandDispatcher,CommandDispatcher>();
builder.Services.AddSingleton<ICreatureFactory, CreatureFactory>();
builder.Services.AddSingleton<IMutationFactory, MutationFactory>();
builder.Services.AddSingleton<IFoodFactorie, FoodFactorie>();
builder.Services.AddSingleton<MovementStrategyFactory>();
builder.Services.AddSingleton<MutationStrategyFactory>();
builder.Services.AddSingleton<MutationSystem>();
builder.Services.AddSingleton<MovementSystem>();
builder.Services.AddSingleton<EventSystem>();
builder.Services.AddSingleton<ObjectsSystem>();
builder.Services.AddSingleton<SimulationEngine>();
builder.Services.AddScoped<ISerializer, JsonSerialize>();
builder.Services.AddScoped<IDataWorld, DataWorld>();
builder.Services.AddScoped<IVisionService, VisionService>();
builder.Services.AddHostedService<SimulationHostedService>();

// En Program.cs
builder.Services.AddCors(options => {
    options.AddPolicy("AllowFrontend", policy => {
        policy.WithOrigins("http://localhost:63342") // Origen de tu frontend (ajusta el puerto)
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials(); // Solo si usas cookies o autenticación
    });
});



var app = builder.Build();
app.UseWebSockets(); 
app.UseCors("AllowFrontend");
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

app.MapHub<GameHub>("/gameHub");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}