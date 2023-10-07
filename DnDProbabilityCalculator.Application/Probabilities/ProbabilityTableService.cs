﻿using DnDProbabilityCalculator.Core.Adventuring;

namespace DnDProbabilityCalculator.Application.Probabilities;

public class ProbabilityTableService : IProbabilityTableService
{
    private readonly IPartyRepository _repository;

    public ProbabilityTableService(IPartyRepository repository)
        => _repository = repository;

    public ICollection<ProbabilityTable> Get(params int[] dcs)
    {
        var party = _repository.Get();
        return party.Characters.Select(actor => new ProbabilityTable
        {
            ActorName = actor.Name,
            DCs = dcs.Select(dc => new DCProbability
            {
                DC = dc,
                StrengthProbability = actor.StrengthSavingThrowSuccessChance(dc),
                DexterityProbability = actor.DexteritySavingThrowSuccessChance(dc),
                ConstitutionProbability = actor.ConstitutionSavingThrowSuccessChance(dc),
                WisdomProbability = actor.WisdomSavingThrowSuccessChance(dc),
                IntelligenceProbability = actor.IntelligenceSavingThrowSuccessChance(dc),
                CharismaProbability = actor.CharismaSavingThrowSuccessChance(dc)
            }).ToList()
        }).ToList();
    }
}