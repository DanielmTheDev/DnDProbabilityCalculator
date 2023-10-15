using DnDProbabilityCalculator.Core.Adventuring;
using DnDProbabilityCalculator.Core.Adventuring.Abilities;

namespace DnDProbabilityCalculator.Application.Probabilities;

public record ProbabilityTableData
{
    private ProbabilityTableData()
    {
    }

    public required string Header { get; init; }
    public required List<IEnumerable<string>> AbilityRows { get; set; }

    public required IEnumerable<string> DcRow { get; set; }

    public static ProbabilityTableData FromActor(Actor actor, int[] dcs)
    {
        var dcRow = new List<string> { "Ability/AC" }.Concat(dcs.Select(dc => dc.ToString())).ToList();
        var abilityRows = Enum.GetValues<AbilityScoreType>()
            .Select(abilityScoreType => CreateRow(actor, abilityScoreType, dcs));

        return new()
        {
            Header = actor.Name,
            DcRow = dcRow,
            AbilityRows = abilityRows.ToList()
        };
    }

    private static IEnumerable<string> CreateRow(Actor actor, AbilityScoreType abilityScoreType, IEnumerable<int> dcs)
    {
        var abilityScore = actor.AbilityScores.Get(abilityScoreType);
        var firstCell = $"{abilityScore.Abbreviation} ({abilityScore.Value})";
        return new List<string> { firstCell }.Concat(dcs.Select(dc
            => ((SuccessChanceViewModel)actor.SavingThrowSuccessChance(abilityScoreType, dc)).ToString())).ToList();
    }
}