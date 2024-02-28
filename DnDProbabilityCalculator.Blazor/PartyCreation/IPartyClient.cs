using DnDProbabilityCalculator.Shared.PartyCreation;
using FluentResults;

namespace DnDProbabilityCalculator.Blazor.PartyCreation;

public interface IPartyClient
{
    Task<Result<string>> Save(CreatePartyDto party);
}