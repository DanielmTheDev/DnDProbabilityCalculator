namespace DnDProbabilityCalculator.Application.Probabilities;

public interface IProbabilityTableService
{
    IList<ProbabilityTableData> Get(params int[] dcs);
}