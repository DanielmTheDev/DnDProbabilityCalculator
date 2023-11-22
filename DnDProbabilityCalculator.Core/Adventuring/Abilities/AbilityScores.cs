namespace DnDProbabilityCalculator.Core.Adventuring.Abilities;

public record AbilityScores
{
    public required Dexterity Dexterity { get; init; }
    public required Strength Strength { get; init; }
    public required Constitution Constitution { get; init; }
    public required Intelligence Intelligence { get; init; }
    public required Wisdom Wisdom { get; init; }
    public required Charisma Charisma { get; init; }

    public AbilityScore Get(AbilityScoreType abilityScoreType)
        => AllAbilityScores().Single(abilityScore => abilityScore.Type == abilityScoreType);

    private IEnumerable<AbilityScore> AllAbilityScores()
        => new List<AbilityScore> { Dexterity, Strength, Constitution, Intelligence, Wisdom, Charisma };

    public IEnumerable<AbilityScore> AsList()
        => new List<AbilityScore> { Dexterity, Strength, Constitution, Intelligence, Wisdom, Charisma };
}