using DnDProbabilityCalculator.Application.Probabilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DnDProbabilityCalculator.Application.Tests.Probabilities;

[TestClass]
public class SuccessChanceViewModelTests
{
    [TestMethod]
    public void Implicitly_Converts_ValueToDouble()
    {
        // Arrange
        var chance = new SuccessChanceViewModel(5.0);

        // Act
        double chanceAsDouble = chance;

        // Assert
        Assert.AreEqual(5.0, chanceAsDouble);
    }

    [TestMethod]
    public void Implicitly_Converts_DoubleViewModel()
    {
        // Arrange
        const double chanceAsDouble = 0.5d;

        // Act
        SuccessChanceViewModel chance = chanceAsDouble;

        // Assert
        Assert.AreEqual(chanceAsDouble, chance.Chance);
    }

    [TestMethod]
    [DataRow(0.0d, "red")]
    [DataRow(0.25d, "red")]
    [DataRow(0.26d, "orange4_1")]
    [DataRow(0.45d, "orange4_1")]
    [DataRow(0.46d, "yellow")]
    [DataRow(0.65d, "yellow")]
    [DataRow(0.66d, "turquoise2")]
    [DataRow(0.85d, "turquoise2")]
    [DataRow(0.86d, "green")]
    [DataRow(double.MaxValue, "green")]
    public void ToString_WhenCalled_WrapsResultInColor(double chanceAsDouble, string expectedColor)
    {
        // Arrange
        var chance = new SuccessChanceViewModel(chanceAsDouble);

        // Act
        var chanceAsString = chance.ToString();

        // Assert
        Assert.IsTrue(chanceAsString.Contains(expectedColor));
    }

}