using DnDProbabilityCalculator.Core.Adventuring;
using DnDProbabilityCalculator.Core.Adventuring.Abilities;

namespace DnDProbabilityCalculator.Application.Probabilities;

public record ProbabilityTableData
{
    private ProbabilityTableData() { }

    public required string Header { get; init; }
    public required IEnumerable<string> DcRow { get; init; }
    public required List<string> AttackModifierRow { get; set; }
    public required List<IEnumerable<string>> SavingThrowRows { get; init; }
    public required List<IEnumerable<string>> GetHitRows{ get; init; }


    public static ProbabilityTableData FromActor(Actor actor, int[] dcs, int[] attackModifiers)
    {
        ValidateSameNumberOfElements(dcs, attackModifiers);

        var dcRow = new List<string> { "Ability/AC" }.Concat(dcs.Select(dc => dc.ToString())).ToList();
        var savingThrowRows = Enum.GetValues<AbilityScoreType>().Select(abilityScoreType => CreateRow(actor, abilityScoreType, dcs));

        var attackModifierRow = new List<string> { "#Attacks/Modifier" }.Concat(attackModifiers.Select(modifier => modifier.ToString()));
        var getHitRows = new List<string> { "1" }.Concat(attackModifiers.Select(modifier => ((SuccessChanceViewModel)actor.GetHitChance(modifier)).ToString()));

        return new()
        {
            Header = actor.Name,
            DcRow = dcRow,
            SavingThrowRows = savingThrowRows.ToList(),
            AttackModifierRow = attackModifierRow.ToList(),
            GetHitRows = new() { getHitRows }
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