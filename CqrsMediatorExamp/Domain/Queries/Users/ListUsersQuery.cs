using CqrsMediatorExamp.Domain.Queries.Dto;
using MediatR;

namespace CqrsMediatorExamp.Domain.Queries.Users
{
    public class ListUsersQuery : IRequest<IEnumerable<ListUserDto>>
    {
    }
}