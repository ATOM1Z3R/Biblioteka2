using Domain.Models;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Biblioteka2Tests
{
    public class ReservationRepoTests : TestBase
    {
        [Fact]
        public void ReadListData_ShouldReturnAllUserValue()
        {
            string userId = "123";

            var expected = _context.Reservations.Where(x => x.User.Id == userId).Count();
            var actual = uow.Reservations.GetUserReservations(userId).Count();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task WriteData_ShouldReturnTrue()
        {
            var userId = "111";
            var bookId = 1;
            var user = _context.Users.Find(userId);
            var book = _context.Books.Find(bookId);
            Reservation reservation = new()
            {
                Book = book,
                User = user,
            };

            var actual = await uow.Reservations.MakeAsync(reservation);

            Assert.True(actual);
        }

        [Fact]
        public async Task WriteData_ShouldReturnFalse()
        {
            var userId = "111";
            var bookId = 2;
            var user = _context.Users.Find(userId);
            var book = _context.Books.Find(bookId);
            Reservation reservation = new()
            {
                Book = book,
                User = user,
            };

            var actual = await uow.Reservations.MakeAsync(reservation);

            Assert.False(actual);
        }

        [Fact]
        public void Retrive_ShouldReturnTrue()
        {
            var reservationId = 222;
            var reservation = _context.Reservations.Find(reservationId);

            var actual = uow.Reservations.Retrive(reservation);

            Assert.True(actual);
        }
    }
}
