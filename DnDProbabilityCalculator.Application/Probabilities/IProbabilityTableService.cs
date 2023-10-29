namespace DnDProbabilityCalculator.Application.Probabilities;

public interface IProbabilityTableService
{
    List<ProbabilityTable> Get(int[] dcs, int[] attackModifiers, int numberOfAttacks);
}