﻿using System.Net;
using DnDProbabilityCalculator.Core.Adventuring;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Serilog;

namespace DndProbabilityFunctions.Parties;

public class GetParties
{
    [Function("GetParties")]
    public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "parties/{userId}")] HttpRequestData req,
        [CosmosDBInput("dnd-probability-calculator", "parties", Connection = "CosmosDbConnection", SqlQuery = "SELECT * FROM c WHERE c.userId = {userId}")] Party[] parties, string userId)
    {
        var enumeratedParties = parties.ToList();
        Log.Debug("Returning {NumberOfParties} parties for user {UserId}", enumeratedParties.Count, userId);
        var response = req.CreateResponse(HttpStatusCode.OK);
        await response.WriteAsJsonAsync(enumeratedParties);
        return response;
    }
}