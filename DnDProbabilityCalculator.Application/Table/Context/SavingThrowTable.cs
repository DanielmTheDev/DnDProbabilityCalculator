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

    public static SavingThrowTable FromActor(Actor actor, InputVariables inputVariables)
    {
        var dcRow = new List<string> { "Ability/DC" }.Concat(inputVariables.Dcs.Select(dc => dc.ToString())).ToList();
        var probabilities = Enum.GetValues<AbilityScoreType>().Select(abilityScoreType => CreateRow(actor, abilityScoreType, inputVariables)).ToList();

        return new()
        {
            Dcs = dcRow,
            Probabilities = probabilities
        };
    }

    private static IEnumerable<string> CreateRow(Actor actor, AbilityScoreType abilityScoreType, InputVariables inputVariables)
    {
        var abilityScore = actor.AbilityScores.Get(abilityScoreType);
        var firstCell = $"{ColoredAbbreviation(actor, abilityScore)} ({abilityScore.Value})";
        return new List<string> { firstCell }
            .Concat(inputVariables.Dcs.Select(
                dc =>
                {
                    var probability = actor.SavingThrowSuccessChance(abilityScoreType, dc, inputVariables.Advantage);
                    return ColoredSuccessChance.FromProbability(probability).ToString();
                }).ToList());
    }

    private static string ColoredAbbreviation(Actor actor, AbilityScore abilityScore)
        => actor.AbilityScores.IsProficientAt(abilityScore.Type)
            ? abilityScore.Abbreviation.AsGreen()
            : abilityScore.Abbreviation;
}