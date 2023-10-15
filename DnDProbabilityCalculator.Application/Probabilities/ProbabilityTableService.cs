﻿using DnDProbabilityCalculator.Core.Adventuring;

namespace DnDProbabilityCalculator.Application.Probabilities;

public class ProbabilityTableService : IProbabilityTableService
{
    private readonly IPartyRepository _repository;

    public ProbabilityTableService(IPartyRepository repository)
        => _repository = repository;

    public List<ProbabilityTableData> Get(int[] dcs, int[] attackModifiers)
    {
        var party = _repository.Get();
        return party.Characters.Select(actor => ProbabilityTableData.FromActor(actor, dcs, attackModifiers)).ToList();
    }
}