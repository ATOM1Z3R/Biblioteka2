using Domain.Models;
using System.Collections.Generic;

namespace Biblioteka2Tests.SeedData
{
    public static class AuthorSeed
    {
        public static List<Author> Authors { get; set; } = new()
        {
            new Author { AuthorId = 1, FirstName = "firstname1", LastName = "lastname1" },
            new Author { AuthorId = 2, FirstName = "firstname2", LastName = "lastname2" },
            new Author { AuthorId = 3, FirstName = "firstname3", LastName = "lastname3" },
        };
    }
}
