using System.Net;
using DnDProbabilityCalculator.Core.Adventuring;
using DndProbabilityFunctions.Auth;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace DndProbabilityFunctions.Parties;

public class DeleteParty(ILogger<DeleteParty> logger, CosmosClient cosmosClient)
{
    [Function("DeleteParty")]
    public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "parties/{id}")] HttpRequestData req, FunctionContext executionContext, string id)
    {
        logger.LogDebug("Attempting to delete party with id {PartyId}", id);
        var container = cosmosClient.GetContainer("dnd-probability-calculator", "parties");
        try
        {
            await container.DeleteItemAsync<Party>(id, new(req.GetUserId().Value));
            return req.CreateResponse(HttpStatusCode.OK);
        }
        catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
        {
            return req.CreateResponse(HttpStatusCode.NotFound);
        }
    }
}