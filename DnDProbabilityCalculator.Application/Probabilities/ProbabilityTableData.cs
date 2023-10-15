using DnDProbabilityCalculator.Core.Adventuring;
using DnDProbabilityCalculator.Core.Adventuring.Abilities;

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
            CharismaRow = CreateRow(actor.AbilityScores.Charisma, dcs, actor.CharismaSavingThrowSuccessChance),
            ConstitutionRow = CreateRow(actor.AbilityScores.Constitution, dcs, actor.ConstitutionSavingThrowSuccessChance),
            DexterityRow = CreateRow(actor.AbilityScores.Dexterity, dcs, actor.DexteritySavingThrowSuccessChance),
            IntelligenceRow = CreateRow(actor.AbilityScores.Intelligence, dcs, actor.IntelligenceSavingThrowSuccessChance),
            StrengthRow = CreateRow(actor.AbilityScores.Strength, dcs, actor.StrengthSavingThrowSuccessChance),
            WisdomRow = CreateRow(actor.AbilityScores.Wisdom, dcs, actor.WisdomSavingThrowSuccessChance),
        };

    private static List<string> CreateRow(AbilityScore abilityScore, IEnumerable<int> dcs, Func<int, double> successChanceMethod)
    {
        var firstCell = $"{abilityScore.Abbreviation} ({abilityScore.Value})";
        return new List<string> { firstCell }.Concat(dcs.Select(dc
            => ((SuccessChanceViewModel)successChanceMethod(dc)).ToString())).ToList();
    }
}