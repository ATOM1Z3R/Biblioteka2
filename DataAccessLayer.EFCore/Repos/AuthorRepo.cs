using DataAccessLayer.EFCore.Data;
using Domain.Interfaces;
using Domain.Models;

namespace DataAccessLayer.EFCore.Repos
{
    public class AuthorRepo : GenericRepo<Author>, IAuthorRepo
    {
        public AuthorRepo(AppDbContext context) : base(context)
        {
        }
    }
}
