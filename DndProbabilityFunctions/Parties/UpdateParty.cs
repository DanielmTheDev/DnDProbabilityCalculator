using System.Net;
using DnDProbabilityCalculator.Core.Adventuring;
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
        var (partyDto, userId) = await req.ParseRequest();


        if (partyDto is null || userId is null)
        {
            return req.CreateResponse(HttpStatusCode.BadRequest);
        }

        await container.UpsertItemAsync(partyDto.ToParty(userId));

        try
        {
            _ = await container.ReadItemAsync<Party>(id, new(userId));

            var updatedParty = partyDto.ToParty(userId) with { Id = id };
            var replaceResponse = await container.ReplaceItemAsync(updatedParty, id, new PartitionKey(userId));
            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(replaceResponse.Resource);
            return response;
        }
        catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
        {
            return req.CreateResponse(HttpStatusCode.NotFound);
        }
    }
}