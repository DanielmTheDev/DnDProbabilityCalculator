﻿using DnDProbabilityCalculator.Core.Adventuring;

namespace DnDProbabilityCalculator.Core.Tests.Adventuring;

[TestClass]
public class ActorTests
{
    [TestMethod]
    public void FluentBuilder_WithValidValues_ReturnsFullyBuiltActor()
    {
        // Arrange and Act
        var actor = BuildValidActor();

        // Assert
        Assert.AreEqual("Durak", actor.Name);
        Assert.AreEqual(13, actor.AbilityScores.Strength);
        Assert.IsTrue(actor.AbilityScores.Strength.IsProficient);
        Assert.AreEqual(11, actor.AbilityScores.Dexterity);
        Assert.AreEqual(10, actor.AbilityScores.Constitution);
        Assert.IsTrue(actor.AbilityScores.Constitution.IsProficient);
        Assert.AreEqual(13, actor.AbilityScores.Wisdom);
        Assert.AreEqual(9, actor.AbilityScores.Intelligence);
        Assert.AreEqual(10, actor.AbilityScores.Charisma);
        Assert.AreEqual(5, actor.ArmorClass);
        Assert.AreEqual(4, actor.ProficiencyBonus);
    }

    [TestMethod]
    [DataRow(-1)]
    [DataRow(31)]
    public void FluentBuilder_WithTooLowAbilityScore_ThrowsException(int abilityScore)
    {
        // Arrange Act Assert
        var message = Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            Actor
                .New()
                .WithName("Durak")
                .WithStrength(abilityScore, true)
                .WithDexterity(11)
                .WithConstitution(10, true)
                .WithWisdom(13)
                .WithIntelligence(11)
                .WithCharisma(10)
                .WithProficiency(4)
                .WithArmorClass(5)
                .Build()).Message;
        Assert.IsTrue(message.Contains(ErrorMessages.Ability_Score_Out_Of_Range));
    }

    [TestMethod]
    public void CalculateSavingThrowSuccessChance_WithoutProficiency_ReturnsChanceForSuccess()
    {
        // Arrange
        var actor = BuildValidActor();

        // Act
        var dexterityChance = actor.DexteritySavingThrowSuccessChance(13);
        var charismaChance = actor.CharismaSavingThrowSuccessChance(13);
        var intelligenceChance = actor.IntelligenceSavingThrowSuccessChance(13);

        // Assert
        Assert.AreEqual(0.4, dexterityChance);
        Assert.AreEqual(0.4, charismaChance);
        Assert.AreEqual(0.35, intelligenceChance);
    }

    [TestMethod]
    public void CalculateSavingThrowSuccessChance_WithProficiency_ReturnsChanceForSuccess()
    {
        // Arrange
        var actor = BuildValidActor();

        // Act
        var strengthChance = actor.StrengthSavingThrowSuccessChance(16);
        var constChance = actor.ConstitutionSavingThrowSuccessChance(16);

        // Assert
        Assert.AreEqual(0.5, strengthChance);
        Assert.AreEqual(0.45, constChance);
    }

    [TestMethod]
    [DataRow(15, 5, 0.55)]
    [DataRow(20, 2, 0.15)]
    [DataRow(12, 8, 0.85)]
    [DataRow(10, 0, 0.55)]
    [DataRow(18, -2, 0.05)]
    public void GetHitChance_WithValidModifier_ReturnsChance(int ac, int modifier, double expectedChance)
    {
        // Arrange
        var actor = Actor
            .New()
            .WithName("Durak")
            .WithStrength(13, true)
            .WithDexterity(11)
            .WithConstitution(10, true)
            .WithWisdom(13)
            .WithIntelligence(9)
            .WithCharisma(10)
            .WithProficiency(4)
            .WithArmorClass(ac)
            .Build();

        // Act
        var actualChance = actor.GetHitChance(modifier);

        // Assert
        Assert.AreEqual(expectedChance, actualChance);
    }

    private static Actor BuildValidActor()
        => Actor
            .New()
            .WithName("Durak")
            .WithStrength(13, true)
            .WithDexterity(11)
            .WithConstitution(10, true)
            .WithWisdom(13)
            .WithIntelligence(9)
            .WithCharisma(10)
            .WithProficiency(4)
            .WithArmorClass(5)
            .Build();
}