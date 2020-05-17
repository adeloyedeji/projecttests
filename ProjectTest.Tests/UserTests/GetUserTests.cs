using Microsoft.Azure.Cosmos;
using Moq;
using ProjectTest.Libs.Contracts;
using ProjectTest.Libs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;
using User = ProjectTest.Libs.Models.User;

namespace ProjectTest.Tests.UserTests
{
    public class GetUserTests
    {
        private readonly List<User> _usersCollection;

        public GetUserTests()
        {
            _usersCollection = new List<User>()
            {
                new User()
                {
                    Id          = "1111-1111-1111",
                    FirstName   = "Clark",
                    LastName    = "Kent",
                    UserName    = "SuperMan",
                    Address     = "Daily Planet",
                    PhoneNumber = "1234567890"
                },
                new User()
                {
                    Id          = "2222-2222-2222",
                    FirstName   = "Barry",
                    LastName    = "Allen",
                    UserName    = "Flash",
                    Address     = "Star Labs",
                    PhoneNumber = "12345678901"
                }
            };
        }

        [Fact]
        public async Task GetAsync_ValidInput_ShouldSucceed()
        {
            //Arrange
            var userInterface = new Mock<IUser>();
            userInterface.Setup(u => u.GetAsync(It.IsAny<string>())).ReturnsAsync(new User());

            //Act
            var userId = Guid.NewGuid().ToString();
            var result = await userInterface.Object.GetAsync(userId);

            //Assert
            Assert.IsType<User>(result);
        }

        [Fact]
        public async Task GetAsync_ValidInputFromList_ShouldSucceed()
        {
            //Arrange
            var userInterface = new Mock<IUser>();
            userInterface.Setup(u => u.GetAsync(It.IsAny<string>()))
                         .Returns((string id) =>
                         {
                             var user = _usersCollection.Where(u => u.Id == id).FirstOrDefault();
                             return Task.FromResult(user);
                         });

            //Act
            var userId = "1111-1111-1111";
            var result = await userInterface.Object.GetAsync(userId);

            //Assert
            Assert.IsType<User>(result);
            Assert.Equal("Clark", result.FirstName);
            Assert.Equal("Kent", result.LastName);
            Assert.Equal("SuperMan", result.UserName);
        }

        [Fact]
        public void GetAsync_InvalidInputFromList_ShouldFail()
        {
            //Arrange
            var userInterface = new Mock<IUser>();
            userInterface.Setup(u => u.GetAsync(It.IsAny<string>()))
                         .Returns((string id) =>
                         {
                             var user = _usersCollection.Where(u => u.Id == id).FirstOrDefault();
                             user = user ?? throw new CosmosException("Item not found", HttpStatusCode.NotFound, 0, "", 0.0);
                             return Task.FromResult(user);
                         });

            //Act
            var userId = Guid.NewGuid().ToString();

            //Assert
            Assert.Throws<CosmosException>(() => userInterface.Object.GetAsync(userId).Result);
        }
    }
}
