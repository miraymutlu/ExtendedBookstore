using FluentAssertions;
using WebApi.BookOperations.GetById;

namespace WebApi.UnitTests.Application.BookOperations.Queries.GetBooksById;

public class GetBooksByIdQueryValidatorTests
{
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-100)]
    public void WhenBookIdLessThanOrEqualZero_ValidationShouldReturnError(int bookId)
    {
        GetByIdQuery query = new GetByIdQuery(null, null);
        query.BookID = bookId;

        GetByIdQueryValidator validator = new GetByIdQueryValidator();
        var result = validator.Validate(query);
        
        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenBookIdGreaterThanZero_ValidationShouldNotReturnError()
    {
        GetByIdQuery query = new(null, null);
        query.BookID = 15;

        GetByIdQueryValidator validator = new GetByIdQueryValidator();
        var result = validator.Validate(query);
        
        result.Errors.Count.Should().Be(0);
    }
}