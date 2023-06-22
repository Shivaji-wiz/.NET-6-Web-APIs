using AutoMapper;
using CorpEstate.BLL.Model;
using CorpEstate.Controllers;
using CorpEstate.DAL.DTO;
using CorpEstate.DAL.Repository.IRepository;
using CorpEstate.Services;
using CorpEstate.Services.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CorpEstateTest
{
    [TestFixture]
    public class AuthControllerTests
    {
        private AuthController _authController;
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IMapper> _mapperMock;
        private Mock<IRepository<User>> _repositoryMock;
        private Mock<IJwtService> _jwtServiceMock;
        private Mock<IConfiguration> _configurationMock;

        [SetUp]
        public void Setup()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _mapperMock = new Mock<IMapper>();
            _repositoryMock = new Mock<IRepository<User>>();
            _jwtServiceMock = new Mock<IJwtService>();
            _configurationMock = new Mock<IConfiguration>();
            _authController = new AuthController(_userRepositoryMock.Object, _mapperMock.Object, _jwtServiceMock.Object);
        }

        [Test]
        public async Task RegisterUser_WhenValidRequest_ReturnsCreatedResult()
        {
            // Arrange
            var newUser = new UserCreateDTO
            {

            };

            byte[] expectedHash = new byte[] { };
            byte[] expectedSalt = new byte[] { };
            _jwtServiceMock.Setup(s => s.CreatePasswordHash(newUser.Password, out expectedHash, out expectedSalt));

            var expectedUser = new User();
            _mapperMock.Setup(m => m.Map<User>(newUser)).Returns(expectedUser);

            _userRepositoryMock.Setup(d => d.CreateAsync(expectedUser)).Returns(Task.CompletedTask);

            //Act
            var result = await _authController.Register(newUser);

            //Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);

        }

        [Test]
        public void CreatePasswordHash_WithValidPassword_ReturnsHashAndSalt()
        {
            // Arrange
            var jwtService = new JwtService(_configurationMock.Object);
            string password = "test123";
            byte[] expectedPasswordHash;
            byte[] expectedPasswordSalt;

            // Act
            jwtService.CreatePasswordHash(password, out expectedPasswordHash, out expectedPasswordSalt);

            // Assert
            Assert.NotNull(expectedPasswordHash);
            Assert.NotNull(expectedPasswordSalt);
        }


        [Test]
        public async Task Login_ValidUser_ReturnsOk()
        {
            // Arrange
            var request = new UserLoginDTO
            {
                Name = "AdminA",
                Password = "AdminA@123"
            };
            var user = new User(); // Mock the user returned from _dbUser.GetAsync
            _userRepositoryMock
                .Setup(r => r.GetAsync(u => u.Name == request.Name, false))
                .ReturnsAsync(user);
            _jwtServiceMock
                .Setup(s => s.CreateAdminToken(It.IsAny<User>()))
                .Returns("mockToken");

            // Act
            var result = await _authController.Login(request);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
        }
    

    }
}