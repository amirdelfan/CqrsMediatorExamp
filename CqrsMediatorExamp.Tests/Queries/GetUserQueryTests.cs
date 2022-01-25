using CqrsMediatorExamp.Domain.Models;
using CqrsMediatorExamp.Domain.Queries.Users;
using CqrsMediatorExamp.Domain.Repositories;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CqrsMediatorExamp.Tests.Queries
{
    public class GetUserQueryTests
    {
        private Mock<IUserRepository> mockUserRepository;

        IList<User> users = new List<User>
                {
                    new User { Id = 1, Name = "John", Age = 22},
                    new User { Id = 2, Name = "Olfa", Age = 30},
                    new User { Id = 3, Name = "Anke", Age = 19}
                };

        public GetUserQueryTests()
        {
            mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(mr => mr.FindByIdAsync(
                It.IsAny<int>())).ReturnsAsync((int i) => users.Where(
                x => x.Id == i).Single());
        }

        [Fact]
        public async Task GetExistingUserAsync()
        {
            var handler = new GetUserQueryHandler(this.mockUserRepository.Object);
            var result = await handler.Handle(new GetUserQuery(1), new System.Threading.CancellationToken());
            Assert.NotNull(result);
        }
    }
}