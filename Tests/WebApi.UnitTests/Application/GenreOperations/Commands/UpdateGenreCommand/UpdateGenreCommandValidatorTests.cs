using FluentAssertions;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;

namespace WebApi.UnitTests.Application.GenreOperations.Commands.UpdateGenreCommand;

public class UpdateGenreCommandValidatorTests
{
    [Theory]
    [InlineData("xyz")]
    public void WhenModelIsInvalid_Validator_ShouldHaveError(string name)
    {
        var model = new UpdateGenreModel { Name = name };
        WebApi.Application.GenreOperations.Commands.UpdateGenre.UpdateGenreCommand updateCommand = new WebApi.Application.GenreOperations.Commands.UpdateGenre.UpdateGenreCommand(null);
        updateCommand.GenreId = 1;
        updateCommand.Model = model;

        UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
        var result = validator.Validate(updateCommand);
        
        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Theory]
    [InlineData("Adventure")]
    [InlineData("Fantasy")]
    public void WhenInputsAreValid_Validator_ShouldNotHaveError(string name)
    {
        // arrange
        var model = new UpdateGenreModel { Name = name };
        WebApi.Application.GenreOperations.Commands.UpdateGenre.UpdateGenreCommand updateCommand = new WebApi.Application.GenreOperations.Commands.UpdateGenre.UpdateGenreCommand(null);
        updateCommand.GenreId = 2;
        updateCommand.Model = model;

        UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
        var result = validator.Validate(updateCommand);

        result.Errors.Count.Should().Be(0);
    }
}