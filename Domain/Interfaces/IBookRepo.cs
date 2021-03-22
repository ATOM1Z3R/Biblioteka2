using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IBookRepo : IGenericRepo<Book>
    {
        Task<Book> GetBookAndItsReservationsAsync(int id);
    }
}
