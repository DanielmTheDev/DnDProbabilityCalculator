namespace DnDProbabilityCalculator.Application.Probabilities;

public interface IProbabilityTableService
{
    List<ProbabilityTableData> Get(int[] dcs, int[] attackModifiers);
}