namespace DnDProbabilityCalculator.Application.Probabilities;

public interface ITableContextService
{
    List<TableContext> Get(InputVariables inputVariables);
}