using AutoMapper;
using FluentAssertions;
using WebApi.Application.AuthorOperations.Commands.CreateAuthorCommand;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.AuthorOperations.Commands.CreateAuthorCommand;

public class CreateAuthorCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly BookstoreDbContext _context;
    private readonly IMapper _mapper;

    public CreateAuthorCommandTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenAlreadyExitAuthorNameSurnameIsGiven_InvalidOperationException_ShouldBeReturn()
    {
        var author = new Author ()
        { 
            Name = "Haruki",
            Surname= "Murakami",
            Birthday= new DateTime(1970,05,10)
        };
        _context.Authors.Add(author);
        _context.SaveChanges();

        WebApi.Application.AuthorOperations.Commands.CreateAuthorCommand.CreateAuthorCommand command = new WebApi.Application.AuthorOperations.Commands.CreateAuthorCommand.CreateAuthorCommand(_context ,_mapper);
        command.Model = new CreateAuthorViewModel()
        { 
            Name = author.Name,
            Surname = author.Surname,
            Birthday = author.Birthday
        };

        FluentActions
            .Invoking(() => command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("The author is already exists.");
    }

    [Fact]
    public void WhenValidInputsAreGiven_Author_ShouldBeCreated()
    {
        WebApi.Application.AuthorOperations.Commands.CreateAuthorCommand.CreateAuthorCommand command = new WebApi.Application.AuthorOperations.Commands.CreateAuthorCommand.CreateAuthorCommand(_context, _mapper);
        CreateAuthorViewModel model = new CreateAuthorViewModel()
        {
            Name = "Nikos",
            Surname = "Kazancakis",
            Birthday = new DateTime(1994, 08, 07)
        };

        command.Model = model;

        FluentActions.Invoking(() => command.Handle()).Invoke();
        
        var author = _context.Authors.SingleOrDefault(g => g.Name == model.Name);
        author.Should().NotBeNull();
        author.Name.Should().Be(model.Name);
        author.Surname.Should().Be(model.Surname);
        author.Birthday.Should().Be(model.Birthday);
    }
}