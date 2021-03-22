using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IReservationRepo : IGenericRepo<Reservation>
    {
        IEnumerable<Reservation> GetUserReservations(string userId);
        Task<bool> MakeAsync(Reservation reservation);
        bool Retrive(Reservation reservation);

    }
}
