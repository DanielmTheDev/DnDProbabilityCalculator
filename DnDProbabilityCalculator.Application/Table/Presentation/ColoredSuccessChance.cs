namespace DnDProbabilityCalculator.Application.Table.Presentation;

public record ColoredSuccessChance
{
    private readonly double _chance;

    private ColoredSuccessChance(double chance)
    {
        _chance = chance;
    }

    public static ColoredSuccessChance FromProbability(double chance) => new(chance);
    private bool UseInvertedColorings { get; init; }

    public ColoredSuccessChance WithInvertedColors()
        => this with { UseInvertedColorings = true };

    public override string ToString()
    {
        var successChanceAsString = _chance.ToString("P0");
        return UseInvertedColorings
            ? _chance switch
            {
                < 0.25 => successChanceAsString.AsGreen(),
                < 0.45 => successChanceAsString.AsTurquoise2(),
                < 0.65 => successChanceAsString.AsYellow(),
                < 0.85 => successChanceAsString.AsOrange4_1(),
                _ => successChanceAsString.AsRed()
            }
            : _chance switch
            {
                < 0.25 => successChanceAsString.AsRed(),
                < 0.45 => successChanceAsString.AsOrange4_1(),
                < 0.65 => successChanceAsString.AsYellow(),
                < 0.85 => successChanceAsString.AsTurquoise2(),
                _ => successChanceAsString.AsGreen(),
            };
    }
}