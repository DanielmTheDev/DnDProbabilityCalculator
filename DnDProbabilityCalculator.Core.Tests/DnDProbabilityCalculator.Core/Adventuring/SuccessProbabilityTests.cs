using DnDProbabilityCalculator.Core.Adventuring;

namespace DnDProbabilityCalculator.Core.Tests.DnDProbabilityCalculator.Core.Adventuring;

[TestClass]
public class SuccessProbabilityTests
{
    [TestMethod]
    [DataRow(5, 15, AdvantageType.None, 0.55)]
    [DataRow(3, 17, AdvantageType.Advantage, 0.5775)]
    [DataRow(2, 12, AdvantageType.Disadvantage, 0.3025)]
    [DataRow(10, 10, AdvantageType.None, 1.0)]
    [DataRow(0, 20, AdvantageType.None, 0.05)]
    [DataRow(7, 10, AdvantageType.Advantage, 1.0)]
    [DataRow(7, 10, AdvantageType.Disadvantage, 0.81)]
    public void CalculateProbability_WhenCalled_ReturnsProbabilities(int positiveModifier, int negativeModifier, AdvantageType advantage, double expectedProbability)
    {
        // Act
        var actualProbability = Probability.Calculate(positiveModifier, negativeModifier, advantage);

        // Assert
        Assert.AreEqual(expectedProbability, actualProbability, 0.1);
    }
}