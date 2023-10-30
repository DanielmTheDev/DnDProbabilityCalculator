using DnDProbabilityCalculator.Core.Adventuring;

namespace DnDProbabilityCalculator.Application.Probabilities;

public class TableContextService : ITableContextService
{
    private readonly IPartyRepository _repository;

    public TableContextService(IPartyRepository repository)
        => _repository = repository;

    public List<ProbabilityTable> Get(int[] dcs, int[] attackModifiers, int numberOfAttacks)
    {
        var party = _repository.Get();
        return party.Characters.Select(actor => ProbabilityTable.FromActor(actor, dcs, attackModifiers, numberOfAttacks)).ToList();
    }
}