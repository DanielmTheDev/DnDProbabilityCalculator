using DnDProbabilityCalculator.Shared.Party.Validation;
using DnDProbabilityCalculator.Shared.PartyCreation;

namespace DnDProbabilityCalculator.Blazor.Tests.Pages.Party;

[TestClass]
public class CreatePartyDtoTests
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

    [TestMethod]
    public void Validate_WithNegativeAbilityScores_IsInvalid()
    {
        // Arrange
        var model = GetValidParty();
        model.Characters[0].Strength = -1;
        model.Characters[0].Dexterity = -1;
        model.Characters[0].Constitution = -1;
        model.Characters[0].Intelligence = -1;
        model.Characters[0].Wisdom = -1;
        model.Characters[0].Charisma = -1;

        // Act
        var result = new CreatePartyDtoValidator().Validate(model);

        // Assert
        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Select(error => error.ErrorMessage).Contains("Strength must be between 0 and 30"));
        Assert.IsTrue(result.Errors.Select(error => error.ErrorMessage).Contains("Dexterity must be between 0 and 30"));
        Assert.IsTrue(result.Errors.Select(error => error.ErrorMessage).Contains("Constitution must be between 0 and 30"));
        Assert.IsTrue(result.Errors.Select(error => error.ErrorMessage).Contains("Intelligence must be between 0 and 30"));
        Assert.IsTrue(result.Errors.Select(error => error.ErrorMessage).Contains("Wisdom must be between 0 and 30"));
        Assert.IsTrue(result.Errors.Select(error => error.ErrorMessage).Contains("Charisma must be between 0 and 30"));
    }

    [TestMethod]
    [DataRow(-1)]
    [DataRow(0)]
    public void Validate_NonPositiveNumberOfDamageDice_IsInvalid(int diceNumber)
    {
        // Arrange
        var model = GetValidParty();
        model.Characters[0].NumberOfDamageDice = diceNumber;

        // Act
        var result = new CreatePartyDtoValidator().Validate(model);

        // Assert
        Assert.IsFalse(result.IsValid);
        Assert.AreEqual("The number of damage dice must be at least 1", result.Errors[0].ErrorMessage);
    }

    [TestMethod]
    [DataRow(-1)]
    [DataRow(0)]
    [DataRow(3)]
    [DataRow(16)]
    public void Validate_WithIncorrectDiceSides_IsInvalid(int diceSides)
    {
        // Arrange
        var model = GetValidParty();
        model.Characters[0].DiceSides = diceSides;

        // Act
        var result = new CreatePartyDtoValidator().Validate(model);

        // Assert
        Assert.IsFalse(result.IsValid);
        Assert.AreEqual("The damage die number must be one of 4, 6, 8, 10, 12, 20", result.Errors[0].ErrorMessage);
    }

    private static CreatePartyDto GetValidParty()
        => new()
        {
            Name = "Party Name",
            Characters =
            [
                new()
                {
                    Name = "Character Name"
                }
            ]
        };
}