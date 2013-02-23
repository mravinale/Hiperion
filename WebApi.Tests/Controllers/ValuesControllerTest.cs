using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using WebApi.Controllers;
using WebApi.Models;
using WebApi.Services;
using System.Net;
 
using WebApi.Tests.Helpers;

namespace WebApi.Tests.Controllers
{
    [TestFixture]
    public class ValuesControllerTest
    {
        private List<UserDto> userList;
         
        [SetUp]
        public void initData()
        {
             userList = new List<UserDto>
                {
                    new UserDto
                        {
                            Id= 1,
                            Address = "Address1",
                            Name = "Name1"
                        },
                    new UserDto
                        {
                            Id= 2,
                            Address = "Address2",
                            Name = "Name2"
                        }
                };
        }

        [Test]
        public void Get()
        {
            //Arrange
            var userServiceMock = new Mock<IUserServices>();
            userServiceMock.Setup(foo => foo.GetAllUsers()).Returns(userList);

            var controller = new UserController(userServiceMock.Object);
			controller.SetupController<UserDto>();

            // Act
            var result = controller.Get();

            // Assert
            Assert.IsNotNull(result);
			Assert.AreEqual(2, result.GetContent<IEnumerable<UserDto>>().Count());
			Assert.AreEqual("Name1", result.GetContent<IEnumerable<UserDto>>().ElementAt(0).Name);
			Assert.AreEqual("Name2", result.GetContent<IEnumerable<UserDto>>().ElementAt(1).Name);
        }

        [Test]
        public void Post()
        {
            //Arrange
            var userServiceMock = new Mock<IUserServices>();
            userServiceMock.Setup(foo => foo.SaveOrUpdateUser(userList.ElementAt(0))).Returns(true);
            
            var controller = new UserController(userServiceMock.Object);
            controller.SetupController<UserDto>();
          
            // Act
            var responseMessage = controller.Post(userList.ElementAt(0));
             
            // Assert
            Assert.IsNotNull(responseMessage);
            Assert.AreEqual(responseMessage.StatusCode, HttpStatusCode.OK);
            Assert.AreEqual(responseMessage.GetContent<UserDto>().Name, userList.ElementAt(0).Name);

        }
    }
}
