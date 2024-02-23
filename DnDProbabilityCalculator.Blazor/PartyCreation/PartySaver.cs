using System.Net.Http.Json;
using DnDProbabilityCalculator.Shared.PartyCreation;

namespace DnDProbabilityCalculator.Blazor.PartyCreation;

public class PartySaver(IHttpClientFactory clientFactory) : IPartySaver
{
    public async Task<string> Save(CreatePartyDto party)
    {
        var client = clientFactory.CreateClient("B2CSandbox.ServerAPI");
        var result = await client.PostAsJsonAsync("api/SaveParty", party);
        var parsedResult = await result.Content.ReadFromJsonAsync<SavePartyResponse>();
        return parsedResult!.PartyId;
    }
}