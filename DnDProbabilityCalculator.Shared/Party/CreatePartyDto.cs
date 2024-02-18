namespace DnDProbabilityCalculator.Shared.Party;

public class CreatePartyDto
{
    public string? Name { get; set; }

    public IList<CreateCharacterDto> Characters { get; set; } = [];
}