using DnDProbabilityCalculator.Core.Adventuring;
using DnDProbabilityCalculator.Core.Adventuring.Abilities;

namespace DnDProbabilityCalculator.Application.Table.SavingThrow;

public class SavingThrowTable
{
    public required int[] Dcs { get; init; }
    public required List<SavingThrowTableRow> Probabilities { get; init; }

    private SavingThrowTable()
    {
    }

    public static SavingThrowTable FromActor(Actor actor, InputVariables inputVariables)
        => new()
        {
            Dcs = inputVariables.Dcs,
            Probabilities = Enum.GetValues<AbilityScoreType>()
                .Select(abilityScoreType => CreateRow(actor, abilityScoreType, inputVariables))
                .ToList()
        };

    private static SavingThrowTableRow CreateRow(Actor actor, AbilityScoreType abilityScoreType, InputVariables inputVariables)
        => new()
        {
            AbilityScoreType = abilityScoreType,
            IsProficient = actor.AbilityScores.IsProficientAt(abilityScoreType),
            Cells = inputVariables.Dcs
                .Select(dc => actor.SavingThrowSuccessChance(abilityScoreType, dc, inputVariables.Advantage)).ToList(),
            AbilityScore = actor.AbilityScores.Get(abilityScoreType).Value
        };
}