using Domain.Models;
using System;
using System.Collections.Generic;

namespace Biblioteka2Tests.SeedData
{
    public static class ReservationSeed
    {
        public static List<Reservation> Reservations { get; set; } = new()
        {
            new Reservation
            {
                ReservationId = 111,
                ReservationDate = DateTime.Now,
                RetriveDate = DateTime.MinValue,
                User = UserSeed.Users[0],
                Book = BookSeed.Books[1]
            },
            new Reservation
            {
                ReservationId = 222,
                ReservationDate = DateTime.Now,
                RetriveDate = DateTime.MinValue,
                User = UserSeed.Users[1],
                Book = BookSeed.Books[1]
            },
            new Reservation
            {
                ReservationId = 333,
                ReservationDate = DateTime.Now,
                RetriveDate = DateTime.MinValue,
                User = UserSeed.Users[1],
                Book = BookSeed.Books[0]
            },
        };
    }
}
