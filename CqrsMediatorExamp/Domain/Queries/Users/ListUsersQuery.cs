using System.Collections.Generic;
using CqrsMediatorExamp.Domain.Models;
using MediatR;

namespace CqrsMediatorExamp.Domain.Queries.Users
{
    public class ListUsersQuery : IRequest<IEnumerable<User>>
    {
    }
}