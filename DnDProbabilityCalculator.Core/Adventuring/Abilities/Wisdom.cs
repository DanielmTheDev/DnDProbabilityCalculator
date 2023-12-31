﻿namespace DnDProbabilityCalculator.Core.Adventuring.Abilities;

public record Wisdom : AbilityScore
{
    public override AbilityScoreType Type => AbilityScoreType.Wisdom;
    public static implicit operator Wisdom(int value) => new() { Value = value };
}