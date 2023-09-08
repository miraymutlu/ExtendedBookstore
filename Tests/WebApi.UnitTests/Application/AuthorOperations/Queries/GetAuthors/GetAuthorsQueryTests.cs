using AutoMapper;
using FluentAssertions;
using WebApi.Application.AuthorOperations.Queries;
using WebApi.DBOperations;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.AuthorOperations.Queries.GetAuthors;

public class GetAuthorsQueryTests : IClassFixture<CommonTestFixture>
{
    private readonly BookstoreDbContext _context;
    private readonly IMapper _mapper;

    public GetAuthorsQueryTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenGetAuthorsQueryIsHandled_AuthorListShouldBeReturned()
    {
        var query = new GetAuthorsQuery(_context, _mapper);

        var result = query.Handle();

        result.Should().NotBeNull();
        result.Should().HaveCount(3);

        result[0].Name.Should().Be("George");
        result[0].Surname.Should().Be("Saunders");
        result[0].Birthday.Should().Be(new DateTime(1970, 01, 06));

        result[1].Name.Should().Be("Jean Louis");
        result[1].Surname.Should().Be("Fourniere");
        result[1].Birthday.Should().Be(new DateTime(1938,12,16));

        result[2].Name.Should().Be("Haruki");
        result[2].Surname.Should().Be("Murakami");
        result[2].Birthday.Should().Be(new DateTime(1956,06,24));
    }
}