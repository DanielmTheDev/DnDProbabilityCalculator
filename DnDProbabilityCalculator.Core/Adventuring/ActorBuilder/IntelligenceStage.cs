﻿namespace DnDProbabilityCalculator.Core.Adventuring.ActorBuilder;

public class IntelligenceStage : IIntelligenceStage
{
    private readonly Actor _actor;

    public IntelligenceStage(Actor actor)
        => _actor = actor;

    public ICharismaStage WithIntelligence(int value, bool isProficient = false)
    {
        _actor.AbilityScores.Intelligence = new AbilityScore { Value = value, IsProficient = isProficient };
        return new CharismaStage(_actor);
    }
}