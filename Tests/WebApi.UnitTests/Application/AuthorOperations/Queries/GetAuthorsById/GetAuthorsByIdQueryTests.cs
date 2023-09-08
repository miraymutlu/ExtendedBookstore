using AutoMapper;
using FluentAssertions;
using WebApi.Application.AuthorOperations.Queries;
using WebApi.Application.AuthorOperations.Queries.GetAuthorsById;
using WebApi.DBOperations;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.AuthorOperations.Queries.GetAuthorsById;

public class GetAuthorsByIdQueryTests : IClassFixture<CommonTestFixture>
{
    private readonly BookstoreDbContext _context;
    private readonly IMapper _mapper;

    public GetAuthorsByIdQueryTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenValidInputsAreGiven_Author_ShouldBeReturned()
    {
        GetAuthorsByIdQuery query = new GetAuthorsByIdQuery(_context, _mapper);
        var AuthorId = query.Id = 1;

        var author = _context.Authors.Where(a => a.Id == AuthorId).SingleOrDefault();

        GetAuthorsByIdViewModel vm = query.Handle();

        vm.Should().NotBeNull();
        vm.Name.Should().Be(author.Name);
        vm.Surname.Should().Be(author.Surname);
        vm.Birthday.Should().Be(author.Birthday);
    }

    [Fact]
    public void WhenNonExistingAuthorIdIsGiven_InvalidOperationException_ShouldBeReturn()
    {
        int authorId = 11;

        GetAuthorsByIdQuery query = new GetAuthorsByIdQuery(_context, _mapper);
        query.Id = authorId;

        query.Invoking(x => x.Handle())
            .Should().Throw<InvalidOperationException>()
            .And.Message.Should().Be("The author does not exists.");
    }
}