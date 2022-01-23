using CQRSMediatR.Domain.Models;

namespace CQRSMediatR.Domain.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }
    }
}