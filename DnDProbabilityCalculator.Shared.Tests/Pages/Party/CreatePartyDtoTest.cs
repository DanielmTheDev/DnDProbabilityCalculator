using DnDProbabilityCalculator.Shared.Party;

namespace DnDProbabilityCalculator.Blazor.Tests.Pages.Party;

[TestClass]
public class CreatePartyDtoTest
{
    [TestMethod]
    public void Validate_WithValidParty_IsValid()
    {
        // Arrange
        var model = GetValidParty();

        // Act
        var result = new CreatePartyDtoValidator().Validate(model);

        // Assert
        Assert.IsTrue(result.IsValid);
    }

    [TestMethod]
    public void Validate_WithZeroCharacters_IsInvalid()
    {
        // Arrange
        var model = GetValidParty();
        model.Characters = [];

        // Act
        var result = new CreatePartyDtoValidator().Validate(model);

        // Assert
        Assert.IsFalse(result.IsValid);
        Assert.AreEqual("Your party must have at least one character", result.Errors[0].ErrorMessage);
    }

    [TestMethod]
    [DataRow("")]
    [DataRow(null)]
    public void Validate_WithoutPartyName_IsInvalid(string name)
    {
        // Arrange
        var model = GetValidParty();
        model.Name = name;

        // Act
        var result = new CreatePartyDtoValidator().Validate(model);

        // Assert
        Assert.IsFalse(result.IsValid);
        Assert.AreEqual("Your party must have a name", result.Errors[0].ErrorMessage);
    }

    [TestMethod]
    public void Validate_WithZeroNumberOfAttacks_IsInvalid()
    {
        // Arrange
        var model = GetValidParty();
        model.Characters[0].NumberOfAttacks = 0;

        // Act
        var result = new CreatePartyDtoValidator().Validate(model);

        // Assert
        Assert.IsFalse(result.IsValid);
        Assert.AreEqual("The number of attacks must be at least 1", result.Errors[0].ErrorMessage);
    }

    [TestMethod]
    public void Validate_WithBelowTwoProficiencyBonus_IsInvalid()
    {
        // Arrange
        var model = GetValidParty();
        model.Characters[0].ProficiencyBonus = 1;

        // Act
        var result = new CreatePartyDtoValidator().Validate(model);

        // Assert
        Assert.IsFalse(result.IsValid);
        Assert.AreEqual("Proficiency bonus must be at least 2", result.Errors[0].ErrorMessage);
    }

    [TestMethod]
    public void Validate_WithNegativeArmorClass_IsInvalid()
    {
        // Arrange
        var model = GetValidParty();
        model.Characters[0].ArmorClass = -1;

        // Act
        var result = new CreatePartyDtoValidator().Validate(model);

        // Assert
        Assert.IsFalse(result.IsValid);
        Assert.AreEqual("Armor class can't be negative", result.Errors[0].ErrorMessage);
    }

    private static CreatePartyDto GetValidParty()
        => new()
        {
            Name = "Party Name",
            Characters =
            [
                new()
                {
                    Name = "Character Name",
                    ProficiencyBonus = 2,
                    ArmorClass = 10,
                    NumberOfAttacks = 1,
                }
            ]
        };
}