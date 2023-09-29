// var serviceProvider = new ServiceCollection()
//     .AddScoped<ICalculator, Calculator>()
//     .BuildServiceProvider();
//
// var calculator = serviceProvider.GetService<ICalculator>();

using DnDProbabilityCalculator;
using Dumpify;

var jsonString = File.ReadAllText(@"C:\Users\DanielMuckelbauer\AppData\Roaming\JetBrains\Rider2023.2\scratches\scratch.json");
var character = Character.FromJsonString(jsonString);
character.Dump();