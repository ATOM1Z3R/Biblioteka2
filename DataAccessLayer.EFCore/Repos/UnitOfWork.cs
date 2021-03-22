using DataAccessLayer.EFCore.Data;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace DataAccessLayer.EFCore.Repos
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context, UserManager<User> user)
        {
            _context = context;
            Books = new BookRepo(_context);
            Authors = new AuthorRepo(_context);
            Reservations = new ReservationRepo(_context);
            Users = user;
        }
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Books = new BookRepo(_context);
            Authors = new AuthorRepo(_context);
            Reservations = new ReservationRepo(_context);
            Users = null;
        }
        public IBookRepo Books { get; private init; }

        public IReservationRepo Reservations { get; private init; }

        public IAuthorRepo Authors { get; private init; }
        public UserManager<User> Users { get; private init; }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
