using DataAccessLayer.EFCore.Data;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccessLayer.EFCore.Repos
{
    public class ReservationRepo : GenericRepo<Reservation>, IReservationRepo
    {
        private readonly AppDbContext _context;

        public ReservationRepo(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<Reservation> GetAsync(int id)
        {
            var reservation = await _context.Reservations.Include(x => x.Book)
                                                         .FirstOrDefaultAsync(x => x.ReservationId == id);
            return reservation;
        }

        public override IEnumerable<Reservation> GetAll()
        {
            var reservations = _context.Reservations.Include(x => x.Book)
                                                    .Include(x => x.User);
            return reservations;
        }

        public IEnumerable<Reservation> GetUserReservations(string userId)
        {
            var reservations = _context.Reservations.Include(x => x.Book)
                                                    .Where(x => x.User.Id == userId);
            return reservations;
        }

        public async Task<bool> MakeAsync(Reservation reservation)
        {
            
            if(reservation.Book.Quantity == 0)
            {
                return false;
            }
            await CreateAsync(reservation);
            reservation.Book.Quantity--;

            return true;
        }

        public bool Retrive(Reservation reservation)
        {
            if (reservation.RetriveDate == DateTime.MinValue)
            {
                reservation.RetriveDate = DateTime.Now;
                reservation.Book.Quantity++;
            }
            else
            {
                return false;
            }
            return true;
        }
    }
}
