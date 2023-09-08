using AutoMapper;
using FluentAssertions;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.GenreOperations.Commands.CreateGenreCommand;

public class CreateGenreCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly BookstoreDbContext _context;

    public CreateGenreCommandTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
    }
    
    [Fact]
    public void WhenAlreadyExitsGenreNameIsGiven_InvalidOperationException_ShouldBeReturn()
    {
        var genre = new Genre()
        {
            Name = "Deneme",
            IsActive = true
        };
        _context.Genres.Add(genre);
        _context.SaveChanges();

        WebApi.Application.GenreOperations.Commands.CreateGenre.CreateGenreCommand command = new WebApi.Application.GenreOperations.Commands.CreateGenre.CreateGenreCommand(_context);
        command.Model = new CreateGenreModel() { Name = genre.Name };

        FluentActions
            .Invoking(() => command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("The book genre already exists.");

    }

    [Fact]
    public void WhenValidInputsAreGiven_Genre_ShouldBeCreated()
    {
        // arrange
        WebApi.Application.GenreOperations.Commands.CreateGenre.CreateGenreCommand command = new WebApi.Application.GenreOperations.Commands.CreateGenre.CreateGenreCommand(_context);
        CreateGenreModel model = new CreateGenreModel()
        {
            Name = "Romance",
        };

        command.Model = model;

        FluentActions.Invoking(() => command.Handle()).Invoke();
        
        var genre = _context.Genres.SingleOrDefault(genre => genre.Name == model.Name);
        genre.Should().NotBeNull();
        genre.IsActive.Should().BeTrue();
    }
}
