using System.Net;
using DnDProbabilityCalculator.Core.Adventuring;
using DndProbabilityFunctions.Auth;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace DndProbabilityFunctions.Parties;

public class UpdateParty(ILogger<DeleteParty> logger, CosmosClient cosmosClient)
{
    [Function("UpdateParty")]
    public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "parties/{id}")] HttpRequestData req, FunctionContext executionContext, string id)
    {
        logger.LogDebug("Attempting to update party with id {PartyId}", id);
        var container = cosmosClient.GetContainer("dnd-probability-calculator", "parties");
        return req.CreateResponse(HttpStatusCode.OK);
        // try
        // {
        //     await container.DeleteItemAsync<Party>(id, new(req.GetUserId().Value));
        //     return req.CreateResponse(HttpStatusCode.OK);
        // }
        // catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
        // {
        //     return req.CreateResponse(HttpStatusCode.NotFound);
        // }
    }
}