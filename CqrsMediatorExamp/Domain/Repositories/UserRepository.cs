using CqrsMediatorExamp.Domain.Models;

namespace CqrsMediatorExamp.Domain.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }
    }
}