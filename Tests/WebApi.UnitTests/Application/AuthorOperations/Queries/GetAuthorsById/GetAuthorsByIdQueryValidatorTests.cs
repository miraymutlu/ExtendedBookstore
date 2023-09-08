using FluentAssertions;
using WebApi.Application.AuthorOperations.Queries.GetAuthorsById;

namespace WebApi.UnitTests.Application.AuthorOperations.Queries.GetAuthorsById;

public class GetAuthorsByIdQueryValidatorTests
{
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-100)]
    public void WhenAuthorIdLessThanOrEqualZero_ValidationShouldReturnError(int authorId)
    {
        GetAuthorsByIdQuery query = new GetAuthorsByIdQuery(null, null);
        query.Id = authorId;

        GetAuthorsByIdQueryValidator validator = new GetAuthorsByIdQueryValidator();
        var result = validator.Validate(query);

        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenAuthorIdGreaterThanZero_ValidationShouldNotReturnError()
    {
        GetAuthorsByIdQuery query = new(null, null);
        query.Id = 12;

        GetAuthorsByIdQueryValidator validator = new GetAuthorsByIdQueryValidator();
        var result = validator.Validate(query);

        result.Errors.Count.Should().Be(0);
    }
}