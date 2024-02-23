namespace DnDProbabilityCalculator.Console.Presentation;

public static class ColorStringExtensions
{
    public static string AsRed(this string value)
        => $"[red]{value}[/]";

    public static string AsGreen(this string value)
        => $"[green]{value}[/]";

    public static string AsTurquoise2(this string value)
        => $"[turquoise2]{value}[/]";

    public static string AsYellow(this string value)
        => $"[yellow]{value}[/]";

    public static string AsOrange4_1(this string value)
        => $"[orange4_1]{value}[/]";
}