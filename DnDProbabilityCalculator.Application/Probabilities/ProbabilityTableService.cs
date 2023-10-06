using DnDProbabilityCalculator.Core.Adventuring;

namespace DnDProbabilityCalculator.Application.Probabilities;

public class ProbabilityTableService : IProbabilityTableService
{
    private readonly IPartyRepository _repository;

    public ProbabilityTableService(IPartyRepository repository)
        => _repository = repository;

    public Party Get()
        => _repository.Get();
}