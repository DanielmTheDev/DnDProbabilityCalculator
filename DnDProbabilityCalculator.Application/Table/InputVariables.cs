namespace DnDProbabilityCalculator.Application.Table;

public record InputVariables
{
    public int[] Dcs { get; }
    public int[] AttackModifiers { get; }
    public int[] ArmorClasses { get; }
    public int NumberOfAttacks { get; }

    public InputVariables(int[] dcs, int[] attackModifiers, int[] armorClasses, int numberOfAttacks)
    {
        Dcs = dcs;
        AttackModifiers = attackModifiers;
        NumberOfAttacks = numberOfAttacks;
        ArmorClasses = armorClasses;
        ValidateSameNumberOfElements();
    }

    public InputVariables WithIncrementedNumberOfAttacks()
        => new(Dcs, AttackModifiers, ArmorClasses, NumberOfAttacks + 1);

    public InputVariables WithDecrementedNumberOfAttacks()
        => new(Dcs, AttackModifiers, ArmorClasses, NumberOfAttacks - 1);

    public InputVariables WithIncrementedColumns()
        => new(IncrementedColumn(Dcs), IncrementedColumn(AttackModifiers), IncrementedColumn(ArmorClasses), NumberOfAttacks);

    public InputVariables WithDecrementedColumns()
        => new(DecrementedColumn(Dcs), DecrementedColumn(AttackModifiers), DecrementedColumn(ArmorClasses), NumberOfAttacks);

    private int[] DecrementedColumn(int[] values)
        => new[] { values.First() - 1 }.Concat(values.Take(values.Length - 1)).ToArray();

    private int[] IncrementedColumn(int[] values)
        => values.Skip(1).Concat(new[] { values.Last() + 1 }).ToArray();

    private void ValidateSameNumberOfElements()
    {
        if (Dcs.Length != AttackModifiers.Length || Dcs.Length != ArmorClasses.Length)
        {
            throw new ArgumentException("The number of dcs and attack modifiers must be the same.");
        }
    }
}