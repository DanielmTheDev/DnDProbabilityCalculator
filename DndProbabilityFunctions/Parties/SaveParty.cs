﻿using System.Net;
using DnDProbabilityCalculator.Core.Adventuring;
using DnDProbabilityCalculator.Shared.PartyCreation;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace DndProbabilityFunctions.Parties;

public class SaveParty(ILogger<SaveParty> logger)
{
    [Function("SaveParty")]
    public async Task<PartyMultiResponse> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "parties")] HttpRequestData req)
    {
        try
        {
            var (partyDto, userId) = await req.ParseRequest();

            return partyDto is null || userId is null
                ? CreateErrorResponse(req)
                : await CreateSuccessResponse(req, partyDto, userId);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Exception occured in function");
            throw;
        }
    }

    private static PartyMultiResponse CreateErrorResponse(HttpRequestData req)
        => new()
        {
            Party = null,
            HttpResponse = req.CreateResponse(HttpStatusCode.BadRequest)
        };

    private async Task<PartyMultiResponse> CreateSuccessResponse(HttpRequestData req, CreatePartyDto partyDto, string userId)
    {
        var partyEntity = partyDto.ToParty(userId);

        logger.LogDebug("Attempting to save party with id {PartyId} for user {UserId}", partyEntity.Id, partyEntity.UserId);

        var response = await CreateResponse(req, partyEntity);

        return new()
        {
            Party = partyEntity,
            HttpResponse = response
        };
    }

    private static async Task<HttpResponseData> CreateResponse(HttpRequestData req, Party partyEntity)
    {
        var dto = new SavePartyResponse(partyEntity.Id);
        var response = req.CreateResponse(HttpStatusCode.OK);
        await response.WriteAsJsonAsync(dto);
        return response;
    }
}