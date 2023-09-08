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