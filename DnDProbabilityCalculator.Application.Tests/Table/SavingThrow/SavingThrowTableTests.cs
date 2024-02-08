using DnDProbabilityCalculator.Application.Table.SavingThrow;
using DnDProbabilityCalculator.Core.Adventuring;
using DnDProbabilityCalculator.Core.Adventuring.Abilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DnDProbabilityCalculator.Application.Tests.Table.SavingThrow;

[TestClass]
public class SavingThrowTableTests
{
    [TestMethod]
    public void FromActor_WhenCalled_ReturnsSavingThrowTable()
    {
        // Arrange
        var actor = GetValidActor();

        // Act
        var tableData = SavingThrowTable.FromActor(actor, new(new[] { 10, 12, 14 }, new[] { 10, 12, 14 }, new[] { 10, 12, 14 }, 1, AdvantageType.None));

        // Assert
        CollectionAssert.AreEquivalent(new List<int> { 10, 12, 14 }, tableData.Dcs);
        // new List<string> { $"{"Dex".AsGreen()} (12)", "100%", "95%", "85%" }.AssertElementsAreContainedIn(tableData.Probabilities[0]);
        // new List<string> { "Str (10)", "55%", "45%", "35%" }.AssertElementsAreContainedIn(tableData.Probabilities[1]);
        // new List<string> { "Con (14)", "65%", "55%", "45%" }.AssertElementsAreContainedIn(tableData.Probabilities[2]);
        // new List<string> { "Int (16)", "70%", "60%", "50%" }.AssertElementsAreContainedIn(tableData.Probabilities[3]);
        // new List<string> { "Cha (8)", "50%", "40%", "30%" }.AssertElementsAreContainedIn(tableData.Probabilities[4]);
        // new List<string> { "Wis (15)", "65%", "55%", "45%" }.AssertElementsAreContainedIn(tableData.Probabilities[5]);
    }

    private static Actor GetValidActor()
        => new()
        {
            Name = "Durak",
            AbilityScores = new()
            {
                Dexterity = new()
                {
                    Value = 12,
                    IsProficient = true
                },
                Strength = 10,
                Constitution = 14,
                Intelligence = 16,
                Wisdom = 15,
                Charisma = 8
            },
            ProficiencyBonus = 9,
            ArmorClass = 5,
            NumberOfAttacks = 2,
            Weapon = new()
            {
                NumberOfDice = 10,
                DiceSides = 6,
                Bonus = 2,
                MiscDamageBonus = 2
            },
            AttackAbility = AbilityScoreType.Dexterity,
            Id = Guid.NewGuid().ToString()
        };
}