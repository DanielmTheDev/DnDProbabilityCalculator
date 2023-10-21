using DnDProbabilityCalculator.Application.Probabilities;
using DnDProbabilityCalculator.Core.Adventuring;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DnDProbabilityCalculator.Application.Tests.Probabilities;

[TestClass]
public class ProbabilityTableDataTests
{
    [TestMethod]
    public void FromActor_WhenCalled_ReturnsSavingThrowTableData()
    {
        // Arrange
        var actor = GetValidActor();

        // Act
        var tableData = ProbabilityTableData.FromActor(actor, new[] { 10, 12, 14 }, new[] { -1, 0, 1 });

        // Assert
        Assert.AreEqual("Durak", tableData.Header);

        new List<string> { "Ability/AC", "10", "12", "14" }.AssertElementsAreContainedIn(tableData.DcRow);
        new List<string> { "Dex (12)", "105%", "95%", "85%" }.AssertElementsAreContainedIn(tableData.SavingThrowRows[0]);
        new List<string> { "Str (10)", "55%", "45%", "35%" }.AssertElementsAreContainedIn(tableData.SavingThrowRows[1]);
        new List<string> { "Con (14)", "65%", "55%", "45%" }.AssertElementsAreContainedIn(tableData.SavingThrowRows[2]);
        new List<string> { "Int (16)", "70%", "60%", "50%" }.AssertElementsAreContainedIn(tableData.SavingThrowRows[3]);
        new List<string> { "Cha (8)", "50%", "40%", "30%" }.AssertElementsAreContainedIn(tableData.SavingThrowRows[4]);
        new List<string> { "Wis (15)", "65%", "55%", "45%" }.AssertElementsAreContainedIn(tableData.SavingThrowRows[5]);
    }

    [TestMethod]
    public void FromActor_WhenCalled_ReturnsGetHitChanceTableData()
    {
        // Arrange
        var actor = GetValidActor();

        // Act
        var tableData = ProbabilityTableData.FromActor(actor, new[] { 10, 12, 14 }, new[] { -1, 0, 1 });

        // Assert
        Assert.AreEqual("Durak", tableData.Header);
        new List<string> { "#Attacks/Modifier", "-1", "0", "1" }.AssertElementsAreContainedIn(tableData.AttackModifierRow);
        new List<string> { "1", "75%", "80%", "85%" }.AssertElementsAreContainedIn(tableData.GetHitRows[0]);
    }

    [TestMethod]
    public void FromActor_WithDifferentNumberOfElementsInPassedCollections_ThrowsException()
    {
        // Arrange
        var actor = GetValidActor();

        // Act and Assert
        Assert.ThrowsException<ArgumentException>(() => ProbabilityTableData.FromActor(actor, new[] { 1 }, new[] { 1, 2 }));
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