namespace DnDProbabilityCalculator.Application.Probabilities;

public interface IProbabilityTableService
{
    ICollection<ProbabilityTable> Get(params int[] dcs);
}