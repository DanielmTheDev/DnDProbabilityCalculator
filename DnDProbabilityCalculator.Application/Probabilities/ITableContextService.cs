namespace DnDProbabilityCalculator.Application.Probabilities;

public interface ITableContextService
{
    List<ProbabilityTable> Get(InputVariables inputVariables);
}