using DnDProbabilityCalculator.Application.Table.Presentation;

namespace DnDProbabilityCalculator.Application.Tests.DnDProbabilityCalculator.Application.Table.Presentation;

[TestClass]
public class ColorStringExtensionsTests
{
    [TestMethod]
    public void AsRed_WhenCalled_WrapsInRed()
    {
        // Arrange Act Assert
        Assert.AreEqual("[red]value[/]", "value".AsRed());
    }

    [TestMethod]
    public void AsGreen_WhenCalled_WrapsInGreen()
    {
        // Arrange Act Assert
        Assert.AreEqual("[green]value[/]", "value".AsGreen());
    }

    [TestMethod]
    public void AsTurquoise2_WhenCalled_WrapsInTurquoise2()
    {
        // Arrange Act Assert
        Assert.AreEqual("[turquoise2]value[/]", "value".AsTurquoise2());
    }

    [TestMethod]
    public void AsYellow_WhenCalled_WrapsInYellow()
    {
        // Arrange Act Assert
        Assert.AreEqual("[yellow]value[/]", "value".AsYellow());
    }

    [TestMethod]
    public void AsOrange4_1_WhenCalled_WrapsInYellow()
    {
        // Arrange Act Assert
        Assert.AreEqual("[orange4_1]value[/]", "value".AsOrange4_1());
    }
}