using DnDProbabilityCalculator.Core;

namespace DnDProbabilityCalculator.Application.Table;

public class TableContextFactory(IPartyRepository repository) : ITableContextFactory
{
    public List<TableContext> Create(InputVariables inputVariables)
    {
        var party = repository.Get();
        return party.Characters
            .Select(actor => TableContext.FromActor(actor, inputVariables))
            .ToList();
    }
}