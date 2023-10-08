using System.Globalization;
using DnDProbabilityCalculator.Core.Adventuring;

namespace DnDProbabilityCalculator.Application.Probabilities;

public class ProbabilityTableService : IProbabilityTableService
{
    private readonly IPartyRepository _repository;

    public ProbabilityTableService(IPartyRepository repository)
        => _repository = repository;

    public IList<ProbabilityTableData> Get(params int[] dcs)
    {
        var party = _repository.Get();
        return party.Characters.Select(actor => new ProbabilityTableData
        {
            ActorName = actor.Name,
            DCs = dcs,
            CharismaProbabilities = dcs.Select(actor.CharismaSavingThrowSuccessChance).ToList(),
            ConstitutionProbabilities = dcs.Select(actor.ConstitutionSavingThrowSuccessChance).ToList(),
            DexterityProbabilities = dcs.Select(actor.DexteritySavingThrowSuccessChance).ToList(),
            IntelligenceProbabilities = dcs.Select(actor.IntelligenceSavingThrowSuccessChance).ToList(),
            StrengthProbabilities = dcs.Select(actor.StrengthSavingThrowSuccessChance).ToList(),
            WisdomProbabilities = dcs.Select(actor.WisdomSavingThrowSuccessChance).ToList()

        }).ToList();
    }
}