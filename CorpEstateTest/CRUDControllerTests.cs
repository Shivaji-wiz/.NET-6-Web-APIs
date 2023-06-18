using AutoMapper;
using CorpEstate.BLL.Model;
using CorpEstate.Controllers;
using CorpEstate.DAL.DTO;
using CorpEstate.DAL.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;
using System.Linq.Expressions;
using System.Net;

namespace CorpEstateTest
{
    [TestFixture]
    public class CRUDControllerTests
    {
        private CRUDController _crudController;
        private Mock<IPropertyRepository> _propertyRepositoryMock;
        private Mock<IRepository<Property>> _repositoryMock;
        private Mock<IMapper> _mapperMock;

        [SetUp]
        public void Setup()
        {
            // Set up the mocks for IPropertyRepository and IMapper
            _propertyRepositoryMock = new Mock<IPropertyRepository>();
            _mapperMock = new Mock<IMapper>();
            _repositoryMock = new Mock<IRepository<Property>>();
            // Create an instance of the CRUDController class and inject the mocks
            _crudController = new CRUDController(_propertyRepositoryMock.Object, _mapperMock.Object);
        }

        [Test]
        public async Task GetProperties_WhenCalled_ReturnsOkResultWithResponse()
        {
            // Arrange
            var propertyList = new List<Property>();
            _repositoryMock.Setup(r => r.GetAllAsync(null)).ReturnsAsync(propertyList);

            var mappedPropertyList = new List<PropertyDTO>();
            _mapperMock.Setup(m => m.Map<List<PropertyDTO>>(It.IsAny<IEnumerable<Property>>())).Returns(mappedPropertyList);

            var expectedResponse = new APIResponse
            {
                Result = mappedPropertyList,
                IsSuccess = true,
                StatusCode = HttpStatusCode.OK
            };

            // Act
            var result = await _crudController.GetProperties();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = (OkObjectResult)result.Result;
            var actualResponse = okResult.Value as APIResponse;

            Assert.NotNull(actualResponse);
            Assert.AreEqual(expectedResponse.Result, actualResponse.Result);
            Assert.AreEqual(expectedResponse.IsSuccess, actualResponse.IsSuccess);
            Assert.AreEqual(expectedResponse.StatusCode, actualResponse.StatusCode);
        }

        [Test]
        public async Task CreateProperty_WhenValidRequest_ReturnsCreatedResult()
        {
            // Arrange
            var createPropertyDto = new CreatePropertyDTO
            {
                Property_Name = "Test Property",
            };

            var property = new Property();
            _mapperMock.Setup(m => m.Map<Property>(createPropertyDto)).Returns(property);

            _repositoryMock.Setup(d => d.CreateAsync(property)).Returns(Task.CompletedTask);

            var expectedResponse = new APIResponse
            {
                Result = _mapperMock.Object.Map<PropertyDTO>(property),
                IsSuccess = true,
                StatusCode = HttpStatusCode.Created
            };

            // Act
            var result = await _crudController.CreateProperty(createPropertyDto);

            // Assert
            Assert.IsInstanceOf<CreatedAtRouteResult>(result.Result);
            var createdAtRouteResult = (CreatedAtRouteResult)result.Result;
            var actualResponse = (APIResponse)createdAtRouteResult.Value;

            Assert.NotNull(actualResponse);
            Assert.AreEqual(expectedResponse.Result, actualResponse.Result);
            Assert.AreEqual(expectedResponse.IsSuccess, actualResponse.IsSuccess);
            Assert.AreEqual(expectedResponse.StatusCode, actualResponse.StatusCode);

        }

        [Test]
        public async Task CreateProperty_WhenInvalidRequest_ReturnsBadRequest()
        {
            // Arrange
            CreatePropertyDTO createPropertyDto = null;

            // Act
            var result = await _crudController.CreateProperty(createPropertyDto);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
            var badRequestResult = (BadRequestObjectResult)result.Result;
            Assert.AreEqual(createPropertyDto, badRequestResult.Value);
        }
    }
}