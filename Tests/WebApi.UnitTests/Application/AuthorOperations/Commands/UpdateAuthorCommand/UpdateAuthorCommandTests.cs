using FluentAssertions;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.AuthorOperations.Commands.UpdateAuthorCommand;

public class UpdateAuthorCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly BookstoreDbContext _context;

    public UpdateAuthorCommandTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
    }

    [Fact]
    public void WhenGivenAuthorIsNotFound_InvalidOperationException_ShouldBeReturn()
    {
        WebApi.Application.AuthorOperations.Commands.UpdateAuthor.UpdateAuthorCommand command = new WebApi.Application.AuthorOperations.Commands.UpdateAuthor.UpdateAuthorCommand(_context);
        command.Id= 9;

        FluentActions
            .Invoking(() => command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("The author does not exists.");
    }

    [Fact]
    public void WhenValidInputsAreGiven_Author_ShouldBeUpdated()
    {
        WebApi.Application.AuthorOperations.Commands.UpdateAuthor.UpdateAuthorCommand command = new WebApi.Application.AuthorOperations.Commands.UpdateAuthor.UpdateAuthorCommand(_context);
        var author = new Author { Name = "Carol Rifka", Surname = "Brunt", Birthday = new DateTime(2000, 11, 22) };

        _context.Authors.Add(author);
        _context.SaveChanges();

        command.Id = author.Id;
        UpdateAuthorViewModel model = new UpdateAuthorViewModel { Name = "Janet", Surname = "Evanovich", Birthday = new DateTime(2002, 11, 22) };
        command.Model = model;

        FluentActions.Invoking(() => command.Handle()).Invoke();

        var updatedAuthor = _context.Authors.SingleOrDefault(a => a.Id == author.Id);
        updatedAuthor.Should().NotBeNull();
        updatedAuthor.Name.Should().Be(model.Name);
        updatedAuthor.Surname.Should().Be(model.Surname);
        updatedAuthor.Birthday.Should().Be(model.Birthday);
    }
}