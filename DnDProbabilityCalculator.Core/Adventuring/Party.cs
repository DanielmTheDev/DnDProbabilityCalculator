namespace DnDProbabilityCalculator.Core.Adventuring;

public class Party
{
    public required string Id { get; init; }
    public required string Name { get; init; }
    public required string UserId { get; set; }
    public IList<Actor> Characters { get; init; } = new List<Actor>();
}