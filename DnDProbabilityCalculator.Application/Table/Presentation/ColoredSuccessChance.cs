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
                < 0.25 => $"[green]{successChanceAsString}[/]",
                < 0.45 => $"[turquoise2]{successChanceAsString}[/]",
                < 0.65 => $"[yellow]{successChanceAsString}[/]",
                < 0.85 => $"[orange4_1]{successChanceAsString}[/]",
                _ => $"[red]{successChanceAsString}[/]",
            }
            : _chance switch
            {
                < 0.25 => $"[red]{successChanceAsString}[/]",
                < 0.45 => $"[orange4_1]{successChanceAsString}[/]",
                < 0.65 => $"[yellow]{successChanceAsString}[/]",
                < 0.85 => $"[turquoise2]{successChanceAsString}[/]",
                _ => $"[green]{successChanceAsString}[/]",
            };
    }
}