using FluentAssertions;
using WebApi.Application.GenreOperations.Commands.DeleteGenre;

namespace WebApi.UnitTests.Application.GenreOperations.Commands.DeleteGenreCommand;

public class DeleteGenreCommandValidatorTests
{
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-100)]
    public void WhenGenreIdLessThanOrEqualZero_ValidationShouldReturnError(int genreId)
    {
        WebApi.Application.GenreOperations.Commands.DeleteGenre.DeleteGenreCommand command = new WebApi.Application.GenreOperations.Commands.DeleteGenre.DeleteGenreCommand(null);
        command.GenreId = genreId;

        DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
        var result = validator.Validate(command);
        
        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenGenreIdGreaterThanZero_ValidationShouldNotReturnError()
    {
        WebApi.Application.GenreOperations.Commands.DeleteGenre.DeleteGenreCommand command = new WebApi.Application.GenreOperations.Commands.DeleteGenre.DeleteGenreCommand(null);
        command.GenreId = 4;

        DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().Be(0);
    }
}