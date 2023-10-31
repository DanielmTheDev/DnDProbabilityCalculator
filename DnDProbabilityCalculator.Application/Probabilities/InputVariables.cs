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

    private void ValidateSameNumberOfElements()
    {
        if (Dcs.Length != AttackModifiers.Length)
        {
            throw new ArgumentException("The number of dcs and attack modifiers must be the same.");
        }
    }

}