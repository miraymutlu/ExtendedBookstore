using FluentAssertions;
using WebApi.BookOperations.DeleteBook;

namespace WebApi.UnitTests.Application.BookOperations.Commands.DeleteBookCommand;

public class DeleteBookCommandValidatorTests
{
    [Theory]
    [InlineData(0)]
    [InlineData(8)]
    public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int id)
    {
        WebApi.BookOperations.DeleteBook.DeleteBookCommand command = new WebApi.BookOperations.DeleteBook.DeleteBookCommand(null);
        DeleteBookCommandValidator validator = new DeleteBookCommandValidator();

        var result = validator.Validate(command);

        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
    {
        WebApi.BookOperations.DeleteBook.DeleteBookCommand command = new WebApi.BookOperations.DeleteBook.DeleteBookCommand(null);
        command.BookId = 2;
        
        DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().Be(0);

    }

}