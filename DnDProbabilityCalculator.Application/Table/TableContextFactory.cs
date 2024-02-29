using DnDProbabilityCalculator.Core.Adventuring;

namespace DnDProbabilityCalculator.Application.Table;

public class TableContextFactory : ITableContextFactory
{
    public List<TableContext> Create(InputVariables inputVariables, Party party)
        => party.Characters
            .Select(actor => TableContext.FromActor(actor, inputVariables))
            .ToList();
}