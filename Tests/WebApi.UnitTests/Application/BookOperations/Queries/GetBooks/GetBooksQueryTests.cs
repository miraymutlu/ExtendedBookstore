using AutoMapper;
using FluentAssertions;
using WebApi.BookOperations.GetBooks;
using WebApi.DBOperations;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.BookOperations.Queries.GetBooks;

public class GetBooksQueryTests : IClassFixture<CommonTestFixture>
{
    private readonly BookstoreDbContext _context;
    private readonly IMapper _mapper;

    public GetBooksQueryTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }
    
    [Fact]
    public void WhenGetBooksQueryIsHandled_BookListShouldBeReturned()
    {
        
        var query = new GetBooksQuery(_context, _mapper);
        
        var result = query.Handle();

        result.Should().NotBeNull();
        result.Should().HaveCount(3);

        result[0].Title.Should().Be("Lean Startup");
        result[0].Genre.Should().Be("Personal Growth");
        result[0].PageCount.Should().Be(200);
        result[0].PublishDate.Should().Be("12.06.2001 00:00:00");

        result[1].Title.Should().Be("Herland");
        result[1].Genre.Should().Be("Science Fiction");
        result[1].PageCount.Should().Be(250);
        result[1].PublishDate.Should().Be("23.05.2010 00:00:00");

        result[2].Title.Should().Be("Dune");
        result[2].Genre.Should().Be("Science Fiction");
        result[2].PageCount.Should().Be(540);
        result[2].PublishDate.Should().Be("21.12.2001 00:00:00");

    }
}