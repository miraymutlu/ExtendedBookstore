using FluentAssertions;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.BookOperations.Commands.UpdateBookCommand;

public class UpdateBookCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly BookstoreDbContext _context;

    public UpdateBookCommandTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
    }

    [Fact]
    public void WhenGivenBookIsNotFound_InvalidOperationException_ShouldBeReturn()
    {
        WebApi.BookOperations.UpdateBook.UpdateBookCommand command = new WebApi.BookOperations.UpdateBook.UpdateBookCommand(_context);
        command.BookId = 13;

        FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should()
            .Be("The book does not exists.");
    }

    [Fact]
    public void WhenValidInputsAreGiven_Book_ShouldBeUpdated()
    {
        WebApi.BookOperations.UpdateBook.UpdateBookCommand command =
            new WebApi.BookOperations.UpdateBook.UpdateBookCommand(_context);
        var book = new Book() {Title = "Test", GenreId = 1, PageCount = 100, PublishDate = DateTime.Now.Date.AddYears(-10)};

        _context.Books.Add(book);
        _context.SaveChanges();

        command.BookId = book.Id;
        WebApi.BookOperations.UpdateBook.UpdateBookCommand.UpdateBookModel model =
            new WebApi.BookOperations.UpdateBook.UpdateBookCommand.UpdateBookModel()
            {
                Title = "Lord Of The Rings",
                PageCount = 1300,
                PublishDate = DateTime.Now.Date.AddYears(-10),
                GenreId = 3
            };
        command.Model = model;
        
        FluentActions.Invoking(() => command.Handle()).Invoke();

        var updatedBook = _context.Books.SingleOrDefault(updatedBook => updatedBook.Id == book.Id);
        updatedBook.Should().NotBeNull();
        updatedBook.PublishDate.Should().Be(model.PublishDate);
        updatedBook.PageCount.Should().Be(model.PageCount);
        updatedBook.GenreId.Should().Be(model.GenreId);
        updatedBook.Title.Should().Be(model.Title);
    }
}