﻿using System.Net;
using DnDProbabilityCalculator.Core.Adventuring;
using DnDProbabilityCalculator.Shared.PartyCreation;
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

        try
        {
            var replaceResponse = await ReplaceParty(id, container, userId, partyDto);
            return await CreateSuccessResponse(req, replaceResponse);
        }
        catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
        {
            return req.CreateResponse(HttpStatusCode.NotFound);
        }
    }

    private static async Task<HttpResponseData> CreateSuccessResponse(HttpRequestData req, ItemResponse<Party> replaceResponse)
    {
        var response = req.CreateResponse(HttpStatusCode.OK);
        await response.WriteAsJsonAsync(replaceResponse.Resource);
        return response;
    }

    private static async Task<ItemResponse<Party>> ReplaceParty(string id, Container container, string userId, CreatePartyDto partyDto)
    {
        _ = await container.ReadItemAsync<Party>(id, new(userId));
        var updatedParty = partyDto.ToParty(userId) with { Id = id };
        var replaceResponse = await container.ReplaceItemAsync(updatedParty, id, new PartitionKey(userId));
        return replaceResponse;
    }
}