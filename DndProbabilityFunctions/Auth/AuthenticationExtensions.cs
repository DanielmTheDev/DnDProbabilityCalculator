using FluentResults;
using Microsoft.Azure.Functions.Worker.Http;

namespace DndProbabilityFunctions.Auth;

public static class AuthenticationExtensions
{
    public static Result<string> GetUserId(this HttpRequestData req)
    {
        var userId = Environment.GetEnvironmentVariable("AZURE_FUNCTIONS_ENVIRONMENT") == "Development"
            ? "e5efb91c-4cff-4047-aa78-5f3b636b84e9"
            : req.Headers.SingleOrDefault(header => header.Key == "x-ms-client-principal-id").Value?.FirstOrDefault();
        return userId is null
            ? Result.Fail("User Id not found")
            : Result.Ok(userId);
    }
}