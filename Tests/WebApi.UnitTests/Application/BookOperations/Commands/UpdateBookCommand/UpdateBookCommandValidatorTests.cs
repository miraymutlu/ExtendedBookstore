using FluentAssertions;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.UpdateBook;

namespace WebApi.UnitTests.Application.BookOperations.Commands.UpdateBookCommand;

public class UpdateBookCommandValidatorTests
{
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-100)]
    public void WhenBookIdIsInvalid_Validator_ShouldHaveError(int bookId)
    {
        var model = new WebApi.BookOperations.UpdateBook.UpdateBookCommand.UpdateBookModel { Title = "Title", GenreId = 3, PageCount = 100, PublishDate = DateTime.Now.Date.AddYears(-2)};
        WebApi.BookOperations.UpdateBook.UpdateBookCommand command = new WebApi.BookOperations.UpdateBook.UpdateBookCommand(null);
        command.Model = model;
        command.BookId = bookId;

        UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
        var result = validator.Validate(command);
        
        result.Errors.Count.Should().BeGreaterThan(0);
    }
    
    [Theory]
    [InlineData("Lord Of The Rings",0,0)]
    [InlineData("Lord Of The Rings",0,1)]
    [InlineData("Lord Of The Rings",100,0)]
    [InlineData("",0,0)]
    [InlineData("",100,1)]
    [InlineData("",0,1)]
    [InlineData(" ",100,1)]
    public void WhenModelIsInvalid_Validator_ShouldBeReturnErrors(string title, int pageCount, int genreId)
    {
        WebApi.BookOperations.UpdateBook.UpdateBookCommand command = new WebApi.BookOperations.UpdateBook.UpdateBookCommand(null);
        command.Model = new WebApi.BookOperations.UpdateBook.UpdateBookCommand.UpdateBookModel()
        {
            Title = title,
            PageCount = pageCount,
            PublishDate = DateTime.Now.Date.AddYears(-1),
            GenreId = genreId
        };
        
        UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().BeGreaterThan(0);
    }
    
    [Fact]
    public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError()
    {
        WebApi.BookOperations.UpdateBook.UpdateBookCommand command = new WebApi.BookOperations.UpdateBook.UpdateBookCommand(null);
        command.Model = new WebApi.BookOperations.UpdateBook.UpdateBookCommand.UpdateBookModel()
        {
            Title = "Lord Of The Rings",
            PageCount = 1200,
            PublishDate = DateTime.Now.Date,
            GenreId = 3
        };
        
        UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
        var result = validator.Validate(command);
        
        result.Errors.Count.Should().BeGreaterThan(0);
    }
    
    [Theory]
    [InlineData("Title", 1, 100)]
    [InlineData("Long Title", 2, 200)]
    public void WhenInputsAreValid_Validator_ShouldNotHaveError(string title, int genreId, int pageCount)
    {
        var model = new WebApi.BookOperations.UpdateBook.UpdateBookCommand.UpdateBookModel { Title = title, GenreId = genreId, PageCount = pageCount,             PublishDate = DateTime.Now.Date.AddYears(-1),};
        WebApi.BookOperations.UpdateBook.UpdateBookCommand updateCommand = new WebApi.BookOperations.UpdateBook.UpdateBookCommand(null);
        updateCommand.BookId = 1;
        updateCommand.Model = model;

        UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
        var result = validator.Validate(updateCommand);
        
        result.Errors.Count.Should().Be(0);
    }
}