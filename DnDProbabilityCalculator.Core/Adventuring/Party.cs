namespace DnDProbabilityCalculator.Core.Adventuring;

public class Party
{
    public IList<Actor> Characters { get; set; } = new List<Actor>();
}