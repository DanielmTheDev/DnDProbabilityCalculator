// See https://aka.ms/new-console-template for more information

using DnDProbabilityCalculator;
using Microsoft.Extensions.DependencyInjection;

var serviceProvider = new ServiceCollection()
    .AddScoped<ICalculator, Calculator>()
    .BuildServiceProvider();

var calculator = serviceProvider.GetService<ICalculator>();

Console.WriteLine("Hello, World!");