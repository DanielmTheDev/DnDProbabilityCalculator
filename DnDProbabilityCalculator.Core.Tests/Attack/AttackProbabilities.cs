using DnDProbabilityCalculator.Core.Adventuring.Attack;

namespace DnDProbabilityCalculator.Core.Tests.Attack;

[TestClass]
public class AttackProbabilitiesTests
{
    [TestMethod]
    public void CalculateGetHit_WithPositiveAttackModifiers_ReturnsMultipleProbabilities()
    {
        // Act
        var probabilities = AttackProbabilities.Create(6, 2, 15);

        // Assert
        var expected = new List<AttackProbability>
        {
            new(0, 0.16),
            new(1, 0.48),
            new(2, 0.36)
        };
        CollectionAssert.AreEquivalent(expected, probabilities.Probabilities);
        Assert.AreEqual(6, probabilities.AttackModifier);
        Assert.AreEqual(15, probabilities.ArmorClass);
    }
}