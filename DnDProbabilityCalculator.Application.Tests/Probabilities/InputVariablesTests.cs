using DnDProbabilityCalculator.Application.Probabilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DnDProbabilityCalculator.Application.Tests.Probabilities;

[TestClass]
public class InputVariablesTests
{
    [TestInitialize]
    public void Initialize()
    {
    }

    [TestMethod]
    public void Constructor_WithDifferentNumberOfDcsAndAttackModifiers_Throws()
    {
        // Arrange Act and Assert
        Assert.ThrowsException<ArgumentException>(() => new InputVariables(new[] { 1 }, new[] { 1, 2 }, 3));
    }
}