using Domain.Models;
using System.Collections.Generic;

namespace Biblioteka2Tests.SeedData
{
    public static class BookSeed
    {
        public static List<Book> Books { get; set; } = new()
        {
            new Book
            {
                BookId = 1,
                Name = "Book1",
                Description = "Sample desc 1",
                ReleaseDate = new System.DateTime(2000, 01, 02),
                Author = AuthorSeed.Authors[0],
                Quantity = 2
            },
            new Book
            {
                BookId = 2,
                Name = "Book2",
                Description = "Sample desc 2",
                ReleaseDate = new System.DateTime(2010, 01, 02),
                Author = AuthorSeed.Authors[1],
                Quantity = 0
            },
        };
    }
}
