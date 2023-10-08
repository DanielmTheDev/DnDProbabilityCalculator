namespace DnDProbabilityCalculator.Application.Probabilities;

public interface IProbabilityTableService
{
    List<ProbabilityTableData> Get(params int[] dcs);
}