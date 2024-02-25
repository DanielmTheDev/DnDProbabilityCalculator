using System.Net.Http.Json;
using DnDProbabilityCalculator.Shared.PartyCreation;
using FluentResults;

namespace DnDProbabilityCalculator.Blazor.PartyCreation;

public class PartySaver(IHttpClientFactory clientFactory) : IPartySaver
{
    public async Task<Result<string>> Save(CreatePartyDto party)
    {
        var client = clientFactory.CreateClient("B2CSandbox.ServerAPI");
        var result = await client.PostAsJsonAsync("api/SaveParty", party);
        if (!result.IsSuccessStatusCode)
        {
            return Result.Fail("Creation of party failed");
        }
        var parsedResult = await result.Content.ReadFromJsonAsync<SavePartyResponse>();
        return parsedResult is null
            ? Result.Fail("Result could not be read")
            : Result.Ok(parsedResult.PartyId);
    }
}