using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using WebApi.BookOperations.GetById;
using WebApi.DBOperations;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.BookOperations.Queries.GetBooksById;

public class GetBooksByIdQueryTests : IClassFixture<CommonTestFixture>
{
    private readonly BookstoreDbContext _context;
    private readonly IMapper _mapper;

    public GetBooksByIdQueryTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }
    
    [Fact]
    public void WhenValidInputsAreGiven_Book_ShouldBeReturned()
    {
        GetByIdQuery query = new GetByIdQuery(_context, _mapper);
        var BookId = query.BookID = 1;

        var book = _context.Books.Include(x => x.Genre).Where(b => b.Id == BookId).SingleOrDefault();
        
        GetByIdQuery.BookByIdViewModel vm = query.Handle();

        vm.Should().NotBeNull();
        vm.Title.Should().Be(book.Title);
        vm.PageCount.Should().Be(book.PageCount);
        vm.Genre.Should().Be(book.Genre.Name);
        vm.PublishDate.Should().Be(book.PublishDate.ToString("dd/MM/yyyy 00:00:00"));
    }

    [Fact]
    public void WhenNonExistingBookIdIsGiven_InvalidOperationException_ShouldBeReturn()
    {
        int bookId = 11;

        GetByIdQuery query = new GetByIdQuery(_context, _mapper);
        query.BookID = bookId;
        
        query.Invoking(x => x.Handle())
            .Should().Throw<InvalidOperationException>()
            .And.Message.Should().Be("The book does not exists.");
    }
}