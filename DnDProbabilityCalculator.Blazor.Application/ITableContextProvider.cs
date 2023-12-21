using DnDProbabilityCalculator.Application.Table;

namespace DnDProbabilityCalculator.Blazor.Application;

public interface ITableContextProvider
{
    IEnumerable<TableContext> Get();
}