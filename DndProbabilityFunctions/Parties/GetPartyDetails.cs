using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace DndProbabilityFunctions.Parties;

public class GetPartyDetails
{
    [Function("GetPartyDetails")]
    public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "party-details/{partyId}")] HttpRequestData req, FunctionContext executionContext, string partyId)
    {
        // Inject CosmosClient
        // Get UserId from header
        // Retrieve party using those (thereby checking if the correct user is accessing it)
        // return if found

        var response = req.CreateResponse(HttpStatusCode.OK);
        response.WriteString("Welcome to Azure Functions!");

        return response;
    }
}