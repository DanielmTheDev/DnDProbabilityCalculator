namespace DnDProbabilityCalculator.Core.Adventuring;

public class Actor
{
    public string Name { get; init; } = string.Empty;
    public AbilityScores AbilityScores { get; init; } = new();

    // todo: saving throw probability (probably parameterized)
    // todo: builder to create a character
    public static IStrengthStage New()
    {
        throw new NotImplementedException();
    }
}

public interface IStrengthStage
{
    IDexterityStage WithStrength(int value, bool isProficient = false);
}

public interface IDexterityStage
{
    IConstitutionStage WithDexterity(int value, bool isProficient = false);
}

public interface IConstitutionStage
{
    IWisdomStage WithConstitution(int value, bool isProficient = false);
}

public interface IWisdomStage
{
    IIntelligenceStage WithWisdom(int value, bool isProficient = false);
}

public interface IIntelligenceStage
{
    ICharismaStage WithIntelligence(int value, bool isProficient = false);
}

public interface ICharismaStage
{
    IBuildStage WithCharisma(int value, bool isProficient = false);
}

public interface IBuildStage
{
    Actor Build();
}