namespace DnDProbabilityCalculator.Core.Adventuring.Abilities;

public abstract record AbilityScore
{
    private readonly int _value;

    public int Value
    {
        get => _value;
        init
        {
            if (value is < 1 or > 30)
            {
                throw new ArgumentOutOfRangeException(nameof(value), ErrorMessages.Ability_Score_Out_Of_Range);
            }

            _value = value;
        }
    }

    public bool IsProficient { get; init; }
    public bool IsAttackAbility { get; init; }
    public int Modifier => (int)Math.Floor((Value - 10) / 2.0);
    public abstract AbilityScoreType Type { get; }
    public static implicit operator int(AbilityScore attribute) => attribute.Value;

    public string Abbreviation => Type switch
    {
        AbilityScoreType.Dexterity => "Dex",
        AbilityScoreType.Strength => "Str",
        AbilityScoreType.Constitution => "Con",
        AbilityScoreType.Intelligence => "Int",
        AbilityScoreType.Charisma => "Cha",
        AbilityScoreType.Wisdom => "Wis",
        _ => throw new ArgumentOutOfRangeException()
    };
}