using DnDProbabilityCalculator.Application.Table;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DnDProbabilityCalculator.Application.Tests.Table;

[TestClass]
public class ColoredSuccessChanceTests
{
    [TestMethod]
    [DataRow(0.24, "[green]24%[/]")]
    [DataRow(0.25, "[turquoise2]25%[/]")]
    [DataRow(0.44, "[turquoise2]44%[/]")]
    [DataRow(0.45, "[yellow]45%[/]")]
    [DataRow(0.64, "[yellow]64%[/]")]
    [DataRow(0.65, "[orange4_1]65%[/]")]
    [DataRow(0.84, "[orange4_1]84%[/]")]
    [DataRow(0.85, "[red]85%[/]")]
    public void ToString_WithInvertedEnabled_WrapsPercentInInvertedColor(double value, string expectedString)
    {
        Assert.AreEqual(expectedString, ColoredSuccessChance.FromProbability(value).WithInvertedColors().ToString());
    }

    [TestMethod]
    [DataRow(0.24, "[red]24%[/]")]
    [DataRow(0.25, "[orange4_1]25%[/]")]
    [DataRow(0.44, "[orange4_1]44%[/]")]
    [DataRow(0.45, "[yellow]45%[/]")]
    [DataRow(0.64, "[yellow]64%[/]")]
    [DataRow(0.65, "[turquoise2]65%[/]")]
    [DataRow(0.84, "[turquoise2]84%[/]")]
    [DataRow(0.85, "[green]85%[/]")]
    public void ToString_WithInvertedDisabled_WrapsPercentInInvertedColor(double value, string expectedString)
    {
        Assert.AreEqual(expectedString, ColoredSuccessChance.FromProbability(value).ToString());
    }
}