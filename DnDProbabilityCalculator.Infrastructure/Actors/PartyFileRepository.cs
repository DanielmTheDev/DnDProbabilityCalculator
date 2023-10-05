using DnDProbabilityCalculator.Core.Adventuring;

namespace DnDProbabilityCalculator.Infrastructure.Actors;

public class PartyFileRepository : IPartyRepository
{
    public Party Get(string filePath)
        => throw new NotImplementedException();

    public void Save(Party party, string filePath)
    {
        throw new NotImplementedException();
    }
}