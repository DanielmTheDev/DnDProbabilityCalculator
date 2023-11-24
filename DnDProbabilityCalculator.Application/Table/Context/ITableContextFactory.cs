namespace DnDProbabilityCalculator.Application.Table.Context;

public interface ITableContextFactory
{
    List<TableContext> Create(InputVariables inputVariables);
}