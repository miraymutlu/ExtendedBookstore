using FluentAssertions;
using WebApi.Application.AuthorOperations.Commands.DeleteAuthor;

namespace WebApi.UnitTests.Application.AuthorOperations.Commands.DeleteAuthorCommand;

public class DeleteAuthorCommandValidatorTests
{
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-100)]
    public void WhenAuthorIdLessThanOrEqualZero_ValidationShouldReturnError(int authorId)
    {
        WebApi.Application.AuthorOperations.Commands.DeleteAuthor.DeleteAuthorCommand command = new WebApi.Application.AuthorOperations.Commands.DeleteAuthor.DeleteAuthorCommand(null);
        command.Id = authorId;

        DeleteAuthorCommandValidator validator = new DeleteAuthorCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Theory]
    [InlineData(5)]
    [InlineData(10)]
    [InlineData(100)]
    public void WhenAuthorIdGreaterThanZero_ValidationShouldNotReturnError(int authorId)
    {
        WebApi.Application.AuthorOperations.Commands.DeleteAuthor.DeleteAuthorCommand command = new WebApi.Application.AuthorOperations.Commands.DeleteAuthor.DeleteAuthorCommand(null);
        command.Id = authorId;

        DeleteAuthorCommandValidator validator = new DeleteAuthorCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().Be(0);
    }
}