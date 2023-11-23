using DnDProbabilityCalculator.Core.Adventuring;

namespace DnDProbabilityCalculator.Application.Table;

public class TableContextFactory : ITableContextFactory
{
    private readonly IPartyRepository _repository;

    public TableContextFactory(IPartyRepository repository)
        => _repository = repository;

    public List<TableContext> Create(InputVariables inputVariables)
    {
        var party = _repository.Get();
        return party.Characters
            .Select(actor => TableContext.FromActor(actor, inputVariables))
            .ToList();
    }
}