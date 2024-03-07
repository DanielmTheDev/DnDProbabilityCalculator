using System.Net;
using DnDProbabilityCalculator.Core.Adventuring;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace DndProbabilityFunctions.Parties;

public class GetParties(ILogger<GetParties> logger)
{
    [Function("GetParties")]
    public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "parties/{userId}")] HttpRequestData req,
        [CosmosDBInput("dnd-probability-calculator", "parties", Connection = "CosmosDbConnection", SqlQuery = "SELECT * FROM c WHERE c.userId = {userId}")] Party[] parties, string userId)
    {
        var enumeratedParties = parties.ToList();
        logger.LogDebug("Returning {NumberOfParties} parties for user {UserId}", enumeratedParties.Count, userId);
        var response = req.CreateResponse(HttpStatusCode.OK);
        await response.WriteAsJsonAsync(enumeratedParties);
        return response;
    }
}