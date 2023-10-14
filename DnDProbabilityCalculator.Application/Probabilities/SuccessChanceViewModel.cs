namespace DnDProbabilityCalculator.Application.Probabilities;

public record SuccessChanceViewModel(double Chance)
{
    public static implicit operator SuccessChanceViewModel(double value)
        => new(value);

    public static implicit operator double(SuccessChanceViewModel value)
        => value.Chance;

    public override string ToString()
    {
        var successChanceAsString = Chance.ToString("P0");
        return Chance switch
        {
            <= 0.25 => $"[red]{successChanceAsString}[/]",
            <= 0.45 => $"[orange4_1]{successChanceAsString}[/]",
            <= 0.65 => $"[yellow]{successChanceAsString}[/]",
            <= 0.85 => $"[turquoise2]{successChanceAsString}[/]",
            _ => $"[green]{successChanceAsString}[/]",
        };
    }
}