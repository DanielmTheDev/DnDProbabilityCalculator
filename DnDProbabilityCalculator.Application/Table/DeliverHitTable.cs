using DnDProbabilityCalculator.Core.Adventuring;

namespace DnDProbabilityCalculator.Application.Table;

public class DeliverHitTable
{
    public static DeliverHitTable FromActor(Actor actor, int[] armorClasses, int numberOfAttacks)
    {
        throw new NotImplementedException();
    }

    public required IEnumerable<string> ArmorClasses { get; set; }
    public required List<IEnumerable<string>> Probabilities { get; init; }
}