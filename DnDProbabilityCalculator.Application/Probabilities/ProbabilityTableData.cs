using DnDProbabilityCalculator.Core.Adventuring;
using DnDProbabilityCalculator.Core.Adventuring.Abilities;

namespace DnDProbabilityCalculator.Application.Probabilities;

public record ProbabilityTableData
{
    private ProbabilityTableData() { }

    public required string Header { get; init; }
    public required IEnumerable<string> DcRow { get; set; }
    public required List<IEnumerable<string>> SavingThrowRows { get; set; }
    public required List<IEnumerable<string>> GetHitRows{ get; set; }

    public static ProbabilityTableData FromActor(Actor actor, int[] dcs, int[] attackModifiers)
    {
        ValidateSameNumberOfElements(dcs, attackModifiers);
        var dcRow = new List<string> { "Ability/AC" }.Concat(dcs.Select(dc => dc.ToString())).ToList();
        var savingThrowRows = Enum.GetValues<AbilityScoreType>()
            .Select(abilityScoreType => CreateRow(actor, abilityScoreType, dcs));

        return new()
        {
            Header = actor.Name,
            DcRow = dcRow,
            GetHitRows = new(),
            SavingThrowRows = savingThrowRows.ToList()
        };
    }

    private static void ValidateSameNumberOfElements(int[] dcs, int[] attackModifiers)
    {
        if (dcs.Length != attackModifiers.Length)
        {
            throw new ArgumentException("The number of dcs and attack modifiers must be the same.");
        }
    }

    private static IEnumerable<string> CreateRow(Actor actor, AbilityScoreType abilityScoreType, IEnumerable<int> dcs)
    {
        var abilityScore = actor.AbilityScores.Get(abilityScoreType);
        var firstCell = $"{abilityScore.Abbreviation} ({abilityScore.Value})";
        return new List<string> { firstCell }.Concat(dcs.Select(dc
            => ((SuccessChanceViewModel)actor.SavingThrowSuccessChance(abilityScoreType, dc)).ToString())).ToList();
    }
}