using FluentAssertions;
using WebApi.Application.AuthorOperations.Commands.CreateAuthorCommand;

namespace WebApi.UnitTests.Application.AuthorOperations.Commands.CreateAuthorCommand;

public class CreateAuthorCommandValidatorTests
{
    [Theory]
    [InlineData("")]
    public void WhenNameIsInvalid_Validator_ShouldReturnError(string name)
    {
        var command = new WebApi.Application.AuthorOperations.Commands.CreateAuthorCommand.CreateAuthorCommand(null, null);
        var Model = new CreateAuthorViewModel { Name = name, Surname = "Test", Birthday = new DateTime(1990, 1, 1) };

        command.Model = Model;

        var validator = new CreateAuthorCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenBirthdayIsAfterToday_Validator_ShouldReturnError()
    {
        var command = new WebApi.Application.AuthorOperations.Commands.CreateAuthorCommand.CreateAuthorCommand(null, null);
        var Model = new CreateAuthorViewModel { Name = "Deneme", Surname = "Test", Birthday = DateTime.Now.AddDays(1) };

        command.Model = Model;

        var validator = new CreateAuthorCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Should().ContainSingle();
    }

    [Fact]
    public void WhenModelIsValid_Validator_ShouldNotReturnError()
    {
        var command = new WebApi.Application.AuthorOperations.Commands.CreateAuthorCommand.CreateAuthorCommand(null, null);
        var Model = new CreateAuthorViewModel { Name = "Deneme", Surname = "Test", Birthday = new DateTime(1990, 1, 1) };

        command.Model = Model;

        var validator = new CreateAuthorCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Should().BeEmpty();
    }
}