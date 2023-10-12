using System.Globalization;
using DnDProbabilityCalculator.Core.Adventuring;

namespace DnDProbabilityCalculator.Application.Probabilities;

public class ProbabilityTableService : IProbabilityTableService
{
    private readonly IPartyRepository _repository;

    public ProbabilityTableService(IPartyRepository repository)
        => _repository = repository;

    // todo: move into row?
    public List<ProbabilityTableData> Get(params int[] dcs)
    {
        var party = _repository.Get();
        return party.Characters.Select(actor => new ProbabilityTableData
        {
            HeaderRow = new List<string>{ actor.Name }.Concat(dcs.Select(dc => dc.ToString())).ToList(),
            CharismaRow = CreateRow(dcs, "Cha", actor.CharismaSavingThrowSuccessChance),
            ConstitutionRow = CreateRow(dcs, "Con", actor.ConstitutionSavingThrowSuccessChance),
            DexterityRow = CreateRow(dcs, "Dex", actor.DexteritySavingThrowSuccessChance),
            IntelligenceRow = CreateRow(dcs, "Int", actor.IntelligenceSavingThrowSuccessChance),
            StrengthRow = CreateRow(dcs, "Str", actor.StrengthSavingThrowSuccessChance),
            WisdomRow = CreateRow(dcs, "Wis", actor.WisdomSavingThrowSuccessChance),
        }).ToList();
    }

    private static List<string> CreateRow(IEnumerable<int> dcs, string abilityScore, Func<int, double> successChanceMethod)
        => new List<string> { abilityScore }.Concat(dcs.Select(dc => successChanceMethod(dc).ToString(CultureInfo.InvariantCulture))).ToList();
}