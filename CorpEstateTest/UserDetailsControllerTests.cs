using AutoMapper;
using CorpEstate.BLL.Model;
using CorpEstate.Controllers;
using CorpEstate.DAL.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorpEstateTest
{
    [TestFixture]
    public class UserDetailsControllerTests
    {
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IMapper> _mapperMock;
        private Mock<IPropertyRepository> _propertyRepositoryMock;
        private UserDetailsController _userDetailsController;

        [SetUp]
        public void Setup()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _mapperMock = new Mock<IMapper>();
            _propertyRepositoryMock = new Mock<IPropertyRepository>();
            _userDetailsController = new UserDetailsController(_userRepositoryMock.Object, _propertyRepositoryMock.Object, _mapperMock.Object);
        }

        [Test]
        public async Task GetUser_ReturnsActionResultOfAPIResponse()
        {
            // Arrange
            string email = "test@example.com";

            // Act
            var response = await _userDetailsController.GetUser(email);

            // Assert
            Assert.That(response, Is.TypeOf<ActionResult<APIResponse>>());
        }

        [Test]
        public async Task GetAllUsers_ReturnsActionResultOfAPIResponse()
        {
            // Act
            var response = await _userDetailsController.GetAllUsers();

            // Assert
            Assert.That(response, Is.TypeOf<ActionResult<APIResponse>>());
        }

        [Test]
        public async Task GetAllUserProperty_ReturnsActionResultOfAPIResponse()
        {
            // Arrange
            int id = 1;

            // Act
            var response = await _userDetailsController.GetAllUserProperty(id);

            // Assert
            Assert.That(response, Is.TypeOf<ActionResult<APIResponse>>());
        }

        [Test]
        public async Task DeleteUser_ReturnsActionResultOfAPIResponse()
        {
            // Arrange
            int id = 1;

            // Act
            var response = await _userDetailsController.DeleteUser(id);

            // Assert
            Assert.That(response, Is.TypeOf<ActionResult<APIResponse>>());
        }

    }
}
