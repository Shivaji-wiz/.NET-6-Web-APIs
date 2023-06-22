using AutoMapper;
using CorpEstate.BLL.Model;
using CorpEstate.Controllers;
using CorpEstate.DAL.DTO;
using CorpEstate.DAL.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CorpEstateTest
{
    [TestFixture]
    public class AdminControllerTests
    {
        private Mock<IPropertyRepository> _propertyRepositoryMock;
        private Mock<IMapper> _mapperMock;
        private AdminController _adminController;

        [SetUp]
        public void Setup()
        {
            _propertyRepositoryMock = new Mock<IPropertyRepository>();
            _mapperMock = new Mock<IMapper>();
            _adminController = new AdminController(_propertyRepositoryMock.Object, _mapperMock.Object);
        }

        [Test]
        public async Task GetProperties_ReturnsListOfUnapprovedProperties()
        {
            // Arrange
            var unapprovedProperties = new List<Property> { new Property(), new Property() };
            _propertyRepositoryMock.Setup(r => r.GetAllAsync(u => u.Approved == false)).ReturnsAsync(unapprovedProperties);
            _mapperMock.Setup(m => m.Map<List<PropertyDTO>>(unapprovedProperties)).Returns(new List<PropertyDTO>());

            // Act
            var response = await _adminController.GetProperties();

            // Assert
            var okResult = response.Result as OkObjectResult;
            var apiResponse = okResult.Value as APIResponse;
            Assert.IsTrue(apiResponse.IsSuccess);
            Assert.That(okResult.StatusCode, Is.EqualTo((int)HttpStatusCode.OK));
            Assert.IsNotNull(apiResponse.Result);
            Assert.IsInstanceOf<List<PropertyDTO>>(apiResponse.Result);
        }

        [Test]
        public async Task ApproveProperty_ReturnsActionResultOfAPIResponse()
        {
            // Arrange
            int id = 1;
            var approvePropertyDto = new ApprovePropertyDTO();

            // Act
            var response = await _adminController.ApproveProperty(id, approvePropertyDto);

            // Assert
            Assert.That(response, Is.TypeOf<ActionResult<APIResponse>>());
        }

        [Test]
        public async Task RejectProperty_ReturnsActionResultOfAPIResponse()
        {
            // Arrange
            int id = 1;

            // Act
            var response = await _adminController.RejectProperty(id);

            // Assert
            Assert.That(response, Is.TypeOf<ActionResult<APIResponse>>());
        }

    }

}
