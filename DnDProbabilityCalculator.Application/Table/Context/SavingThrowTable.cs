using DnDProbabilityCalculator.Application.Table.Presentation;
using DnDProbabilityCalculator.Core.Adventuring;
using DnDProbabilityCalculator.Core.Adventuring.Abilities;

namespace DnDProbabilityCalculator.Application.Table.Context;

public class SavingThrowTable
{
    public required IEnumerable<string> Dcs { get; init; }
    public required List<IEnumerable<string>> Probabilities { get; init; }

    private SavingThrowTable()
    {
    }

    public static SavingThrowTable FromActor(Actor actor, int[] dcs)
    {
        var dcRow = new List<string> { "Ability/DC" }.Concat(dcs.Select(dc => dc.ToString())).ToList();
        var probabilities = Enum.GetValues<AbilityScoreType>().Select(abilityScoreType => CreateRow(actor, abilityScoreType, dcs)).ToList();

        return new()
        {
            Dcs = dcRow,
            Probabilities = probabilities
        };
    }

    private static IEnumerable<string> CreateRow(Actor actor, AbilityScoreType abilityScoreType, IEnumerable<int> dcs)
    {
        var abilityScore = actor.AbilityScores.Get(abilityScoreType);
        var firstCell = $"{abilityScore.Abbreviation} ({abilityScore.Value})";
        return new List<string> { firstCell }
            .Concat(dcs.Select(
                dc =>
                {
                    var probability = actor.SavingThrowSuccessChance(abilityScoreType, dc);
                    return ColoredSuccessChance.FromProbability(probability).ToString();
                }).ToList());
    }
}