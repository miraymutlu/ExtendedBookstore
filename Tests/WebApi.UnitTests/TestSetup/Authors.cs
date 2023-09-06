using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.UnitTests.TestSetup;

public static class Authors
{
    public static void AddAuthors(this BookstoreDbContext context)
    {
        context.Authors.AddRange(
            new Author
            {
                Name = "George",
                Surname = "Saunders",
                Birthday = new DateTime(1970,01,06)
            },
            new Author
            {
                Name = "Jean Louis",
                Surname = "Fourniere",
                Birthday = new DateTime(1938,12,16)
            },
            new Author
            {
                Name = "Haruki",
                Surname = "Murakami",
                Birthday = new DateTime(1956,06,24)
            });
    }
}