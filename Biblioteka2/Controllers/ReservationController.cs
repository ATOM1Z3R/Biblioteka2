using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Biblioteka2.Controllers
{
    [Authorize]
    public class ReservationController : Controller
    {
        private readonly IUnitOfWork _uow;

        public ReservationController(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public IActionResult Index()
        {
            if (User.IsInRole("emp"))
            {
                return View(_uow.Reservations.GetAll());
            }
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return View(_uow.Reservations.GetUserReservations(userId));

        }

        public async Task<IActionResult> Reserve(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }
            var book = await _uow.Books.GetAsync((int)id);
            if(book is null)
            {
                return NotFound();
            }
            var user = await _uow.Users.GetUserAsync(User);
            Reservation reservation = new()
            {
                Book = book,
                User = user,
            };
            if(await _uow.Reservations.MakeAsync(reservation))
            {
                await _uow.SaveAsync();
            }
            
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Retrive(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var reservation = await _uow.Reservations.GetAsync((int)id);
            if(reservation is null)
            {
                return NotFound();
            }
            _uow.Reservations.Retrive(reservation);
            await _uow.SaveAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
