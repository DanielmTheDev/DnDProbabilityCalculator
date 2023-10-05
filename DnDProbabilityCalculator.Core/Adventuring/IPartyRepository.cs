namespace DnDProbabilityCalculator.Core.Adventuring;

public interface IPartyRepository
{
    Party Get();
    void Save(Party party);
}