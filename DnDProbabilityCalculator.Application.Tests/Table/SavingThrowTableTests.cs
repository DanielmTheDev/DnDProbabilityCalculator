using DnDProbabilityCalculator.Application.Table;
using DnDProbabilityCalculator.Core.Adventuring;
using DnDProbabilityCalculator.Core.Adventuring.Abilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DnDProbabilityCalculator.Application.Tests.Table;

[TestClass]
public class SavingThrowTableTests
{
    [TestMethod]
    public void FromActor_WhenCalled_ReturnsSavingThrowTable()
    {
        // Arrange
        var actor = GetValidActor();

        // Act
        var tableData = SavingThrowTable.FromActor(actor, new[] { 10, 12, 14 });

        // Assert
        new List<string> { "Ability/DC", "10", "12", "14" }.AssertElementsAreContainedIn(tableData.Dcs);
        new List<string> { "Dex (12)", "105%", "95%", "85%" }.AssertElementsAreContainedIn(tableData.Probabilities[0]);
        new List<string> { "Str (10)", "55%", "45%", "35%" }.AssertElementsAreContainedIn(tableData.Probabilities[1]);
        new List<string> { "Con (14)", "65%", "55%", "45%" }.AssertElementsAreContainedIn(tableData.Probabilities[2]);
        new List<string> { "Int (16)", "70%", "60%", "50%" }.AssertElementsAreContainedIn(tableData.Probabilities[3]);
        new List<string> { "Cha (8)", "50%", "40%", "30%" }.AssertElementsAreContainedIn(tableData.Probabilities[4]);
        new List<string> { "Wis (15)", "65%", "55%", "45%" }.AssertElementsAreContainedIn(tableData.Probabilities[5]);
    }

    private static Actor GetValidActor()
        => Actor.New()
            .WithName("Durak")
            .WithStrength(10)
            .WithDexterity(12, true)
            .WithConstitution(14)
            .WithWisdom(15)
            .WithIntelligence(16)
            .WithCharisma(8)
            .WithProficiency(9)
            .WithArmorClass(5)
            .WithNumberOfAttacks(2)
            .WithAttackAbility(AbilityScoreType.Dexterity)
            .Build();
}