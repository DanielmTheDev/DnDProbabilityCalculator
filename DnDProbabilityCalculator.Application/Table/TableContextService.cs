using DnDProbabilityCalculator.Core.Adventuring;

namespace DnDProbabilityCalculator.Application.Table;

public class TableContextService : ITableContextService
{
    private readonly IPartyRepository _repository;

    public TableContextService(IPartyRepository repository)
        => _repository = repository;

    public List<TableContext> Get(InputVariables inputVariables)
    {
        var party = _repository.Get();
        return party.Characters.Select(actor => TableContext.FromActor(actor, inputVariables)).ToList();
    }
}