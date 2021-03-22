using DataAccessLayer.EFCore.Data;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLayer.EFCore.Repos
{
    public class BookRepo : GenericRepo<Book>, IBookRepo
    {
        private readonly AppDbContext _context;

        public BookRepo(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public override IEnumerable<Book> GetAll()
        {
            var books = _context.Books.Include(x => x.Author);
            return books;
        }
        public override async Task<Book> GetAsync(int id)
        {
            var book = await _context.Books.Include(x => x.Author)
                                           .FirstOrDefaultAsync(x => x.BookId == id);
            return book;
        }

        public async Task<Book> GetBookAndItsReservationsAsync(int id)
        {
            var book = await _context.Books.Include(x => x.Author)
                                           .Include(x => x.Reservation)
                                           .ThenInclude(x => x.User)
                                           .FirstOrDefaultAsync(x => x.BookId == id);
            return book;
            
        }
    }
}
