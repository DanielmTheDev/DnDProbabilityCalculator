using DnDProbabilityCalculator.Core.Adventuring.Abilities;
using DnDProbabilityCalculator.Shared.PartyCreation;
using DnDProbabilityCalculator.Shared.PartyCreation.Validation;

namespace DnDProbabilityCalculator.Shared.Tests.Pages.Party;

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

    [TestMethod]
    public void CreateParty_WhenCalled_CreatesCorrespondingParty()
    {
        // Arrange
        var model = new CreatePartyDto
        {
            Name = "Testparty",
            Characters =
            [
                new()
                {
                    Name = "TestCharacter",
                    Strength = 10,
                    Dexterity = 10,
                    Constitution = 10,
                    Intelligence = 10,
                    Wisdom = 10,
                    Charisma = 10,
                    NumberOfAttacks = 1,
                    ProficiencyBonus = 2,
                    ArmorClass = 10,
                    AttackAbility = AbilityScoreType.Strength,
                    NumberOfDamageDice = 2,
                    DiceSides = 6,
                    Bonus = 1,
                    MiscDamageBonus = 5,
                }
            ]
        };

        // Act
        var result = model.ToParty("test");

        // Assert
        Assert.AreEqual("Testparty", result.Name);
        Assert.AreEqual(1, result.Characters.Count);
        Assert.AreEqual("TestCharacter", result.Characters[0].Name);
        Assert.AreEqual(10, result.Characters[0].AbilityScores.Strength.Value);
        Assert.AreEqual(10, result.Characters[0].AbilityScores.Dexterity.Value);
        Assert.AreEqual(10, result.Characters[0].AbilityScores.Constitution.Value);
        Assert.AreEqual(10, result.Characters[0].AbilityScores.Intelligence.Value);
        Assert.AreEqual(10, result.Characters[0].AbilityScores.Wisdom.Value);
        Assert.AreEqual(10, result.Characters[0].AbilityScores.Charisma.Value);
        Assert.AreEqual(1, result.Characters[0].NumberOfAttacks);
        Assert.AreEqual(2, result.Characters[0].ProficiencyBonus);
        Assert.AreEqual(10, result.Characters[0].ArmorClass);
        Assert.AreEqual(AbilityScoreType.Strength, result.Characters[0].AttackAbility);
        Assert.AreEqual(2, result.Characters[0].Weapon.NumberOfDice);
        Assert.AreEqual(6, result.Characters[0].Weapon.DiceSides);
        Assert.AreEqual(1, result.Characters[0].Weapon.Bonus);
        Assert.AreEqual(5, result.Characters[0].Weapon.MiscDamageBonus);
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