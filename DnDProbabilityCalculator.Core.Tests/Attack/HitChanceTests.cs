using DnDProbabilityCalculator.Core.Adventuring;
using DnDProbabilityCalculator.Core.Adventuring.Attack;

namespace DnDProbabilityCalculator.Core.Tests.Attack;

[TestClass]
public class HitChanceTests
{
    [TestMethod]
    [DataRow(0, 0.16)]
    [DataRow(1, 0.48)]
    [DataRow(2, 0.36)]
    public void Create_WithValidAttackModifiers_ReturnsProbability(int numberOfHits, double expectedChance)
    {
        // Act
        var probability = HitChance.Create(6, 15, 2, numberOfHits, AdvantageType.None);

        // Assert
        Assert.AreEqual(expectedChance, probability.Probability);
    }
}