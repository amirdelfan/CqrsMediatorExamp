using CqrsMediatorExamp.Domain.Queries.Dto;
using CqrsMediatorExamp.Domain.Repositories;
using MediatR;

namespace CqrsMediatorExamp.Domain.Queries.Users
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, GetUserDto>
    {
        private readonly IUserRepository _userRepository;

        public GetUserQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<GetUserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            if (request.UserId <= 0)
            {
                throw new ArgumentException(nameof(request.UserId));
            }

            var user = await _userRepository.FindByIdAsync(request.UserId);

            return new GetUserDto()
            {
                Id = user.Id,
                Age = user.Age,
                Name = user.Name
            };
        }
    }
}