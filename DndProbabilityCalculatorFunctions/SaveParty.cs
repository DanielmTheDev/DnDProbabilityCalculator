using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace DndProbabilityCalculatorFunctions;

public static class SaveParty
{
    [FunctionName("SaveParty")]
    public static Task<IActionResult> RunAsync([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req, ILogger log)
    {
        try
        {
            return Task.FromResult<IActionResult>(new OkObjectResult("Test"));
        }
        catch (Exception e)
        {
            return Task.FromResult<IActionResult>(new OkObjectResult(e.Message));
        }
    }
}