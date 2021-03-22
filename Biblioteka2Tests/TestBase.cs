using Biblioteka2Tests.SeedData;
using Microsoft.EntityFrameworkCore;
using System;
using DataAccessLayer.EFCore.Data;
using DataAccessLayer.EFCore.Repos;

namespace Biblioteka2Tests
{
    public class TestBase : IDisposable
    {
        protected readonly AppDbContext _context;
        readonly protected UnitOfWork uow;

        public TestBase()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            _context = new AppDbContext(options);
            _context.Database.EnsureCreated();
            _context.AddRange(BookSeed.Books);
            _context.AddRange(AuthorSeed.Authors);
            _context.AddRange(ReservationSeed.Reservations);
            _context.AddRange(UserSeed.Users);
            uow = new(_context);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
