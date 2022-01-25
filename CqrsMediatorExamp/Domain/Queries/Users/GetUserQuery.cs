using CqrsMediatorExamp.Domain.Queries.Dto;
using MediatR;

namespace CqrsMediatorExamp.Domain.Queries.Users
{
    public class GetUserQuery : IRequest<GetUserDto>
    {
        public int UserId { get; private set; }

        public GetUserQuery(int userId)
        {
            this.UserId = userId;
        }
    }
}