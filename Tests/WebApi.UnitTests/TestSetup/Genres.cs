using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.UnitTests.TestSetup;

public class Genres
{
    public static void AddGenres(BookstoreDbContext context)
    {
        context.Genres.AddRange(
            new Genre
            {
                Name = "Personal Growth"
            },
            new Genre
            {
                Name = "Science Fiction"
            },
            new Genre
            {
                Name = "Fantasy"
                    
            });
    }
}