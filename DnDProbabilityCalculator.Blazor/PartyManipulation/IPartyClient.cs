using DnDProbabilityCalculator.Core.Adventuring;
using DnDProbabilityCalculator.Shared.PartyCreation;
using FluentResults;

namespace DnDProbabilityCalculator.Blazor.PartyManipulation;

public interface IPartyClient
{
    Task<Result<string>> Save(CreatePartyDto party);
    Task<Result<Party[]>> GetAll();
    Task<Result<Party>> Get(string partyId);
    Task<Result> Delete(string partyId);
}