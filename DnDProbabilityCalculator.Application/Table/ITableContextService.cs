namespace DnDProbabilityCalculator.Application.Table;

public interface ITableContextService
{
    List<TableContext> Get(InputVariables inputVariables);
}