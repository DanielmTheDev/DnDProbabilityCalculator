namespace DnDProbabilityCalculator.Application.Probabilities;

public record InputVariables
{
    public int[] Dcs { get; }
    public int[] AttackModifiers { get; }
    public int NumberOfAttacks { get; }

    public InputVariables(int[] dcs, int[] attackModifiers, int numberOfAttacks)
    {
        Dcs = dcs;
        AttackModifiers = attackModifiers;
        NumberOfAttacks = numberOfAttacks;
        ValidateSameNumberOfElements();
    }

    public InputVariables WithIncrementedNumberOfAttacks()
        => new(Dcs, AttackModifiers, NumberOfAttacks + 1);

    public InputVariables WithDecrementedNumberOfAttacks()
        => new(Dcs, AttackModifiers, NumberOfAttacks - 1);

    public InputVariables WithIncrementedDcsAndModifiers()
    {
        var increasedDcs = Dcs.Skip(1).Concat(new[] { Dcs.Last() + 1 });
        var increasedAttackModifiers = AttackModifiers.Skip(1).Concat(new[] { AttackModifiers.Last() + 1 });
        return new(increasedDcs.ToArray(), increasedAttackModifiers.ToArray(), NumberOfAttacks);
    }

    public InputVariables WithDecrementedDcsAndModifiers()
    {
        var decreasedDcs = new[] { Dcs.First() - 1 }.Concat(Dcs.Take(Dcs.Length - 1));
        var decreasedAttackModifiers = new[] { AttackModifiers.First() - 1 }.Concat(AttackModifiers.Take(AttackModifiers.Length - 1));
        return new(decreasedDcs.ToArray(), decreasedAttackModifiers.ToArray(), NumberOfAttacks);
    }

    private void ValidateSameNumberOfElements()
    {
        if (Dcs.Length != AttackModifiers.Length)
        {
            throw new ArgumentException("The number of dcs and attack modifiers must be the same.");
        }
    }
}