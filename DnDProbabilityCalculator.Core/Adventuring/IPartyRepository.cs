namespace DnDProbabilityCalculator.Core.Adventuring;

public interface IPartyRepository
{
    Party Get(string path);
    void Save(Party party, string filePath);
}