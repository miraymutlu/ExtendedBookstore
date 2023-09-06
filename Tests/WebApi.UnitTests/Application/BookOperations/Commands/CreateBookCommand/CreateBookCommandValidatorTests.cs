using FluentAssertions;
using FluentValidation;
using WebApi.BookOperations.CreateBook;

namespace WebApi.UnitTests.Application.BookOperations.Commands.CreateBookCommand;

public class CreateBookCommandValidatorTests
{
    [Theory]
    [InlineData("Lord Of The Rings",0,0)]
    [InlineData("Lord Of The Rings",0,1)]
    [InlineData("Lord Of The Rings",100,0)]
    [InlineData("",0,0)]
    [InlineData("",100,1)]
    [InlineData("",0,1)]
    [InlineData(" ",100,1)]
    public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string title, int pageCount, int genreId)
    {
        //arrange
        WebApi.BookOperations.CreateBook.CreateBookCommand command = new WebApi.BookOperations.CreateBook.CreateBookCommand(null,null);
        command.Model = new WebApi.BookOperations.CreateBook.CreateBookCommand.CreateBookModel()
        {
            Title = title,
            PageCount = pageCount,
            PublishDate = DateTime.Now.Date.AddYears(-1),
            GenreId = genreId
        };
        
        //act
        CreateBookCommandValidator validator = new CreateBookCommandValidator();
        var result = validator.Validate(command);
        
        //assert
        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError()
    {
        WebApi.BookOperations.CreateBook.CreateBookCommand command = new WebApi.BookOperations.CreateBook.CreateBookCommand(null,null);
        command.Model = new WebApi.BookOperations.CreateBook.CreateBookCommand.CreateBookModel()
        {
            Title = "Lord Of The Rings",
            PageCount = 1200,
            PublishDate = DateTime.Now.Date,
            GenreId = 3
        };
        
        CreateBookCommandValidator validator = new CreateBookCommandValidator();
        var result = validator.Validate(command);
        
        result.Errors.Count.Should().BeGreaterThan(0);
    }
    
    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
    {
        WebApi.BookOperations.CreateBook.CreateBookCommand command = new WebApi.BookOperations.CreateBook.CreateBookCommand(null,null);
        command.Model = new WebApi.BookOperations.CreateBook.CreateBookCommand.CreateBookModel()
        {
            Title = "Lord Of The Rings",
            PageCount = 1200,
            PublishDate = DateTime.Now.Date.AddYears(-2),
            GenreId = 3
        };
        
        CreateBookCommandValidator validator = new CreateBookCommandValidator();
        var result = validator.Validate(command);
        
        result.Errors.Count.Should().Be(0);
    }
}