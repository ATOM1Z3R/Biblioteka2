using Domain.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IBookRepo Books { get; }
        IReservationRepo Reservations { get; }
        IAuthorRepo Authors { get;  }
        UserManager<User> Users { get;  }
        Task<int> SaveAsync();
    }
}
