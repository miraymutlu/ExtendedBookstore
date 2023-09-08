using FluentAssertions;
using WebApi.Application.GenreOperations.Queries.GetGenresById;

namespace WebApi.UnitTests.Application.GenreOperations.Queries.GetGenresById;

public class GetGenresByIdQueryValidatorTests
{
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-100)]
    public void WhenGenreIdLessThanOrEqualZero_ValidationShouldReturnError(int genreId)
    {
        GetGenresByIdQuery query = new GetGenresByIdQuery(null, null);
        query.GenreId = genreId;

        GetGenresByIdQueryValidator validator = new GetGenresByIdQueryValidator();
        var result = validator.Validate(query);

        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenGenreIdGreaterThanZero_ValidationShouldNotReturnError()
    {
        GetGenresByIdQuery query = new GetGenresByIdQuery(null, null);
        query.GenreId = 12;

        GetGenresByIdQueryValidator validator = new GetGenresByIdQueryValidator();
        var result = validator.Validate(query);

        result.Errors.Count.Should().Be(0);
    }
}