using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DnDProbabilityCalculator.Blazor.Pages.Party;
using JetBrains.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DnDProbabilityCalculator.Blazor.Tests.Pages.Party;

[TestClass]
[TestSubject(typeof(CreatePartyForm))]
public class CreatePartyFormTest
{
    [TestMethod]
    public void Validate_WithValidParty_IsValid()
    {
        // Arrange
        var model = GetValidParty();
        var context = new ValidationContext(model);
        var results = new List<ValidationResult>();

        // Act
        var isValid = Validator.TryValidateObject(model, context, results, true);

        // Assert
        Assert.IsTrue(isValid);
    }


    [TestMethod]
    public void Validate_WithZeroCharacters_IsInvalid()
    {
        // Arrange
        var model = GetValidParty();
        model.Characters = [];
        var context = new ValidationContext(model);
        var results = new List<ValidationResult>();

        // Act
        var isValid = Validator.TryValidateObject(model, context, results, true);

        // Assert
        Assert.IsFalse(isValid);
        Assert.AreEqual("Your party must have at least one character", results[0].ErrorMessage);
    }

    [TestMethod]
    public void Validate_WithoutPartyName_IsInvalid()
    {
        // Arrange
        var model = GetValidParty();
        model.Name = null;
        var context = new ValidationContext(model);
        var results = new List<ValidationResult>();

        // Act
        var isValid = Validator.TryValidateObject(model, context, results, true);

        // Assert
        Assert.IsFalse(isValid);
        Assert.AreEqual(1, results.Count);
        Assert.AreEqual("The Name field is required.", results[0].ErrorMessage);
    }

    [TestMethod]
    public void Validate_WitEmptyPartyName_IsInvalid()
    {
        // Arrange
        var model = GetValidParty();
        model.Name = string.Empty;
        var context = new ValidationContext(model);
        var results = new List<ValidationResult>();

        // Act
        var isValid = Validator.TryValidateObject(model, context, results, true);

        // Assert
        Assert.IsFalse(isValid);
        Assert.AreEqual(1, results.Count);
        Assert.AreEqual("The Name field is required.", results[0].ErrorMessage);
    }


    private CreatePartyForm GetValidParty()
        => new()
        {
            Name = "Party Name",
            Characters = [new()
            {
                Name = "Character Name",
            }]
        };
}