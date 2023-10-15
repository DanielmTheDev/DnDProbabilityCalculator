using DnDProbabilityCalculator.Application.Probabilities;
using DnDProbabilityCalculator.Core.Adventuring;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DnDProbabilityCalculator.Application.Tests.Probabilities;

[TestClass]
public class ProbabilityTableDataTests
{
    [TestMethod]
    public void FromActor_WhenCalled_ReturnsProbabilityTableData()
    {
        // Arrange
        var actor = GetValidActor();

        // Act
        var tableData = ProbabilityTableData.FromActor(actor, new[] { 10, 12, 14 });

        // Assert
        Assert.AreEqual("Durak", tableData.Header);

        new List<string> { "Ability/AC", "10", "12", "14" }.AssertElementsAreContainedIn(tableData.DcRow);
        new List<string> { "Dex (12)", "105%", "95%", "85%" }.AssertElementsAreContainedIn(tableData.AbilityRows[0]);
        new List<string> { "Str (10)", "55%", "45%", "35%" }.AssertElementsAreContainedIn(tableData.AbilityRows[1]);
        new List<string> { "Con (14)", "65%", "55%", "45%" }.AssertElementsAreContainedIn(tableData.AbilityRows[2]);
        new List<string> { "Int (16)", "70%", "60%", "50%" }.AssertElementsAreContainedIn(tableData.AbilityRows[3]);
        new List<string> { "Cha (8)", "50%", "40%", "30%" }.AssertElementsAreContainedIn(tableData.AbilityRows[4]);
        new List<string> { "Wis (15)", "65%", "55%", "45%" }.AssertElementsAreContainedIn(tableData.AbilityRows[5]);
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
            .Build();
}