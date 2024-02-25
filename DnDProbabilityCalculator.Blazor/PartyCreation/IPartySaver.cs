using DnDProbabilityCalculator.Shared.PartyCreation;
using FluentResults;

namespace DnDProbabilityCalculator.Blazor.PartyCreation;

public interface IPartySaver
{
    Task<Result<string>> Save(CreatePartyDto party);
}