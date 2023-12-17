namespace DnDProbabilityCalculator.Core.Adventuring.Abilities;

public enum AbilityScoreType
{
    Dexterity,
    Strength,
    Constitution,
    Intelligence,
    Charisma,
    Wisdom
}

public static class AbilityScoreTypeExtensions
{
    public static string Abbreviated(this AbilityScoreType abilityScoreType)
        => abilityScoreType switch
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