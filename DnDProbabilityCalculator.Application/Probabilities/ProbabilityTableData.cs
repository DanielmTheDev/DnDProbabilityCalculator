using DnDProbabilityCalculator.Core.Adventuring;

namespace DnDProbabilityCalculator.Application.Probabilities;

public record ProbabilityTableData
{
    private ProbabilityTableData()
    {
    }
    public required string Header { get; set; }
    public required List<string> DcRow { get; set; }
    public required List<string> StrengthRow { get; set; }
    public required List<string> DexterityRow { get; set; }
    public required List<string> ConstitutionRow { get; set; }
    public required List<string> WisdomRow { get; set; }
    public required List<string> IntelligenceRow { get; set; }
    public required List<string> CharismaRow { get; set; }

    public List<List<string>> AllRows
        => new()
        {
            StrengthRow,
            DexterityRow,
            ConstitutionRow,
            WisdomRow,
            IntelligenceRow,
            CharismaRow
        };

    public static ProbabilityTableData FromActor(Actor actor, int[] dcs)
        => new()
        {
            Header = actor.Name,
            DcRow = new List<string> { "DC" }.Concat(dcs.Select(dc => dc.ToString())).ToList(),
            CharismaRow = CreateRow(dcs, "Cha", actor.CharismaSavingThrowSuccessChance),
            ConstitutionRow = CreateRow(dcs, "Con", actor.ConstitutionSavingThrowSuccessChance),
            DexterityRow = CreateRow(dcs, "Dex", actor.DexteritySavingThrowSuccessChance),
            IntelligenceRow = CreateRow(dcs, "Int", actor.IntelligenceSavingThrowSuccessChance),
            StrengthRow = CreateRow(dcs, "Str", actor.StrengthSavingThrowSuccessChance),
            WisdomRow = CreateRow(dcs, "Wis", actor.WisdomSavingThrowSuccessChance),
        };

    private static List<string> CreateRow(IEnumerable<int> dcs, string abilityScore, Func<int, double> successChanceMethod)
        => new List<string> { abilityScore }.Concat(dcs.Select(dc
            => ((SuccessChanceViewModel)successChanceMethod(dc)).ToString())).ToList();
}