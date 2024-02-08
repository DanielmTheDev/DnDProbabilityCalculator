﻿using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using DnDProbabilityCalculator.Core.Adventuring;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace DndProbabilityFunctions;

public class SaveParty(ILoggerFactory loggerFactory)
{
    private readonly ILogger _logger = loggerFactory.CreateLogger<SaveParty>();

    private readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        Converters = { new JsonStringEnumConverter() }
    };

    [Function("SaveParty")]
    public async Task<PartyMultiResponse> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request");
        var party = await JsonSerializer.DeserializeAsync<Party>(req.Body, _jsonSerializerOptions);
        if (party is null)
        {
            return new()
            {
                Party = null,
                HttpResponse = req.CreateResponse(HttpStatusCode.BadRequest)
            };
        }

        const string message = "Character Saved";

        var response = req.CreateResponse(HttpStatusCode.OK);
        response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
        await response.WriteStringAsync(message);

        return new()
        {
            Party = party,
            HttpResponse = response
        };
    }
}