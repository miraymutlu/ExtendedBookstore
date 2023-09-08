using FluentAssertions;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.GenreOperations.Commands.UpdateGenreCommand;

public class UpdateGenreCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly BookstoreDbContext _context;

    public UpdateGenreCommandTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
    }

    [Fact]
    public void WhenGivenGenreIsNotFound_InvalidOperationException_ShouldBeReturn()
    {
        WebApi.Application.GenreOperations.Commands.UpdateGenre.UpdateGenreCommand command = new WebApi.Application.GenreOperations.Commands.UpdateGenre.UpdateGenreCommand(_context);
        command.GenreId = 10;

        FluentActions
            .Invoking(() => command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("The book genre not found.");
    }

    [Fact]
    public void WhenGivenGenreNameAlreadyExists_InvalidOperationException_ShouldBeReturn()
    {
        WebApi.Application.GenreOperations.Commands.UpdateGenre.UpdateGenreCommand command = new WebApi.Application.GenreOperations.Commands.UpdateGenre.UpdateGenreCommand(_context);
        var genre = new Genre { Name = "Adventure" };

        _context.Genres.Add(genre);
        _context.SaveChanges();

        var genre1 = new Genre { Name = "Romance" };

        _context.Genres.Add(genre1);
        _context.SaveChanges();

        command.GenreId = genre1.Id;
        UpdateGenreModel model = new UpdateGenreModel { Name = "Romance" };
        command.Model = model;

        FluentActions
            .Invoking(() => command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("A book genre with the same name already exists");
    }

    [Fact]
    public void WhenValidInputsAreGiven_Genre_ShouldBeUpdated()
    {
        WebApi.Application.GenreOperations.Commands.UpdateGenre.UpdateGenreCommand command = new WebApi.Application.GenreOperations.Commands.UpdateGenre.UpdateGenreCommand(_context);
        var genre = new Genre { Name = "Novel" };

        _context.Genres.Add(genre);
        _context.SaveChanges();

        command.GenreId = genre.Id;
        UpdateGenreModel model = new UpdateGenreModel { Name = "History" };
        command.Model = model;

        FluentActions.Invoking(() => command.Handle()).Invoke();
        
        var updatedGenre = _context.Genres.SingleOrDefault(g => g.Id == genre.Id);
        updatedGenre.Should().NotBeNull();
        updatedGenre.IsActive.Should().BeTrue();
        updatedGenre.Name.Should().Be(model.Name);
    }
}