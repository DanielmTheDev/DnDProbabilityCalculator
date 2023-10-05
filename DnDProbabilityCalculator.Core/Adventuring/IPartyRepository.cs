namespace DnDProbabilityCalculator.Core.Adventuring;

public interface IPartyRepository
{
    Party Get(string filePath);
    void Save(Party party, string filePath);
}