using DnDProbabilityCalculator.Core.Adventuring;

namespace DnDProbabilityCalculator.Application.Table;

public record InputVariables
{
    public int[] Dcs { get; }
    public int[] AttackModifiers { get; }
    public int[] ArmorClasses { get; }
    public int NumberOfAttacks { get; private init; }
    public AdvantageType Advantage { get; private init; }

    public InputVariables(int[] dcs, int[] attackModifiers, int[] armorClasses, int numberOfAttacks, AdvantageType advantage)
    {
        Dcs = dcs;
        AttackModifiers = attackModifiers;
        NumberOfAttacks = numberOfAttacks;
        Advantage = advantage;
        ArmorClasses = armorClasses;
        ValidateSameNumberOfElements();
    }

    public static InputVariables CreateDefaultInputVariables()
        => new(Enumerable.Range(12, 5).ToArray(), Enumerable.Range(3, 5).ToArray(), Enumerable.Range(10, 5).ToArray(), 2, AdvantageType.None);

    public InputVariables WithIncrementedNumberOfAttacks()
        => this with { NumberOfAttacks = NumberOfAttacks + 1 };

    public InputVariables WithDecrementedNumberOfAttacks()
        => NumberOfAttacks == 1
            ? this
            : this with { NumberOfAttacks = NumberOfAttacks - 1 };

    public InputVariables WithAdvantage()
        => this with { Advantage = AdvantageType.Advantage };

    public InputVariables WithDisadvantage()
        => this with { Advantage = AdvantageType.Disadvantage };

    public InputVariables WithNoAdvantage()
        => this with { Advantage = AdvantageType.None };

    public InputVariables WithIncrementedColumns()
        => new(IncrementedColumn(Dcs), IncrementedColumn(AttackModifiers), IncrementedColumn(ArmorClasses), NumberOfAttacks, Advantage);

    public InputVariables WithDecrementedColumns()
        => new(DecrementedColumn(Dcs), DecrementedColumn(AttackModifiers), DecrementedColumn(ArmorClasses), NumberOfAttacks, Advantage);

    private static int[] DecrementedColumn(int[] values)
        => new[] { values.First() - 1 }.Concat(values.Take(values.Length - 1)).ToArray();

    private static int[] IncrementedColumn(int[] values)
        => values.Skip(1).Concat(new[] { values.Last() + 1 }).ToArray();

    private void ValidateSameNumberOfElements()
    {
        if (Dcs.Length != AttackModifiers.Length || Dcs.Length != ArmorClasses.Length)
        {
            throw new ArgumentException("The number of dcs and attack modifiers must be the same.");
        }
    }
}