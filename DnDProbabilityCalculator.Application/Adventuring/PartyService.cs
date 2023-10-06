﻿using DnDProbabilityCalculator.Core.Adventuring;

namespace DnDProbabilityCalculator.Application.Adventuring;

public class PartyService : IPartyService
{
    private readonly IPartyRepository _repository;

    public PartyService(IPartyRepository repository)
        => _repository = repository;

    public Party Get()
        => _repository.Get();
}