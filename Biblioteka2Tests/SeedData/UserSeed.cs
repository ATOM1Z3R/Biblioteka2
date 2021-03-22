using Domain.Models;
using System.Collections.Generic;

namespace Biblioteka2Tests.SeedData
{
    public static class UserSeed
    {
        public static List<User> Users { get; set; } = new()
        {
            new User { Id = "111", FristName = "user555", LastName = "lastname000" },
            new User { Id = "4444", FristName = "user888", LastName = "lastname333" },
        };
    }
}
