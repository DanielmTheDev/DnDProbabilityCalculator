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
    [DataRow(0.24d, "red")]
    [DataRow(0.25d, "orange4_1")]
    [DataRow(0.44d, "orange4_1")]
    [DataRow(0.45d, "yellow")]
    [DataRow(0.64d, "yellow")]
    [DataRow(0.65d, "turquoise2")]
    [DataRow(0.84d, "turquoise2")]
    [DataRow(0.85d, "green")]
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

    [TestMethod]
    [DataRow(0.0d, "0%")]
    [DataRow(-0.5d, "-50%")]
    [DataRow(0.512345d, "51%")]
    [DataRow(1.2d, "120%")]
    public void ToString_WhenCalled_ConvertsValueToPercentRepresentationWithoutDecimals(double chanceAsDouble, string expectedString)
    {
        // Arrange
        var chance = new SuccessChanceViewModel(chanceAsDouble);

        // Act
        var chanceAsString = chance.ToString();

        // Assert
        Assert.IsTrue(chanceAsString.Contains(expectedString));
    }
}