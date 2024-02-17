namespace DnDProbabilityCalculator.Blazor.Communication;

public interface IPartySaver
{
    Task<string> Save();
}