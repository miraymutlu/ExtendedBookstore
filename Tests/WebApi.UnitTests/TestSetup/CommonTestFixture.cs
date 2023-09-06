using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;

namespace WebApi.UnitTests.TestSetup;

public class CommonTestFixture
{
    public BookstoreDbContext Context { get; set; }
    public IMapper Mapper { get; set; }

    public CommonTestFixture()
    {
        var options = new DbContextOptionsBuilder<BookstoreDbContext>().UseInMemoryDatabase(databaseName: "BookStoreTestDB").Options;
        Context = new BookstoreDbContext(options);
        Context.Database.EnsureCreated();
    }
}