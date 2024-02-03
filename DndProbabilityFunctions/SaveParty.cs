﻿using System.Collections.Generic;
using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace DndProbabilityFunctions;

public class SaveParty(ILoggerFactory loggerFactory)
{
    private readonly ILogger _logger = loggerFactory.CreateLogger<SaveParty>();

    [Function("SaveParty")]
    public static MultiResponse Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req, FunctionContext executionContext)
    {
        var logger = executionContext.GetLogger("HttpExample");
        logger.LogInformation("C# HTTP trigger function processed a request.");

        var message = "Welcome to Azure Functions!";

        var response = req.CreateResponse(HttpStatusCode.OK);
        response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
        response.WriteString(message);

        // Return a response to both HTTP trigger and Azure Cosmos DB output binding.
        return new MultiResponse()
        {
            Document = new MyDocument
            {
                id = System.Guid.NewGuid().ToString(),
                message = message
            },
            HttpResponse = response
        };
    }}

public class MultiResponse
{
    [CosmosDBOutput("test-sandbox", "sandbox1", Connection = "CosmosDbConnection", CreateIfNotExists = true)]
    public MyDocument Document { get; set; }
    public HttpResponseData HttpResponse { get; set; }
}
public class MyDocument {
    public string id { get; set; }
    public string message { get; set; }
}