﻿namespace DnDProbabilityCalculator.Core.Adventuring.Abilities;

public record AbilityScores
{
    public Dexterity Dexterity { get; set; } = new();
    public Strength Strength { get; set; } = new();
    public Constitution Constitution { get; set; } = new();
    public Intelligence Intelligence { get; set; } = new();
    public Wisdom Wisdom { get; set; } = new();
    public Charisma Charisma { get; set; } = new();

}