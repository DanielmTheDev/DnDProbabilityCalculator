using DnDProbabilityCalculator.Application.Probabilities;
using DnDProbabilityCalculator.Core.Adventuring;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DnDProbabilityCalculator.Application.Tests.Probabilities;

[TestClass]
public class ProbabilityTableDataTests
{
    [TestInitialize]
    public void Initialize()
    {

    }

    [TestMethod]
    public void FromActor_WhenCalled_ReturnsProbabilityTableData()
    {
        // Arrange
        var actor = GetValidActor();

        // Act
        var tableData = ProbabilityTableData.FromActor(actor, new[]{10, 12, 14});

        // Assert
        Assert.AreEqual("Durak", tableData.Header);

        new List<string> { "DC", "10", "12", "14" }.AssertElementsAreContainedIn(tableData.DcRow);
        new List<string> { "Str (10)", "55%", "45%", "35%" }.AssertElementsAreContainedIn(tableData.StrengthRow);
        new List<string> { "Dex (12)", "105%", "95%", "85%" }.AssertElementsAreContainedIn(tableData.DexterityRow);
        new List<string> { "Con (14)", "65%", "55%", "45%" }.AssertElementsAreContainedIn(tableData.ConstitutionRow);
        new List<string> { "Wis (15)", "65%", "55%", "45%" }.AssertElementsAreContainedIn(tableData.WisdomRow);
        new List<string> { "Int (16)", "70%", "60%", "50%" }.AssertElementsAreContainedIn(tableData.IntelligenceRow);
        new List<string> { "Cha (8)", "50%", "40%", "30%" }.AssertElementsAreContainedIn(tableData.CharismaRow);
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
            .Build();
}