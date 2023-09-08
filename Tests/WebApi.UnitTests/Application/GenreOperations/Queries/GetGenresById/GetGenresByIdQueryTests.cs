using AutoMapper;
using FluentAssertions;
using WebApi.Application.GenreOperations.Queries.GetGenresById;
using WebApi.DBOperations;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.GenreOperations.Queries.GetGenresById;

public class GetGenresByIdQueryTests : IClassFixture<CommonTestFixture>
{
    private readonly BookstoreDbContext _context;
    private readonly IMapper _mapper;

    public GetGenresByIdQueryTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenValidInputsAreGiven_Genre_ShouldBeReturned()
    {
        GetGenresByIdQuery query = new GetGenresByIdQuery(_context, _mapper);
        var GenreId = query.GenreId = 1;

        var genre = _context.Genres.Where(g => g.Id == GenreId).SingleOrDefault();

        GetGenresByIdQuery.GenresByIdViewModel vm = query.Handle();

        vm.Should().NotBeNull();
        vm.Name.Should().Be(genre.Name);
    }

    [Fact]
    public void WhenNonExistingGenreIdIsGiven_InvalidOperationException_ShouldBeReturn()
    {
        int genreId = 11;

        GetGenresByIdQuery query = new GetGenresByIdQuery(_context, _mapper);
        query.GenreId = genreId;

        query.Invoking(x => x.Handle())
            .Should().Throw<InvalidOperationException>()
            .And.Message.Should().Be("The book genre not found.");
    }
}