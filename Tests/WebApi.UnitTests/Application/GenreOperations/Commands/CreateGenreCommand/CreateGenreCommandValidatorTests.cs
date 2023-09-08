using FluentAssertions;
using WebApi.Application.GenreOperations.Commands.CreateGenre;

namespace WebApi.UnitTests.Application.GenreOperations.Commands.CreateGenreCommand;

public class CreateGenreCommandValidatorTests 
{
    [Theory]
    [InlineData("")]
    public void WhenInvalidInputsAreGiven_Validator_ShouldReturnErrors(string name)
    {
        WebApi.Application.GenreOperations.Commands.CreateGenre.CreateGenreCommand command = new WebApi.Application.GenreOperations.Commands.CreateGenre.CreateGenreCommand(null);
        command.Model = new CreateGenreModel() { Name = name };

        CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
        
        var result = validator.Validate(command);
        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Theory]
    [InlineData("Adventure")]
    [InlineData("Novel")]
    public void WhenValidInputsAreGiven_Validator_ShouldNotReturnError(string name)
    {
        WebApi.Application.GenreOperations.Commands.CreateGenre.CreateGenreCommand command = new WebApi.Application.GenreOperations.Commands.CreateGenre.CreateGenreCommand(null);
        command.Model = new CreateGenreModel() { Name = name };

        CreateGenreCommandValidator validator = new CreateGenreCommandValidator();

        var result = validator.Validate(command);
        result.Errors.Count.Should().Be(0);
    }
}