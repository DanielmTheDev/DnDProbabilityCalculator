using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using DnDProbabilityCalculator.Core.Adventuring;
using DnDProbabilityCalculator.Shared.PartyCreation;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Serilog;

namespace DndProbabilityFunctions.Parties;

public class SaveParty
{
    private readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        Converters = { new JsonStringEnumConverter() }
    };

    [Function("SaveParty")]
    public async Task<PartyMultiResponse> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "parties")] HttpRequestData req)
    {
        try
        {
            var (partyDto, userId) = await ParseRequest(req);

            if (partyDto is null || userId is null)
            {
                return new()
                {
                    Party = null,
                    HttpResponse = req.CreateResponse(HttpStatusCode.BadRequest)
                };
            }

            var partyEntity = partyDto.ToParty(userId);

            Log.Debug("Attempting to save party with id {PartyId} for user {UserId}", partyEntity.Id, partyEntity.UserId);

            var response = await CreateResponse(req, partyEntity);

            return new()
            {
                Party = partyEntity,
                HttpResponse = response
            };
        }
        catch (Exception e)
        {
            Log.Error(e, "Exception occured in function");
            throw;
        }
    }

    private async Task<(CreatePartyDto? partyDto, string? userId)> ParseRequest(HttpRequestData req)
    {
        var partyDto = await JsonSerializer.DeserializeAsync<CreatePartyDto>(req.Body, _jsonSerializerOptions);
        // var userId = req.Headers.SingleOrDefault(header => header.Key == "x-ms-client-principal-id").Value?.First();
        const string userId = "e5efb91c-4cff-4047-aa78-5f3b636b84e9"; // for local
        return (partyDto, userId);
    }

    private static async Task<HttpResponseData> CreateResponse(HttpRequestData req, Party partyEntity)
    {
        var dto = new SavePartyResponse(partyEntity.Id);
        var response = req.CreateResponse(HttpStatusCode.OK);
        await response.WriteAsJsonAsync(dto);
        return response;
    }
}