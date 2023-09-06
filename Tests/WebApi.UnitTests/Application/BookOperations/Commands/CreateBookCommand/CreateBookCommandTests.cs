using AutoMapper;
using FluentAssertions;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.BookOperations.Commands.CreateBookCommand;

public class CreateBookCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly BookstoreDbContext _context;
    private readonly IMapper _mapper;

    public CreateBookCommandTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn()
    {
        //arrange
        var book = new Book()
        {
            Title = "Test_WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn", 
            PageCount = 100,
            PublishDate = new System.DateTime(1990, 01, 01),
            GenreId = 1
        };
        _context.Books.Add(book);
        _context.SaveChanges();

        WebApi.BookOperations.CreateBook.CreateBookCommand command = new WebApi.BookOperations.CreateBook.CreateBookCommand(_context, _mapper);
        command.Model = new WebApi.BookOperations.CreateBook.CreateBookCommand.CreateBookModel() { Title = book.Title };

        //act & assert
        FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should()
            .Be("The book is already exists.");
    }

    [Fact]
    public void WhenValidInputsAreGiven_Book_ShouldBeCreated()
    {
        WebApi.BookOperations.CreateBook.CreateBookCommand command = new WebApi.BookOperations.CreateBook.CreateBookCommand(_context, _mapper);
        WebApi.BookOperations.CreateBook.CreateBookCommand.CreateBookModel model =
            new WebApi.BookOperations.CreateBook.CreateBookCommand.CreateBookModel(){Title = "Lord Of The Rings" , PageCount = 1300, PublishDate = DateTime.Now.Date.AddYears(-10), GenreId = 3 };;
        command.Model = model;
        
        FluentActions.Invoking(() => command.Handle()).Invoke();

        var book = _context.Books.SingleOrDefault(book => book.Title == model.Title);
        book.Should().NotBeNull();
        book.PageCount.Should().Be(model.PageCount);
        book.PublishDate.Should().Be(model.PublishDate);
        book.GenreId.Should().Be(model.GenreId);
    }
}