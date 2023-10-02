// using DnDProbabilityCalculator.Core.Adventuring;
//
// namespace DnDProbabilityCalculator.Core.Tests.Adventuring;
//
// [TestClass]
// public class AttributesTests
// {
//     [TestMethod]
//     [DataRow(-9, -1)]
//     [DataRow(-8, -2)]
//     [DataRow(10, 0)]
//     [DataRow(11, 0)]
//     [DataRow(14, 2)]
//     public void DexterityModifer_IsCalculatedFromAbilityScore(int abilityScore, int expectedModifier)
//     {
//         // Arrange
//         var attributes = new Attributes { Dexterity = abilityScore };
//
//         // Act
//         var realModifier = attributes.DexterityModifier;
//
//         // Assert
//         Assert.AreEqual(expectedModifier, realModifier);
//     }
// }