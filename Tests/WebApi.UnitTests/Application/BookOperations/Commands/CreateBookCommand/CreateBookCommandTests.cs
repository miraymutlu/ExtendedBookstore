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
}