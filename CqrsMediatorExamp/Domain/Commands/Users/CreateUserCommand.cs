using CqrsMediatorExamp.Domain.Models;
using MediatR;

namespace CqrsMediatorExamp.Domain.Commands.Users
{
    public class CreateUserCommand : IRequest<User>
    {
        public string Name { get; private set; }
        public byte Age { get; private set; }

        public CreateUserCommand(string name, byte age)
        {
            this.Age = age;
            this.Name = name;
        }
    }
}