using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LojaDoSeuManoel.Api.Dtqs;
using LojaDoSeuManoel.Api.Entities;
using LojaDoSeuManoel.Api.Entities.Enums;
using LojaDoSeuManoel.Api.Repositories.Interfaces;
using LojaDoSeuManoel.Api.Services;
using Moq;

namespace LojaDoSeuManoel.Test.Services
{
    public class UserServiceTest
    {
        private readonly Mock<IUserRepository> _userRepository = new Mock<IUserRepository>();
        private readonly UserService _userService ;

        public UserServiceTest()
        {
            _userRepository = new Mock<IUserRepository>();
            _userService = new UserService(_userRepository.Object);
        }

        [Fact]
        public async Task GetFindByEmailAsync()
        {
            // Arrange
            var email = "test@email.com";
            var expectedUser = new User { Email = email, Name = "user_test" };

            _userRepository.Setup(r => r.GetFindByEmailAsync(email))
                .ReturnsAsync(expectedUser);

            // Act
            var result = await _userService.GetFindByEmailAsync(email);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(email, result.Email);
        }

        [Fact]
        public async Task GetUserByIdWhenUserExists()
        {
            // Arrange
            var userId = "123";
            var user = new User { Id = userId, Name = "user-test", Address = "Address A" };

            _userRepository.Setup(r => r.GetUserFromId(userId))
                .ReturnsAsync(user);

            // Act
            var result = await _userService.GetUserById(userId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("user-test", result.Name);
        }

        [Fact]
        public async Task GetUsersReturnsListUserDtos()
        {
            // Arrange
            var users = new List<User>
            {
                new User { Id = "1", Name = "user_test1" },
                new User { Id = "2", Name = "user_test2" }
            };

            _userRepository.Setup(r => r.GetUserAsync())
                .ReturnsAsync(users);

            // Act
            var result = await _userService.GetUsers();

            // Assert
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task UpdateReturnsTrue()
        {
            // Arrange
            var userDtq = new UserDtq { Id = "123", Name = "New Name", Address = "New Address" };
            var user = new User { Id = "123", Name = "old Name", Address = "old Address" };

            _userRepository.Setup(r => r.GetUserFromId(userDtq.Id))
                .ReturnsAsync(user);
            _userRepository.Setup(r => r.Update(It.IsAny<User>()))
                .ReturnsAsync(true);

            // Act
            var result = await _userService.Update(userDtq);

            // Assert
            Assert.True(result);
            _userRepository.Verify(r => r.Update(It.Is<User>(u => u.Name == userDtq.Name && u.Address == userDtq.Address)), Times.Once);
        }

        [Fact]
        public async Task UpdateReturnsFalse()
        {
            // Arrange
            var userDtq = new UserDtq { Id = "invalid_id", Name = "Name" };

            _userRepository.Setup(r => r.GetUserFromId(userDtq.Id))
                .ReturnsAsync((User)null!);

            // Act
            var result = await _userService.Update(userDtq);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task UpdateRoleReturnsTrue()
        {
            // Arrange
            var userId = "admin_id";
            var user = new User { Id = userId, Role = UserRole.Admin};

            _userRepository.Setup(r => r.GetUserFromId(userId))
                .ReturnsAsync(user);
            _userRepository.Setup(r => r.Update(It.IsAny<User>()))
                .ReturnsAsync(true);

            // Act
            var result = await _userService.UpdateRole(userId);

            // Assert
            Assert.True(result);
            Assert.Equal(UserRole.Admin, user.Role);
        }

    }
}
