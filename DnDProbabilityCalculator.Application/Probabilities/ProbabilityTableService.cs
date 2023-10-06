using DnDProbabilityCalculator.Core.Adventuring;

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
                StrengthProbability = actor.CalculateSavingThrowSuccessChance(dc, AbilityType.Strength),
                DexterityProbability = actor.CalculateSavingThrowSuccessChance(dc, AbilityType.Dexterity),
                ConstitutionProbability = actor.CalculateSavingThrowSuccessChance(dc, AbilityType.Constitution),
                WisdomProbability = actor.CalculateSavingThrowSuccessChance(dc, AbilityType.Wisdom),
                IntelligenceProbability = actor.CalculateSavingThrowSuccessChance(dc, AbilityType.Intelligence),
                CharismaProbability = actor.CalculateSavingThrowSuccessChance(dc, AbilityType.Charisma)
            }).ToList()
        }).ToList();
    }
}