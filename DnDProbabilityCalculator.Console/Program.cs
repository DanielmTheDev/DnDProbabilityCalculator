﻿// var serviceProvider = new ServiceCollection()
//     .AddScoped<ICalculator, Calculator>()
//     .BuildServiceProvider();
//
// var calculator = serviceProvider.GetService<ICalculator>();

using DnDProbabilityCalculator.Core.Adventuring;
using Dumpify;

var jsonString = File.ReadAllText(@"C:\Users\DanielMuckelbauer\AppData\Roaming\JetBrains\Rider2023.2\scratches\scratch.json");
var party = Party.FromJsonString(jsonString);
party.Dump();