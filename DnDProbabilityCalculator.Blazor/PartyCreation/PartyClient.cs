using System.Net.Http.Json;
using DnDProbabilityCalculator.Core.Adventuring;
using DnDProbabilityCalculator.Shared.PartyCreation;
using FluentResults;
using Microsoft.AspNetCore.Components.Authorization;

namespace DnDProbabilityCalculator.Blazor.PartyCreation;

public class PartyClient(HttpClient client, AuthenticationStateProvider authStateProvider) : IPartyClient
{
    public async Task<Result<string>> Save(CreatePartyDto party)
    {
        var result = await client.PostAsJsonAsync("api/parties", party);
        return result.IsSuccessStatusCode
            ? await ParseSaveResult(result)
            : Result.Fail("Creation of party failed");
    }

    public async Task<Result<Party[]>> GetAll()
    {
        try
        {
            var authState = await authStateProvider.GetAuthenticationStateAsync();
            return authState.User.Identity is { IsAuthenticated: true }
                ? await GetParties(authState)
                : Result.Fail("User is not authenticated");
        }
        catch (Exception)
        {
            return Result.Fail("Error retrieving parties");
        }
    }

    private static async Task<Result<string>> ParseSaveResult(HttpResponseMessage result)
    {
        var parsedResult = await result.Content.ReadFromJsonAsync<SavePartyResponse>();
        return parsedResult is null
            ? Result.Fail("Result could not be read")
            : Result.Ok(parsedResult.PartyId);
    }

    private async Task<Result<Party[]>> GetParties(AuthenticationState authState)
    {
        var userId = authState.User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
        var result = await client.GetFromJsonAsync<Party[]>($"api/parties/{userId}");

        return result is not null
            ? Result.Ok(result)
            : Result.Fail("Result was null");
    }
}