using DnDProbabilityCalculator.Core.Adventuring;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace DndProbabilityFunctions.Parties;

public class PartyMultiResponse
{
    [CosmosDBOutput("dnd-probability-calculator", "parties", Connection = "CosmosDbConnection", CreateIfNotExists = true)]
    public Party? Party { get; set; }
    public required HttpResponseData HttpResponse { get; set; }
}