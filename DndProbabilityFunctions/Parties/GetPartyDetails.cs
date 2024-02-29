using System.Net;
using DnDProbabilityCalculator.Core.Adventuring;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace DndProbabilityFunctions.Parties;

public class GetPartyDetails
{
    [Function("GetPartyDetails")]
    public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "party-details/{partyId}")] HttpRequestData req, FunctionContext executionContext, string partyId,
        [CosmosDBInput("dnd-probability-calculator", "parties", Connection = "CosmosDbConnection", SqlQuery = "SELECT * FROM c WHERE c.id = {partyId}")] Party[] parties)
    {
        return parties.Length switch
        {
            0 => req.CreateResponse(HttpStatusCode.NotFound),
            > 1 => await CreateProblemDetails(req, partyId),
            _ => await CreatePartyResponse(req, parties)
        };
    }

    private static async Task<HttpResponseData> CreatePartyResponse(HttpRequestData req, Party[] parties)
    {
        var response = req.CreateResponse(HttpStatusCode.OK);
        await response.WriteAsJsonAsync(parties.First());
        return response;
    }

    private static async Task<HttpResponseData> CreateProblemDetails(HttpRequestData req, string partyId)
    {
        var response = req.CreateResponse(HttpStatusCode.BadRequest);
        await response.WriteAsJsonAsync(new ProblemDetails
        {
            Title = "Multiple parties with same Id found",
            Status = 400,
            Detail = "This hints at a bug, since the ids should be globally unique",
            Extensions = { { "partyId", partyId } }
        });
        return response;
    }
}