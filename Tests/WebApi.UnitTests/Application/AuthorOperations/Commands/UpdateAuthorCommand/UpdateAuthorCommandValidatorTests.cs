using FluentAssertions;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;

namespace WebApi.UnitTests.Application.AuthorOperations.Commands.UpdateAuthorCommand;

public class UpdateAuthorCommandValidatorTests
{
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-100)]
    public void WhenAuthorIdIsInvalid_Validator_ShouldHaveError(int authorId)
    {
        var model = new UpdateAuthorViewModel { Name = "Orhan", Surname = "Veli", Birthday = new DateTime(1920, 11, 22) };
        WebApi.Application.AuthorOperations.Commands.UpdateAuthor.UpdateAuthorCommand command = new WebApi.Application.AuthorOperations.Commands.UpdateAuthor.UpdateAuthorCommand(null);
        command.Model = model;
        command.Id = authorId;

        UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Should().ContainSingle();
    }

    [Theory]
    [InlineData("", "")]
    [InlineData("123", "")]
    public void WhenModelIsInvalid_Validator_ShouldHaveError(string name, string surname)
    {
        var model = new UpdateAuthorViewModel { Name = name, Surname = surname, Birthday = new DateTime(2000, 11, 22) };
        WebApi.Application.AuthorOperations.Commands.UpdateAuthor.UpdateAuthorCommand updateCommand = new WebApi.Application.AuthorOperations.Commands.UpdateAuthor.UpdateAuthorCommand(null);
        updateCommand.Id = 3;
        updateCommand.Model = model;

        UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
        var result = validator.Validate(updateCommand);

        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenVirthDayEqualNowGiven_Validator_ShouldBeReturnError()
    {
        WebApi.Application.AuthorOperations.Commands.UpdateAuthor.UpdateAuthorCommand command = new WebApi.Application.AuthorOperations.Commands.UpdateAuthor.UpdateAuthorCommand(null);
        command.Model = new UpdateAuthorViewModel { Name = "Miray", Surname = "Mutlu", Birthday = DateTime.Now.Date };

        UpdateAuthorCommandValidator validator = new();
        var result = validator.Validate(command);

        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenInputsAreValid_Validator_ShouldNotHaveError()
    {
        var model = new UpdateAuthorViewModel { Name = "Defne", Surname = "Mutlu", Birthday = new DateTime(2000, 11, 22) };
        WebApi.Application.AuthorOperations.Commands.UpdateAuthor.UpdateAuthorCommand updateCommand = new WebApi.Application.AuthorOperations.Commands.UpdateAuthor.UpdateAuthorCommand(null);
        updateCommand.Id = 2;
        updateCommand.Model = model;

        UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
        var result = validator.Validate(updateCommand);

        result.Errors.Count.Should().Be(0);
    }
}