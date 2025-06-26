using FluentValidation.TestHelper;
using Nizzola.Domain.Models;
using Nizzola.Domain.Validators;

namespace Nizzola.Domain.Tests;

public class PersonTests
{
    private readonly PersonValidator _validator;

    public PersonTests()
    {
        _validator = new PersonValidator();
    }

    [Fact]
    public void Should_Have_Error_When_FirstName_Is_Empty()
    {
        var person = new Person { FirstName = string.Empty };
        var result = _validator.TestValidate(person);
        result.ShouldHaveValidationErrorFor(p => p.FirstName);
    }

    [Fact]
    public void Should_Have_Error_When_FirstName_Exceeds_MaxLength()
    {
        var person = new Person { FirstName = new string('A', 51) };
        var result = _validator.TestValidate(person);
        result.ShouldHaveValidationErrorFor(p => p.FirstName);
    }

    [Fact]
    public void Should_Have_Error_When_LastName_Is_Empty()
    {
        var person = new Person { LastName = string.Empty };
        var result = _validator.TestValidate(person);
        result.ShouldHaveValidationErrorFor(p => p.LastName);
    }

    [Fact]
    public void Should_Have_Error_When_LastName_Exceeds_MaxLength()
    {
        var person = new Person { LastName = new string('A', 51) };
        var result = _validator.TestValidate(person);
        result.ShouldHaveValidationErrorFor(p => p.LastName);
    }

    [Fact]
    public void Should_Have_Error_When_DateOfBirth_Is_In_The_Future()
    {
        var person = new Person { DateOfBirth = DateTime.Now.AddDays(1) };
        var result = _validator.TestValidate(person);
        result.ShouldHaveValidationErrorFor(p => p.DateOfBirth);
    }

    [Fact]
    public void Should_Have_Error_When_Email_Is_Empty()
    {
        var person = new Person { Email = string.Empty };
        var result = _validator.TestValidate(person);
        result.ShouldHaveValidationErrorFor(p => p.Email);
    }

    [Fact]
    public void Should_Have_Error_When_Email_Is_Invalid()
    {
        var person = new Person { Email = "invalid-email" };
        var result = _validator.TestValidate(person);
        result.ShouldHaveValidationErrorFor(p => p.Email);
    }

    [Fact]
    public void Should_Have_Error_When_PhoneNumber_Is_Empty()
    {
        var person = new Person { PhoneNumber = string.Empty };
        var result = _validator.TestValidate(person);
        result.ShouldHaveValidationErrorFor(p => p.PhoneNumber);
    }

    //[Fact]
    //public void Should_Have_Error_When_PhoneNumber_Is_Invalid()
    //{
    //    var person = new Person { PhoneNumber = "12345" };
    //    var result = _validator.TestValidate(person);
    //    result.ShouldHaveValidationErrorFor(p => p.PhoneNumber);
    //}

    [Fact]
    public void Should_Not_Have_Error_When_All_Fields_Are_Valid()
    {
        var person = new Person
        {
            FirstName = "John",
            LastName = "Doe",
            DateOfBirth = DateTime.Now.AddYears(-30),
            Email = "john.doe@example.com",
            PhoneNumber = "+1234567890"
        };
        var result = _validator.TestValidate(person);
        result.ShouldNotHaveAnyValidationErrors();
    }
}
