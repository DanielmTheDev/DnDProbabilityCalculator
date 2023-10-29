using DnDProbabilityCalculator.Core.Adventuring;

namespace DnDProbabilityCalculator.Application.Probabilities;

public class ProbabilityTableService : IProbabilityTableService
{
    private readonly IPartyRepository _repository;

    public ProbabilityTableService(IPartyRepository repository)
        => _repository = repository;

    public List<ProbabilityTable> Get(int[] dcs, int[] attackModifiers, int numberOfAttacks)
    {
        var party = _repository.Get();
        return party.Characters.Select(actor => ProbabilityTable.FromActor(actor, dcs, attackModifiers, numberOfAttacks)).ToList();
    }
}