using CqrsMediatorExamp.Domain.Communication;
using CqrsMediatorExamp.Domain.Models;
using MediatR;

namespace CqrsMediatorExamp.Domain.Commands.Users
{
    public class DeleteUserCommand : IRequest<Response<User>>
    {
        public int Id { get; private set; }

        public DeleteUserCommand(int id)
        {
            this.Id = id;
        }
    }
}