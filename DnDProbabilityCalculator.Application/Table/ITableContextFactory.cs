namespace DnDProbabilityCalculator.Application.Table;

public interface ITableContextFactory
{
    List<TableContext> Create(InputVariables inputVariables);
}