namespace DnDProbabilityCalculator.Application.Probabilities;

public record ColoredSuccessChance(double Chance)
{
    private bool UseInvertedColorings { get; init; }

    public static implicit operator ColoredSuccessChance(double value)
        => new(value);

    public static implicit operator double(ColoredSuccessChance value)
        => value.Chance;

    public ColoredSuccessChance WithInvertedColors()
        => this with { UseInvertedColorings = true };

    public override string ToString()
    {
        var successChanceAsString = Chance.ToString("P0");
        return UseInvertedColorings
            ? Chance switch
            {
                < 0.25 => $"[green]{successChanceAsString}[/]",
                < 0.45 => $"[turquoise2]{successChanceAsString}[/]",
                < 0.65 => $"[yellow]{successChanceAsString}[/]",
                < 0.85 => $"[orange4_1]{successChanceAsString}[/]",
                _ => $"[red]{successChanceAsString}[/]",
            }
            : Chance switch
            {
                < 0.25 => $"[red]{successChanceAsString}[/]",
                < 0.45 => $"[orange4_1]{successChanceAsString}[/]",
                < 0.65 => $"[yellow]{successChanceAsString}[/]",
                < 0.85 => $"[turquoise2]{successChanceAsString}[/]",
                _ => $"[green]{successChanceAsString}[/]",
            };
    }
}