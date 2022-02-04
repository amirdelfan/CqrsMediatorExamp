using CqrsMediatorExamp.Domain.Queries.Dto;
using CqrsMediatorExamp.Domain.Repositories;
using CqrsMediatorExamp.Exceptions;
using MediatR;

namespace CqrsMediatorExamp.Domain.Queries.Users
{
    public class ListUsersQueryHandler : IRequestHandler<ListUsersQuery, IEnumerable<ListUserDto>>
    {
        private readonly IUserRepository _userRepository;

        public ListUsersQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<ListUserDto>> Handle(ListUsersQuery request, CancellationToken cancellationToken)
        {
            //throw new CommandInvalidException("I can't do this");
            var users = await _userRepository.ListAsync();
            var result = new List<ListUserDto>();
            foreach (var user in users)
            {
                result.Add(new ListUserDto() { Id = user.Id, Name = user.Name });
            }
            return result;
        }
    }
}