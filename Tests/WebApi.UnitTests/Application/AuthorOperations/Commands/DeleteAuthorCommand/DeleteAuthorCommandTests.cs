using FluentAssertions;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.AuthorOperations.Commands.DeleteAuthorCommand;

public class DeleteAuthorCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly BookstoreDbContext _context;

    public DeleteAuthorCommandTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
    }

    [Fact]
    public void WhenGivenAuthorIsNotFound_InvalidOperationException_ShouldBeReturn()
    {
        WebApi.Application.AuthorOperations.Commands.DeleteAuthor.DeleteAuthorCommand command = new WebApi.Application.AuthorOperations.Commands.DeleteAuthor.DeleteAuthorCommand(_context);
        command.Id = 20;

        FluentActions
            .Invoking(() => command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("The author does not exists.");
    }
    

    [Fact]
    public void WhenValidInputsAreGiven_Author_ShouldBeDeleted()
    {
        var newAuthor = new Author()
        {
            Name = "Haruki",
            Surname = "Murakami",
            Birthday = new DateTime(1970, 08, 07)
        };
        _context.Authors.Add(newAuthor);
        _context.SaveChanges();

        WebApi.Application.AuthorOperations.Commands.DeleteAuthor.DeleteAuthorCommand command = new WebApi.Application.AuthorOperations.Commands.DeleteAuthor.DeleteAuthorCommand(_context);
        command.Id = newAuthor.Id;

        FluentActions.Invoking(() => command.Handle()).Invoke();
        
        var author = _context.Authors.SingleOrDefault(a => a.Id == command.Id);
        author.Should().BeNull();
    }
}