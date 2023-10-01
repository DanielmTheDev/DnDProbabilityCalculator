using System.Text.Json;
using DnDProbabilityCalculator.Core;

namespace DnDProbabilityCalculator;

public class Character
{
    public string Name { get; set; } = string.Empty;
    public Attributes Attributes { get; set; } = new();
}