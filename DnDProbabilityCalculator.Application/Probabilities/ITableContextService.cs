namespace DnDProbabilityCalculator.Application.Probabilities;

public interface ITableContextService
{
    List<ProbabilityTable> Get(int[] dcs, int[] attackModifiers, int numberOfAttacks);
}