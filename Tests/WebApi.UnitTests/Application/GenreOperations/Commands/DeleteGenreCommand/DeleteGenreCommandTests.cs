using FluentAssertions;
using WebApi.DBOperations;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.GenreOperations.Commands.DeleteGenreCommand;

public class DeleteGenreCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly BookstoreDbContext _context;

    public DeleteGenreCommandTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
    }

    [Fact]
    public void WhenGivenGenreIsNotFound_InvalidOperationException_ShouldBeReturn()
    {
        WebApi.Application.GenreOperations.Commands.DeleteGenre.DeleteGenreCommand command = new WebApi.Application.GenreOperations.Commands.DeleteGenre.DeleteGenreCommand(_context);
        command.GenreId = 16;

        FluentActions
            .Invoking(() => command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("The book genre not found.");
    }

    [Fact]
    public void WhenValidInputsAreGiven_Genre_ShouldBeDeleted()
    {
        WebApi.Application.GenreOperations.Commands.DeleteGenre.DeleteGenreCommand command = new WebApi.Application.GenreOperations.Commands.DeleteGenre.DeleteGenreCommand(_context);
        command.GenreId = 1;

        FluentActions.Invoking(() => command.Handle()).Invoke();

        var genre = _context.Genres.SingleOrDefault(g => g.Id == command.GenreId);
        genre.Should().BeNull();
    }
}