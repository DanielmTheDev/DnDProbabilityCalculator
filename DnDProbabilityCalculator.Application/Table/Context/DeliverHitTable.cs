using DnDProbabilityCalculator.Application.Table.Presentation;
using DnDProbabilityCalculator.Core.Adventuring;

namespace DnDProbabilityCalculator.Application.Table.Context;

public class DeliverHitTable
{
    public required IEnumerable<string> ArmorClasses { get; set; }
    public required List<IEnumerable<string>> Probabilities { get; init; }

    private DeliverHitTable()
    {
    }

    public static DeliverHitTable FromActor(Actor actor, int[] armorClasses)
    {
        var armorClassesRow = new List<string> { $"{actor.NumberOfAttacks} Attacks/AC" }.Concat(armorClasses.Select(ac => ac.ToString()));

        var probabilityRows = Enumerable.Range(1, actor.NumberOfAttacks)
            .Select(currentNumberOfHits => CreateDeliverHitRow(actor, armorClasses, currentNumberOfHits))
            .ToList();

        return new()
        {
            ArmorClasses = armorClassesRow,
            Probabilities = probabilityRows
        };
    }

    private static IEnumerable<string> CreateDeliverHitRow(Actor actor, IEnumerable<int> armorClasses, int currentNumberOfHits)
        => new[] { $"{currentNumberOfHits} Hits" }.Concat(armorClasses
            .Select(currentArmorClass =>
            {
                var probability = actor.DeliverHitChance(currentArmorClass, currentNumberOfHits).Probability;
                var successChance = ColoredSuccessChance.FromProbability(probability);
                return successChance.ToString();
            }));
}