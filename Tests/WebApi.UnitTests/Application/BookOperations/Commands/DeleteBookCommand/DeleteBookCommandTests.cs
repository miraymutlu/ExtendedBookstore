using AutoMapper;
using FluentAssertions;
using WebApi.DBOperations;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.BookOperations.Commands.DeleteBookCommand;

public class DeleteBookCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly BookstoreDbContext _context;

    public DeleteBookCommandTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
    }

    [Fact]
    public void WhenGivenBookIsNotFound_InvalidOperationException_ShouldBeReturn()
    {
        WebApi.BookOperations.DeleteBook.DeleteBookCommand command = new WebApi.BookOperations.DeleteBook.DeleteBookCommand(_context);
        command.BookId = 7;

        FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should()
            .Be("The book does not exists.");
    }

    [Fact]
    public void WhenValidInputsAreGiven_Book_ShouldBeDeleted()
    {
        WebApi.BookOperations.DeleteBook.DeleteBookCommand command = new WebApi.BookOperations.DeleteBook.DeleteBookCommand(_context);
        command.BookId = 1;
        
        FluentActions.Invoking(() => command.Handle()).Invoke();
        var book = _context.Books.SingleOrDefault(book => book.Id == command.BookId);
        book.Should().BeNull();

    }
    
}