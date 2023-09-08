using AutoMapper;
using FluentAssertions;
using WebApi.Application.GenreOperations.Queries.GetGenres;
using WebApi.DBOperations;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.GenreOperations.Queries.GetGenres;

public class GetGenresQueryTests : IClassFixture<CommonTestFixture>
{
    private readonly BookstoreDbContext _context;
    private readonly IMapper _mapper;

    public GetGenresQueryTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenGetGenresQueryIsHandled_GenreListShouldBeReturned()
    {
        var query = new GetGenresQuery(_context, _mapper);

        var result = query.Handle();

        result.Should().NotBeNull();
        result.Should().HaveCount(3);

        result[0].Name.Should().Be("Personal Growth");

        result[1].Name.Should().Be("Science Fiction");

        result[2].Name.Should().Be("Fantasy");

    }
}